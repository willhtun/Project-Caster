using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerLogic : MonoBehaviour {

	public bool enemyPresent;
	public GameObject enemy;
	public const int TYPES_OF_GROUND_ENEMIES = 1;
	public const int TYPES_OF_AIR_ENEMIES = 1;
	public GameObject[] groundEnemies = new GameObject[TYPES_OF_GROUND_ENEMIES];
	public GameObject[] airEnemies = new GameObject[TYPES_OF_AIR_ENEMIES];

	void OnTriggerEnter2D(Collider2D ground) {
		int groundOrAir = Random.Range (1, 3); // 1 = ground, 2 = air

		//=====Spawn Ground=====//
		if (groundOrAir == 1) {
			int enemyIndex = Random.Range (0, groundEnemies.Length);
			int maxEnemies = 1;
			if (GameObject.Find ("Player").GetComponent<GroundSpawnerLogic> ().battleCount > 2)
				maxEnemies = 2;
			int numberOfEnemies = Random.Range (1, maxEnemies + 1);
			GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight = new GameObject[numberOfEnemies];
			for (int i = 0; i < numberOfEnemies; i++) {
				{
					enemy = (GameObject)Instantiate (groundEnemies[enemyIndex], transform.Find ("groundSpawner_" + (i + 1)).position, Quaternion.identity);
					enemy.transform.parent = transform.Find ("groundSpawner_" + (i + 1)).transform;
					enemyPresent = true;
					GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight [i] = enemy;
					GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight [i].GetComponent<MobLogic> ().enemyPosition = i;
				}
				GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesLeftInFight = numberOfEnemies;
				GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesSpawnedInFight = numberOfEnemies;
			}
		} 

		//=====Spawn Air=====//
		else if (groundOrAir == 2) {
			int enemyIndex = Random.Range (0, airEnemies.Length);
			int maxEnemies = 1;
			if (GameObject.Find ("Player").GetComponent<GroundSpawnerLogic> ().battleCount > 2)
				maxEnemies = 2;
			else if (GameObject.Find ("Player").GetComponent<GroundSpawnerLogic> ().battleCount > 5)
				maxEnemies = 3;
			int numberOfEnemies = Random.Range (1, maxEnemies + 1);
			GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight = new GameObject[numberOfEnemies];
			for (int i = 0; i < numberOfEnemies; i++) {
				{
					enemy = (GameObject)Instantiate (airEnemies[enemyIndex], transform.Find ("airSpawner_" + (i + 1)).position, Quaternion.identity);
					enemy.transform.parent = transform.Find ("airSpawner_" + (i + 1)).transform;
					enemyPresent = true;
					GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight [i] = enemy;
					GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight [i].GetComponent<MobLogic> ().enemyPosition = i;
				}
				GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesLeftInFight = numberOfEnemies;
				GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesSpawnedInFight = numberOfEnemies;
			}
			GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().firstAttacker = Random.Range (0, 2);
		}
	}

	void OnTriggerExit2D(Collider2D ground) {
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode = true;
	}
}
