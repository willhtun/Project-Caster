using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobLogic : MonoBehaviour {

	public int maxHealth = 1;
	public int health = 0;
	public int enemyID;
	public int enemyCurrencyWorth;
	public bool isAlive = true;
	public bool inAction = false;
	private int attackIndex;
	public int enemyPosition;
	public bool shielded;

	public TextAsset enemyAttackPatternsFile;
	public int[] enemyComboID;
	public int[] enemyComboCode;
	public int[] enemyComboProperty;
	public int[] enemyDamage;
	public int[] selectedEnemyComboCodeInArray;
	public int[] selectedEnemyComboPropertyInArray;
	private int numberOfLinesInFile;
	private int numberOfAttacks;

	public Orb[] orbs = new Orb[6];

	void Start() {
		string[] filelines = enemyAttackPatternsFile.text.Split ('\n');
		selectedEnemyComboCodeInArray = new int[8];
		selectedEnemyComboPropertyInArray = new int[8];
		numberOfLinesInFile = filelines.Length;
		enemyComboID = new int[numberOfLinesInFile];
		enemyComboCode = new int[numberOfLinesInFile];
		enemyComboProperty = new int[numberOfLinesInFile];
		enemyDamage = new int[numberOfLinesInFile];

		numberOfAttacks = 0;
		for (int i = 0; i < numberOfLinesInFile; i++) {
			string[] parts = filelines [i].Split ('/');
			enemyComboID [i] = System.Int32.Parse(parts[0]);
			if (enemyComboID [i] == enemyID) {
				enemyComboCode [numberOfAttacks] = System.Int32.Parse (parts [1]);
				enemyComboProperty [numberOfAttacks] = System.Int32.Parse (parts [2]);
				enemyDamage [numberOfAttacks] = System.Int32.Parse (parts [3]);
				numberOfAttacks++;
			}
		}
		attackIndex = Random.Range (0, numberOfAttacks);
	}

	void Update() {
		if (!inAction && GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode) {
			StartCoroutine ("Action");
		}
	}

	public void subtractHealth(int damageTaken) {
		health = health - damageTaken;
		if (health > 0)
			GetComponent<Animator> ().Play ("Damage");
		else if (health <= 0)
			kill ();
	}

	public void kill() {
		isAlive = false;
		PlayerStats.Instance.money += enemyCurrencyWorth;
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().targetEnemyIndex = 0;
		GameObject.Find ("Player").GetComponent<GroundSpawnerLogic> ().enemiesCheckUpdate ();
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().turnOnHealthBar (false);
		StartCoroutine ("Die");
	}

	IEnumerator Action() {
		inAction = true;

		yield return new WaitForSeconds (0.5f);
		if (GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesLeftInFight > 1 && enemyPosition != GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().firstAttacker)
			yield return new WaitForSeconds (Random.Range (2, 5));
		
		turnOnOffEnemyUI (true);
		transform.Find ("EnemyUI/ChargeBar").GetComponent<Animator> ().Play ("Charge", 0, 0f);
		yield return new WaitForSeconds (6);

		turnOnOffEnemyUI (false);

		GetComponent<Animator> ().SetBool ("inAttack", true);
		yield return new WaitForSeconds (0.5f);
		GameObject.Find("Player").GetComponent<PlayerLogic>().subtractHealth(enemyDamage[attackIndex]);
		GetComponent<Animator> ().SetBool ("inAttack", false);
		yield return new WaitForSeconds (0.5f);
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().cursorIndex = 0;
		engageEnemy ();
		inAction = false;
	}

	IEnumerator Die() {
		GetComponent<Animator> ().Play ("Die");
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}

	void turnOnOffEnemyUI (bool onOrOff) {
		transform.Find ("EnemyUI").GetComponent<Canvas> ().enabled = onOrOff;
		transform.Find ("EnemyUI/ChargeBar").GetComponent<SpriteRenderer> ().enabled = onOrOff;
	}

	public void breakAction() {
		inAction = false;
		StopCoroutine ("Action");
		turnOnOffEnemyUI (false);
	}

	public void breakShield() {
		shielded = false;
		transform.Find("EnemyUI/Shield").GetComponent<SpriteRenderer>().enabled = false;
	}
		
	public void engageEnemy() {
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().targetEnemyIndex = enemyPosition;
		for (int i = 0; i < 8; i++) {
			selectedEnemyComboCodeInArray [i] = (enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10;
			selectedEnemyComboPropertyInArray [i] = (enemyComboProperty [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10;
		}

		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().checkCursor ();
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode = true;
		GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().turnOnHealthBar (true);

		for (int i = 0; i < 8; i++) {
			GameObject.Find ("DAttack" + i).GetComponent<Image> ().enabled = true; 
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 0) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().enabled = false;
			}
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 1) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().sprite = orbs [1].sprite;
			}
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 2) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().sprite = orbs [2].sprite;
			}
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 3) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().sprite = orbs [3].sprite;
			}
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 4) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().sprite = orbs [4].sprite;
			}
			if ((enemyComboCode [attackIndex] / (int)(Mathf.Pow(10, 7 - i))) % 10 == 5) {
				GameObject.Find ("DAttack" + i).GetComponent<Image> ().sprite = orbs [5].sprite;
			}
		}

		for (int i = 0; i < 7; i++) {
			if ((enemyComboProperty [attackIndex] / (int)(Mathf.Pow(10, 6 - i))) % 10 == 1) {
				GameObject.Find ("DLink" + i).GetComponent<Image> ().enabled = true;
			}
		}
	}
}
