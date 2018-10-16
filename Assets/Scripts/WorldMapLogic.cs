using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLogic : MonoBehaviour {

	Vector2 startingPosition;
	Vector2 currentPosition;
	Vector2 movePosition;
	float dragDistance;

	bool transitionToMap = false;
	bool transitionToWorld_1 = false;

	void OnMouseDown() {
		startingPosition = Input.mousePosition;
	}

	void OnMouseDrag() {
		currentPosition = Input.mousePosition;
		dragDistance = Mathf.Abs((currentPosition.x - startingPosition.x) / 190f);
		movePosition = currentPosition - startingPosition;
		movePosition = movePosition.normalized;
		movePosition = new Vector2 (movePosition.x, 0f);
		if (transform.position.x > -3.6f && transform.position.x < 3.6f) {
			transform.position = (Vector2)transform.position + movePosition * dragDistance;
		}
		else {
			if (transform.position.x <= 3.6f && movePosition.x > 0) {
				transform.position = (Vector2)transform.position + movePosition * dragDistance;
			}
			if (transform.position.x >= -3.6f && movePosition.x < 0) {
				transform.position = (Vector2)transform.position + movePosition * dragDistance;
			}	
		}
		startingPosition = Input.mousePosition;
	}

	void Update() {
		if (transitionToWorld_1 == true) {
			GameObject.Find ("Main Camera").transform.position = Vector3.Lerp (GameObject.Find ("Main Camera").transform.position, GameObject.Find ("Camera_World1").transform.position, 20f * Time.deltaTime);
			if (Vector3.Distance (GameObject.Find ("Main Camera").transform.position,GameObject.Find ("Camera_World1").transform.position) < 0.05f) {
				transitionToWorld_1 = false;
				GameObject.Find ("Main Camera").transform.position = GameObject.Find ("Camera_World1").transform.position;
			}
		}

		if (transitionToMap == true) {
			GameObject.Find ("Main Camera").transform.position = Vector3.Lerp (GameObject.Find ("Main Camera").transform.position, GameObject.Find ("Camera_Map").transform.position, 10f * Time.deltaTime);
			if (Vector3.Distance (GameObject.Find ("Main Camera").transform.position,GameObject.Find ("Camera_Map").transform.position) < 0.05f) {
				transitionToMap = false;
				GameObject.Find ("Main Camera").transform.position = GameObject.Find ("Camera_Map").transform.position;
				GameObject.Find ("World1_Canvas").GetComponent<Canvas> ().enabled = false;
			}
		}
	}

	public void loadWorld1() {
		transitionToWorld_1 = true;
		GameObject.Find ("World1_Canvas").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("WorldMap").GetComponent<Animator> ().Play ("World1_Open");
		GameObject.Find ("MapHud").GetComponent<Canvas> ().enabled = false;
	}

	public void loadWorld1_Area(int areaNumber) {
		if (areaNumber == 1)
			SceneManager.LoadScene ("1_1_Beachside");
	}

	public void returnToMap(int worldNumber) {
		transitionToMap = true;
		GameObject.Find ("WorldMap").GetComponent<Animator> ().Play ("World" + worldNumber + "_Close");
		GameObject.Find ("MapHud").GetComponent<Canvas> ().enabled = true;
	}

	public void loadHub() {
		SceneManager.LoadScene ("Hub");
	}
}
