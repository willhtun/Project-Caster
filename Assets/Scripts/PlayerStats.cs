using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public static PlayerStats Instance;

	void Awake() {
		if (!Instance) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else if (Instance != null)
			Destroy (this);
	}

	//----------------------------------------------------------------------------------------------

	public int money = 0; // Gold
	public int maxHealth;
	public int itemCapacity;
	public int numberOfItemsEquipped;
	public float shieldRechargeSpeed;

	public Item emptyItem;
	public Item healthPotionS;
	public Item healthPotionM;
	public Item healthPotionL;
	public Item breakBomb;
	public Item megaBreakBomb;

	public Item itemSlot1;
	public Item itemSlot2;

	//----------------------------------------------------------------------------------------------

	public void Save() {
		PlayerPrefs.SetInt ("Money", money);
		PlayerPrefs.SetInt ("MaxHealth", maxHealth);
		PlayerPrefs.SetInt ("ItemCapacity", itemCapacity);
		PlayerPrefs.SetInt ("NumberOfItemsEquipped", numberOfItemsEquipped);
		PlayerPrefs.SetFloat ("ShieldRechargeSpeed", shieldRechargeSpeed);

		PlayerPrefs.SetInt ("HealthPotionS", healthPotionS.itemCount);
		PlayerPrefs.SetInt ("HealthPotionS_S", healthPotionS.itemStack);

		PlayerPrefs.SetInt ("HealthPotionM", healthPotionM.itemCount);
		PlayerPrefs.SetInt ("HealthPotionM_S", healthPotionM.itemStack);

		PlayerPrefs.SetInt ("HealthPotionL", healthPotionL.itemCount);
		PlayerPrefs.SetInt ("HealthPotionL_S", healthPotionL.itemStack);

		PlayerPrefs.SetInt ("BreakBomb", breakBomb.itemCount);
		PlayerPrefs.SetInt ("BreakBomb_S", breakBomb.itemStack);

		PlayerPrefs.SetInt ("MegaBreakBomb", megaBreakBomb.itemCount);
		PlayerPrefs.SetInt ("MegaBreakBomb_S", megaBreakBomb.itemStack);

		PlayerPrefs.SetString ("ItemSlot1", itemSlot1.itemCode);
		PlayerPrefs.SetString ("ItemSlot2", itemSlot2.itemCode);
	}

	public void Load() {

		if (!PlayerPrefs.HasKey ("Money"))
			resetSave ();

		money = PlayerPrefs.GetInt ("Money");
		maxHealth = PlayerPrefs.GetInt ("MaxHealth");
		itemCapacity = PlayerPrefs.GetInt ("ItemCapacity");
		numberOfItemsEquipped = PlayerPrefs.GetInt ("NumberOfItemsEquipped");
		shieldRechargeSpeed = PlayerPrefs.GetFloat ("ShieldRechargeSpeed");

		healthPotionS.itemCount = PlayerPrefs.GetInt ("HealthPotionS");
		healthPotionS.itemStack = PlayerPrefs.GetInt ("HealthPotionS_S");

		healthPotionM.itemCount = PlayerPrefs.GetInt ("HealthPotionM");
		healthPotionM.itemStack = PlayerPrefs.GetInt ("HealthPotionM_S");

		healthPotionL.itemCount = PlayerPrefs.GetInt ("HealthPotionL");
		healthPotionL.itemStack = PlayerPrefs.GetInt ("HealthPotionL_S");

		breakBomb.itemCount = PlayerPrefs.GetInt ("BreakBomb");
		breakBomb.itemStack = PlayerPrefs.GetInt ("BreakBomb_S");

		megaBreakBomb.itemCount = PlayerPrefs.GetInt ("MegaBreakBomb");
		megaBreakBomb.itemStack = PlayerPrefs.GetInt ("MegaBreakBomb_S");


		Item temp1 = null, temp2 = null;
		switch (PlayerPrefs.GetString ("ItemSlot1")) {
		case "H1":
			temp1 = healthPotionS;
			break;
		case "H2":
			temp1 = healthPotionM;
			break;
		case "H3":
			temp1 = healthPotionL;
			break;
		case "B1":
			temp1 = breakBomb;
			break;
		case "B2":
			temp1 = megaBreakBomb;
			break;
		default:
			temp1 = emptyItem;
			break;
		}
		switch (PlayerPrefs.GetString ("ItemSlot2")) {
		case "H1":
			temp2 = healthPotionS;
			break;
		case "H2":
			temp2 = healthPotionM;
			break;
		case "H3":
			temp2 = healthPotionL;
			break;
		case "B1":
			temp2 = breakBomb;
			break;
		case "B2":
			temp2 = megaBreakBomb;
			break;
		default:
			temp2 = emptyItem;
			break;
		}

		itemSlot1 = temp1;
		itemSlot2 = temp2;

	}

	public void resetSave() {
		PlayerPrefs.SetInt ("Money", 9999999);
		PlayerPrefs.SetInt ("MaxHealth", 10);
		PlayerPrefs.SetInt ("ItemCapacity", 1);
		PlayerPrefs.SetInt ("NumberOfItemsEquipped", 0);
		PlayerPrefs.SetFloat ("ShieldRechargeSpeed", 20f);
		PlayerPrefs.SetInt ("HealthPotionS", 0);
		PlayerPrefs.SetInt ("HealthPotionS_S", 0);
		PlayerPrefs.SetInt ("HealthPotionM", 0);
		PlayerPrefs.SetInt ("HealthPotionM_S", 0);
		PlayerPrefs.SetInt ("HealthPotionL", 0);
		PlayerPrefs.SetInt ("HealthPotionL_S", 0);
		PlayerPrefs.SetInt ("BreakBomb", 0);
		PlayerPrefs.SetInt ("BreakBomb_S", 0);
		PlayerPrefs.SetInt ("MegaBreakBomb", 0);
		PlayerPrefs.SetInt ("MegaBreakBomb_S", 0);
		PlayerPrefs.SetString ("ItemSlot1", "");
		PlayerPrefs.SetString ("ItemSlot2", "");
		Load ();
	}
}
