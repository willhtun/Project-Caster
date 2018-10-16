using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundSpawnerLogic: MonoBehaviour {

	public GameObject ground_prefab;
	public GameObject ground_1;
	public GameObject ground_2;

	public bool canMove = true;
	public bool playerEntering = true;

	public int battleCount = 0;

	void Update () {
		if (transform.position.x < -1.86f) {
			this.GetComponent<PlayerLogic> ().transform.Translate (Vector3.right * 1.5f * Time.deltaTime);
		} else
			playerEntering = false;
		if (canMove && !playerEntering) {
			ground_1.transform.Translate(Vector3.right * -2f * Time.deltaTime);
			ground_2.transform.Translate(Vector3.right * -2f * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D ground) {
		if (ground.gameObject.tag == "Ground") {
			Destroy (ground_1);
			ground_1 = ground_2;
			ground_2 = (GameObject)Instantiate (ground_prefab, new Vector3 (ground_1.transform.position.x + 10f, ground_1.transform.position.y, ground_1.transform.position.z), Quaternion.identity);
			GameObject.Find ("BattleCountDisplay").GetComponent<Text> ().text = "Battles Won - " + battleCount;
			battleCount++;
		}
	}

	void OnTriggerExit2D(Collider2D ground) {
		if (ground.gameObject.tag == "Ground") {
			if (ground_1.GetComponent<EnemySpawnerLogic> ().enemyPresent == true) {
				canMove = false;
				GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode = true;
			}
		}
	}

	public void enemiesCheckUpdate() {
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesLeftInFight--;
		if (GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesLeftInFight == 0) {
			canMove = true;
			GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode = false;
		}
	}
}
