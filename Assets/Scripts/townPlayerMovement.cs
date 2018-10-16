using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class townPlayerMovement : MonoBehaviour {

	public Vector2 targetPosition;
	public float distance;
	private bool moving = false;
	private bool walkable = false;
	public bool inShop = false;

	void Start() {
		targetPosition = this.transform.position;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0) && inShop == false) {
			moving = true;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit) && walkable == true) {
				targetPosition = hit.point;
				distance = Vector2.Distance (targetPosition, this.transform.position);
			}

		}
		if (moving) {
			this.transform.position = Vector2.Lerp (this.transform.position, targetPosition, (5f / distance) * Time.deltaTime);
			if ((Vector2)this.transform.position == targetPosition)
				moving = false;
		}
	}

	public void returnToHub() {
		SceneManager.LoadScene ("Hub");
	}

	public void turnOnOffWalkable(bool onOff) {
		walkable = onOff;
	}
}