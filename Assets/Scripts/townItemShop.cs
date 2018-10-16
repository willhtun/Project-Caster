using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class townItemShop : MonoBehaviour {

	public int healthPotionS_price;
	public int healthPotionM_price;
	public int healthPotionL_price;
	public int breakBomb_price;
	public int megaBreakBomb_price;

	public Text[] InventoryText = new Text[5];

	void Start () {
		checkInventory ();
	}

	public void buyItem (string itemCode) {
		switch (itemCode) {
		case "H1":
			if (PlayerStats.Instance.money >= healthPotionS_price) {
				PlayerStats.Instance.money -= healthPotionS_price;
				PlayerStats.Instance.healthPotionS.itemCount++;
			}
			break;
		case "H2":
			if (PlayerStats.Instance.money >= healthPotionM_price) {
				PlayerStats.Instance.money -= healthPotionM_price;
				PlayerStats.Instance.healthPotionM.itemCount++;
			}
			break;
		case "H3":
			if (PlayerStats.Instance.money >= healthPotionL_price) {
				PlayerStats.Instance.money -= healthPotionL_price;
				PlayerStats.Instance.healthPotionL.itemCount++;
			}
			break;
		case "B1":
			if (PlayerStats.Instance.money >= breakBomb_price) {
				PlayerStats.Instance.money -= breakBomb_price;
				PlayerStats.Instance.breakBomb.itemCount++;
			}
			break;
		case "B2":
			if (PlayerStats.Instance.money >= megaBreakBomb_price) {
				PlayerStats.Instance.money -= megaBreakBomb_price;
				PlayerStats.Instance.megaBreakBomb.itemCount++;
			}
			break;
		default:
			break;
		}
		checkInventory ();
	}

	void checkInventory() {
		InventoryText [0].text = (PlayerStats.Instance.healthPotionS.itemCount + PlayerStats.Instance.healthPotionS.itemStack).ToString();
		InventoryText [1].text = (PlayerStats.Instance.healthPotionM.itemCount + PlayerStats.Instance.healthPotionM.itemStack).ToString();
		InventoryText [2].text = (PlayerStats.Instance.healthPotionL.itemCount + PlayerStats.Instance.healthPotionL.itemStack).ToString();
		InventoryText [3].text = (PlayerStats.Instance.breakBomb.itemCount + PlayerStats.Instance.breakBomb.itemStack).ToString();
		InventoryText [4].text = (PlayerStats.Instance.megaBreakBomb.itemCount + PlayerStats.Instance.megaBreakBomb.itemStack).ToString();
	}
}
