using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DashboardLogic : MonoBehaviour {
	
	public Orb[] orbs = new Orb[4];

	private float shieldCharge = 1;

	public bool inBattleMode = false;
	public bool shielded = false;
	public bool isDragging = false;
	public int[] draggingOrbs = new int[8];

	public GameObject[] enemiesInFight;
	public int enemiesSpawnedInFight;
	public int enemiesLeftInFight;
	public int firstAttacker;
	public int cursorIndex = 0;
	public int draggingOrbsIndex = 0;

	public int targetEnemyIndex = 0;

	public GameObject target_GO;
	public GameObject enemyHealthBar_GO;
	public GameObject enemyHealthBarBase_GO;


	void Start () {
		itemChecker ();
	}

	void Update () {
		shieldChargeCounterLogic ();
	}

	public void attackingOrb(int orbSlotIndex) {
		if (inBattleMode) {
			if (orbSlotIndex == enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().selectedEnemyComboCodeInArray [cursorIndex]
			    && enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().selectedEnemyComboPropertyInArray [cursorIndex] == 0) {
				moveCursor ();
			} else {
				cursorIndex = 0;
				enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().engageEnemy ();
			}
		}
	}		

	public void dragAttackingOrb(int orbSlotIndex) {
		if (inBattleMode && isDragging) {
			draggingOrbsIndex++;
			draggingOrbs [draggingOrbsIndex] = orbSlotIndex;
		}
	}

	public void moveCursor() {
		GameObject.Find ("DAttack" + cursorIndex).GetComponent<Image> ().enabled = false;
		if (cursorIndex < enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().selectedEnemyComboCodeInArray.Length) {
			cursorIndex++;
			checkCursor ();
		}
	}

	public void checkCursor() {
		if (cursorIndex >= 8) {
			enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().kill ();
			cursorIndex = 0;
		}
		else if (enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().selectedEnemyComboCodeInArray [cursorIndex] == 0 &&
		    cursorIndex < enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().selectedEnemyComboCodeInArray.Length) {
			cursorIndex++;
			checkCursor ();
		}

	}

	void shieldChargeCounterLogic() {
		GameObject.Find ("battlePod_ShieldCharge").GetComponent<Image> ().fillAmount = shieldCharge / 1f;
		if (shieldCharge < 1f)
			shieldCharge += Time.deltaTime / PlayerStats.Instance.shieldRechargeSpeed;
	}

	void itemChecker() {
		transform.Find ("Item_1").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot1.itemSprite;
		transform.Find ("Item_2").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot2.itemSprite;
		transform.Find ("Item_1/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot1.itemStack.ToString();
		transform.Find ("Item_2/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot2.itemStack.ToString();
	}

	public void breakEnemy() {
		enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().breakAction ();
		GameObject.Find ("Debug").GetComponent<Text> ().text = "break!";
	}

	public void quitToHub() {
		PlayerStats.Instance.Save ();
		SceneManager.LoadScene ("Hub");
	}

	public void useItemSlot1(){
		PlayerStats.Instance.itemSlot1.use ();
		transform.Find ("Item_1/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot1.itemStack.ToString ();
	}

	public void useItemSlot2(){
		PlayerStats.Instance.itemSlot2.use ();
		transform.Find ("Item_2/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot2.itemStack.ToString ();
	}
		
	public void turnOnHealthBar(bool onOff) {
		enemyHealthBar_GO.GetComponent<Image> ().fillAmount = (float) enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().health / enemiesInFight [targetEnemyIndex].GetComponent<MobLogic> ().maxHealth;
		enemyHealthBar_GO.GetComponent<Image> ().enabled = onOff;
		enemyHealthBarBase_GO.GetComponent<Image> ().enabled = onOff;
		target_GO.GetComponent<SpriteRenderer> ().enabled = onOff;
		target_GO.transform.position = enemiesInFight [targetEnemyIndex].transform.Find ("EnemyTarget/Target").transform.position;
	}

	public void initiateDrag(int orbSlotIndex) {
		isDragging = true;
		draggingOrbsIndex = 0;
		draggingOrbs [draggingOrbsIndex] = orbSlotIndex;
	}

	public void endDrag() {
		isDragging = false;

	}
}

