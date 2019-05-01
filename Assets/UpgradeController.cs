using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour {

	GameObject upgrades;
	GameObject stats;
	DataController dataController;

	// Use this for initialization
	void Start () {
		upgrades = GameObject.FindGameObjectWithTag ("Upgrades");
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		stats = GameObject.FindGameObjectWithTag ("Stats");
		setupUpgrades ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setupUpgrades(){
		for (int i = 0; i < upgrades.transform.childCount; i++) {
			updateUpgrade (i);
		}
	}

	// Public UI updates
	public void updateUI(int id = 0){
		updateStatsUI();

		// Update the MoneyMaker themselves
		for (int i = 0; i < upgrades.transform.childCount; i++) {
			updateUpgradeUI (i);
		}
	}

	void updateStatsUI(){
		// Update money count
		if (stats == null) {
			stats = GameObject.FindGameObjectWithTag ("Stats"); // stats is reused in stats page and clicker page, so have to relink
		}
		Text money = stats.transform.GetChild(0).GetComponent<Text>();

		SciNum sn = new SciNum(dataController.getTotalMoney ());
		money.text = "$ " + sn.getNum ();
	}

	void updateUpgradeUI(int id){
		int bought = dataController.getUpgradeBought (id);
		if (bought == 1) {
			Destroy (upgrades.transform.GetChild (id).gameObject);
		}
	}

	void updateUpgrade(int id){
		GameObject upgrade = upgrades.transform.GetChild (id).gameObject;

		Text upgradeTitle = upgrade.transform.GetChild (1).GetComponent<Text> ();
		upgradeTitle.text = dataController.getUpgradeName (id);

		Text upgradeDescription = upgrade.transform.GetChild (2).GetComponent<Text> ();
		upgradeDescription.text = dataController.getUpgradeDescription (id);

		// Cost in Button
		if (dataController.getUpgradeBought (id) == 1) {
			Text upgradeCost = upgrade.transform.GetChild(3).GetChild(0).GetComponent<Text>();
			upgradeCost.text = "BOUGHT";
		} else {
			Text upgradeCost = upgrade.transform.GetChild(3).GetChild(0).GetComponent<Text>();
			upgradeCost.text = new SciNum(dataController.getUpgradePrice (id)).getNum();
		}	 
	}

	// Buy/Sell
	public bool attemptBuy(int id){
		if (id <= 11) {
			// Upgrade
			if (dataController.getUpgradePrice(id) > dataController.getTotalMoney ()) {
				// Invalid
				return false;
			} else if (dataController.getUpgradeBought(id) == 0) {
				// Valid buy
				dataController.decreaseMoney (dataController.getMoneyMakerPrice (id));
				dataController.upgradeMoneyMaker (id);

				dataController.setUpgradeBought (id);

				return true;
			}
		}
		return false;
	}
}
