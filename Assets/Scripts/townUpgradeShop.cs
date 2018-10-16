using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class townUpgradeShop : MonoBehaviour {

	public int healthUpgradeCost;
	public Text healthUpgradeButtonText;
	public Image healthUpgradeProgressBar;

	public int itemCapacityUpgradeCost;
	public Text itemCapacityUpgradeButtonText;
	public Image itemCapacityUpgradeProgressBar;

	public int ShieldRechargeUpgradeCost;
	public Text ShieldRechargeUpgradeButtonText;
	public Image ShieldRechargeUpgradeProgressBar;

	void Start() {
		healthUpgradeCost = (int) Mathf.Pow(2, PlayerStats.Instance.maxHealth / 10) * 100;
		healthUpgradeButtonText.text = " Upgrade - " + healthUpgradeCost;
		itemCapacityUpgradeCost = (int) Mathf.Pow(2, PlayerStats.Instance.itemCapacity / 1) * 100;
		itemCapacityUpgradeButtonText.text = " Upgrade - " + itemCapacityUpgradeCost;
		checkProgressBars ();
	}

	public void upgradeHealth() {
		if (PlayerStats.Instance.maxHealth < 100 && PlayerStats.Instance.money >= healthUpgradeCost) {
			PlayerStats.Instance.money -= healthUpgradeCost;
			PlayerStats.Instance.maxHealth += 10;
			healthUpgradeCost = 2 * healthUpgradeCost;
			healthUpgradeButtonText.text = " Upgrade - " + healthUpgradeCost;
			checkProgressBars ();
		}
	}

	public void upgradeItemCapacity() {
		if (PlayerStats.Instance.itemCapacity < 10 && PlayerStats.Instance.money >= itemCapacityUpgradeCost) {
			PlayerStats.Instance.money -= itemCapacityUpgradeCost;
			PlayerStats.Instance.itemCapacity += 1;
			itemCapacityUpgradeCost = 2 * itemCapacityUpgradeCost;
			itemCapacityUpgradeButtonText.text = " Upgrade - " + itemCapacityUpgradeCost;
			checkProgressBars ();
		}
	}


	public void upgradeShieldRecharge() {
		if (PlayerStats.Instance.shieldRechargeSpeed >= 11 && PlayerStats.Instance.money >= ShieldRechargeUpgradeCost) {
			PlayerStats.Instance.money -= ShieldRechargeUpgradeCost;
			PlayerStats.Instance.shieldRechargeSpeed -= 3;
			ShieldRechargeUpgradeCost = 2 * ShieldRechargeUpgradeCost;
			ShieldRechargeUpgradeButtonText.text = " Upgrade - " + ShieldRechargeUpgradeCost;
			checkProgressBars ();
		}
	}


	void checkProgressBars() {
		healthUpgradeProgressBar.fillAmount = (float) (PlayerStats.Instance.maxHealth) / 100;
		itemCapacityUpgradeProgressBar.fillAmount = (float) (PlayerStats.Instance.itemCapacity) / 10;
		ShieldRechargeUpgradeProgressBar.fillAmount = (float) (23 - PlayerStats.Instance.shieldRechargeSpeed) / 15f;
	}
}
