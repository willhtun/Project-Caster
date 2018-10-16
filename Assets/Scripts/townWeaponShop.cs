using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townWeaponShop : MonoBehaviour {
	
	public int weapon1_price;

	public void buyWeapon (string weaponCode) {
		switch (weaponCode) {
		case "W1":
			if (PlayerStats.Instance.money >= weapon1_price) {
				PlayerStats.Instance.money -= weapon1_price;
			}
			break;
		default:
			break;
		}
		checkWeapons ();
	}

	void checkWeapons() {
	}
}
