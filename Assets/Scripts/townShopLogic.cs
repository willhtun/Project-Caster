using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class townShopLogic : MonoBehaviour {

	public GameObject player_GO;
	private bool isStandingInFrontOfShop = false;

	public Canvas itemShop_C;
	public GameObject itemShop_BG;
	public Canvas upgradeShop_C;
	public GameObject upgradeShop_BG;
	public Canvas weaponShop_C;
	public GameObject weaponShop_BG;

	void Update() { //placeholder
		GameObject.Find("Money").GetComponent<Text>().text = "Money - " + PlayerStats.Instance.money;
	}

	void OnTriggerEnter2D(Collider2D player) {
		this.transform.Find ("Bubble").GetComponent<SpriteRenderer> ().enabled = true;
		isStandingInFrontOfShop = true;
	}

	void OnTriggerExit2D(Collider2D player) {
		this.transform.Find ("Bubble").GetComponent<SpriteRenderer> ().enabled = false;
		isStandingInFrontOfShop = false;
	}

	public void openItemShop() {
		if (isStandingInFrontOfShop == true) {
			player_GO.GetComponent<townPlayerMovement>().inShop = true;
			itemShop_C.enabled = true;
			itemShop_BG.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	public void closeItemShop() {
		PlayerStats.Instance.Save ();
		player_GO.GetComponent<townPlayerMovement>().inShop = false;
		itemShop_C.enabled = false;
		itemShop_BG.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void openUpgradeShop() {
		if (isStandingInFrontOfShop == true) {
			player_GO.GetComponent<townPlayerMovement>().inShop = true;
			upgradeShop_C.enabled = true;
			upgradeShop_BG.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	public void closeUpgradeShop() {
		PlayerStats.Instance.Save ();
		player_GO.GetComponent<townPlayerMovement>().inShop = false;
		upgradeShop_C.enabled = false;
		upgradeShop_BG.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void openWeaponShop() {
		if (isStandingInFrontOfShop == true) {
			player_GO.GetComponent<townPlayerMovement>().inShop = true;
			weaponShop_C.enabled = true;
			weaponShop_BG.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	public void closeWeaponShop() {
		PlayerStats.Instance.Save ();
		player_GO.GetComponent<townPlayerMovement>().inShop = false;
		weaponShop_C.enabled = false;
		weaponShop_BG.GetComponent<SpriteRenderer> ().enabled = false;
	}

}
