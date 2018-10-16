using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour {

	public int currentHealth;
	public Text healthBarText;

	void Start() {
		currentHealth = PlayerStats.Instance.maxHealth;
		healthBarTextChecker ();
	}

	public void subtractHealth (int damageTaken) {
		if (!GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().shielded) {
			currentHealth -= damageTaken;
			GameObject.Find ("HealthBar").GetComponent<Image> ().fillAmount = getHealthInDecimal ();
			if (currentHealth <= 0) {
				print ("Game Over");
				PlayerStats.Instance.Save ();
				SceneManager.LoadScene ("Hub");
			}
		}
		healthBarTextChecker ();
	}

	public void addHealth (int healAmount) {
		currentHealth += healAmount;
		GameObject.Find ("HealthBar").GetComponent<Image> ().fillAmount = getHealthInDecimal();
		if (currentHealth >= PlayerStats.Instance.maxHealth) {
			currentHealth = PlayerStats.Instance.maxHealth;
		}
	}

	float getHealthInDecimal() {
		return (float)currentHealth / PlayerStats.Instance.maxHealth;
	}

	void healthBarTextChecker () {
		healthBarText.text = currentHealth + " / " + PlayerStats.Instance.maxHealth;
	}
}

