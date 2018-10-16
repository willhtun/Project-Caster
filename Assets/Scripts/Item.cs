using UnityEngine;

[CreateAssetMenu]
public class Item: ScriptableObject
{
	public Sprite itemSprite;
	public string itemCode;
	public char itemType;
	public int itemCount;
	public int itemStack;

	public int healAmount;
	public int breakSize;

	public void use() {
		if (itemStack > 0) {
			switch (itemType) {
			case 'H':
				if ( true ) { // GameObject.Find ("Player").GetComponent<PlayerLogic> ().currentHealth != PlayerStats.Instance.maxHealth) {
					GameObject.Find ("Player").GetComponent<PlayerLogic> ().addHealth (healAmount);
					Debug.Log ("use H " + healAmount);
				} 
				break;
			case 'B':
				if (GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().inBattleMode) {
					if (breakSize == 1)
						GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().breakEnemy ();
					else {
						for (int e = 0; e < GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesSpawnedInFight; e++) {
							if (GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().enemiesInFight [e] != null)
								GameObject.Find ("Dashboard").GetComponent<DashboardLogic> ().breakEnemy ();
						}
					}
				}
				break;
			default:
				break;
			}
			PlayerStats.Instance.numberOfItemsEquipped--;
			itemStack--;
		}
	}
}