using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HubLogic : MonoBehaviour {

	public int openedSlot;
	public bool isItemDrawerOpen = false;
	public bool isSlotOpen = false;
	public GameObject[] itemDrawerSlots;
	public Item[] itemDrawerSlots_item;
	public Item[] items;

	public void goToMapSelect() {
		SceneManager.LoadScene ("WorldMap");
	}

	public void goToTown() {
		SceneManager.LoadScene ("Town");
	}

	void Awake() {
		PlayerStats.Instance.Load ();
		GameObject.Find ("ItemCapacityText").GetComponent<Text> ().text = "Item Capacity       " + PlayerStats.Instance.numberOfItemsEquipped + " / " + PlayerStats.Instance.itemCapacity;
		if (PlayerStats.Instance.itemSlot1.itemStack == 0) {
			PlayerStats.Instance.itemSlot1 = items [0];
			GameObject.Find ("Item_1").GetComponent<Image> ().sprite = PlayerStats.Instance.emptyItem.itemSprite;
			transform.Find ("Item_1/Count").GetComponent<Text> ().text = "";
		}
		if (PlayerStats.Instance.itemSlot1 != items [0]) {
			GameObject.Find ("Item_1").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot1.itemSprite;
			transform.Find ("Item_1/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot1.itemStack.ToString ();
		}
		if (PlayerStats.Instance.itemSlot2.itemStack == 0) {
			PlayerStats.Instance.itemSlot2 = items [0];
			GameObject.Find ("Item_2").GetComponent<Image> ().sprite = PlayerStats.Instance.emptyItem.itemSprite;
			transform.Find ("Item_2/Count").GetComponent<Text> ().text = "";
		}
		if (PlayerStats.Instance.itemSlot2 != items [0]) {
			GameObject.Find ("Item_2").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot2.itemSprite;
			transform.Find ("Item_2/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot2.itemStack.ToString ();
		}
	}

	void Update() {
		transform.Find ("Money").GetComponent<Text> ().text = "Money - " + PlayerStats.Instance.money;
	}

	public void resetSave_Bridge() {
		PlayerStats.Instance.resetSave ();
	}

	public void openItemSlot(int slotNumber) {
		checkItemSelectionBox ();
		if (!isSlotOpen) {
			openedSlot = slotNumber;
			isSlotOpen = true;
			GameObject.Find ("ItemSelection").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("ItemSelection_Box" + openedSlot).GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			if (slotNumber == openedSlot) {
				isSlotOpen = false;
				GameObject.Find ("ItemSelection").GetComponent<Canvas> ().enabled = false;
				GameObject.Find ("ItemSelection_Box" + openedSlot).GetComponent<SpriteRenderer> ().enabled = false;
			}
			else {
				openedSlot = slotNumber;
				isSlotOpen = true;
				GameObject.Find ("ItemSelection").GetComponent<Canvas> ().enabled = true;
				GameObject.Find ("ItemSelection_Box" + openedSlot).GetComponent<SpriteRenderer> ().enabled = true;
				if (openedSlot == 1) 
					GameObject.Find ("ItemSelection_Box2").GetComponent<SpriteRenderer> ().enabled = false;
				else
					GameObject.Find ("ItemSelection_Box1").GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}

	public void addItemToSlot(int slotNumber) {
		if (itemDrawerSlots_item [slotNumber].itemCount > 0) {
			if (openedSlot == 1 && itemDrawerSlots_item [slotNumber] != PlayerStats.Instance.itemSlot2) {
				if (PlayerStats.Instance.itemSlot1 == items [0] && PlayerStats.Instance.itemCapacity > PlayerStats.Instance.numberOfItemsEquipped) {
					PlayerStats.Instance.itemSlot1 = itemDrawerSlots_item [slotNumber];
					PlayerStats.Instance.itemSlot1.itemStack++;
					PlayerStats.Instance.itemSlot1.itemCount--;
					PlayerStats.Instance.numberOfItemsEquipped++;
				} else if (PlayerStats.Instance.itemSlot1 == itemDrawerSlots_item [slotNumber] && PlayerStats.Instance.itemCapacity > PlayerStats.Instance.numberOfItemsEquipped) {
					PlayerStats.Instance.itemSlot1.itemStack++;
					PlayerStats.Instance.itemSlot1.itemCount--;
					PlayerStats.Instance.numberOfItemsEquipped++;
				} else if (PlayerStats.Instance.itemSlot1 != itemDrawerSlots_item [slotNumber] && PlayerStats.Instance.itemSlot1 != items [0]) {
					PlayerStats.Instance.itemSlot1.itemCount += PlayerStats.Instance.itemSlot1.itemStack;
					PlayerStats.Instance.itemSlot1.itemStack = 0;
					PlayerStats.Instance.itemSlot1 = itemDrawerSlots_item [slotNumber];
					PlayerStats.Instance.itemSlot1.itemStack++;
					PlayerStats.Instance.itemSlot1.itemCount--;
				}
				transform.Find ("Item_1").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot1.itemSprite;
				transform.Find ("Item_1/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot1.itemStack.ToString ();
			}
			if (openedSlot == 2 && itemDrawerSlots_item [slotNumber] != PlayerStats.Instance.itemSlot1) {
				if (PlayerStats.Instance.itemSlot2 == items [0] && PlayerStats.Instance.itemCapacity > PlayerStats.Instance.numberOfItemsEquipped) {
					PlayerStats.Instance.itemSlot2 = itemDrawerSlots_item [slotNumber];
					PlayerStats.Instance.itemSlot2.itemStack++;
					PlayerStats.Instance.itemSlot2.itemCount--;
					PlayerStats.Instance.numberOfItemsEquipped++;
				} else if (PlayerStats.Instance.itemSlot2 == itemDrawerSlots_item [slotNumber] && PlayerStats.Instance.itemCapacity > PlayerStats.Instance.numberOfItemsEquipped) {
					PlayerStats.Instance.itemSlot2.itemStack++;
					PlayerStats.Instance.itemSlot2.itemCount--;
					PlayerStats.Instance.numberOfItemsEquipped++;
				} else if (PlayerStats.Instance.itemSlot2 != itemDrawerSlots_item [slotNumber] && PlayerStats.Instance.itemSlot2 != items [0]) {
					PlayerStats.Instance.itemSlot2.itemCount += PlayerStats.Instance.itemSlot2.itemStack;
					PlayerStats.Instance.itemSlot2.itemStack = 0;
					PlayerStats.Instance.itemSlot2 = itemDrawerSlots_item [slotNumber];
					PlayerStats.Instance.itemSlot2.itemStack++;
					PlayerStats.Instance.itemSlot2.itemCount--;
				}
				transform.Find ("Item_2").GetComponent<Image> ().sprite = PlayerStats.Instance.itemSlot2.itemSprite;
				transform.Find ("Item_2/Count").GetComponent<Text> ().text = PlayerStats.Instance.itemSlot2.itemStack.ToString ();
			}
			checkItemSelectionBox ();
			PlayerStats.Instance.Save ();
		}
	}

	public void removeItemFromSlot(int slotNumber) {
		if (openedSlot == 1) {
			PlayerStats.Instance.itemSlot1.itemCount += PlayerStats.Instance.itemSlot1.itemStack;
			PlayerStats.Instance.numberOfItemsEquipped -= PlayerStats.Instance.itemSlot1.itemStack;
			PlayerStats.Instance.itemSlot1.itemStack = 0;
			PlayerStats.Instance.itemSlot1 = items [0];
			transform.Find ("Item_1").GetComponent<Image> ().sprite = items [0].itemSprite;
			transform.Find ("Item_1/Count").GetComponent<Text> ().text = "";
		}
		if (openedSlot == 2) {
			PlayerStats.Instance.itemSlot2.itemCount += PlayerStats.Instance.itemSlot2.itemStack;
			PlayerStats.Instance.numberOfItemsEquipped -= PlayerStats.Instance.itemSlot2.itemStack;
			PlayerStats.Instance.itemSlot2.itemStack = 0;
			PlayerStats.Instance.itemSlot2 = items [0];
			transform.Find ("Item_2").GetComponent<Image> ().sprite = items [0].itemSprite;
			transform.Find ("Item_2/Count").GetComponent<Text> ().text = "";
		}
		checkItemSelectionBox ();
		PlayerStats.Instance.Save ();
	}

	void checkItemSelectionBox() {
		int j = 1;
		int i = 1;
		while (i < 9) {
			while (j < items.Length) {
				if (items [j].itemCount > 0 || items[j].itemStack > 0) {
					itemDrawerSlots_item [i] = items [j];
					itemDrawerSlots [i].GetComponent<Image> ().enabled = true;
					itemDrawerSlots [i].GetComponent<Image> ().sprite = items [j].itemSprite;
					itemDrawerSlots_item [i].itemCode = items [j].itemCode;
					itemDrawerSlots_item [i].itemCount = items [j].itemCount;
					itemDrawerSlots [i].GetComponentInChildren<Text> ().text = itemDrawerSlots_item [i].itemCount.ToString();
					j++;
					i++;
					break;
				}
				j++;
			
			}
			if (j == items.Length)
				i = 9;
		}
		GameObject.Find ("ItemCapacityText").GetComponent<Text> ().text = "Item Capacity       " + PlayerStats.Instance.numberOfItemsEquipped + " / " + PlayerStats.Instance.itemCapacity;
	}

	public void closeItemBox () {
		if (isSlotOpen) {
			isSlotOpen = false;
			GameObject.Find ("ItemSelection").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("ItemSelection_Box" + openedSlot).GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	public void openCloseItemDrawer() {
		if (!isItemDrawerOpen) {
			GetComponent<Animator> ().Play ("Open");
			isItemDrawerOpen = true;
		} else {
			closeItemBox ();
			GetComponent<Animator> ().Play ("Close");
			isItemDrawerOpen = false;
		}
	}
}
