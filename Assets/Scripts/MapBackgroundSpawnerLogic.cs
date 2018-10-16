using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackgroundSpawnerLogic : MonoBehaviour {

	public float speed;
	public Vector3 respawnVector;

	void Update () {
		if (GameObject.Find("Player").GetComponent<GroundSpawnerLogic>().canMove && !GameObject.Find("Player").GetComponent<GroundSpawnerLogic>().playerEntering) 
			transform.Translate(Vector3.right * -speed * Time.deltaTime);
	}

	void OnTriggerExit2D(Collider2D player) {
		if (player.gameObject.tag == "Player") {
			transform.position = respawnVector;
		}
	}
		
}
