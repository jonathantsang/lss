using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	GameObject moneyMakers;
	DataController dataController;

	void Awake(){
		moneyMakers = GameObject.FindGameObjectWithTag ("MoneyMakers");
	}

	// Use this for initialization
	void Start () {
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		setupMoneyMakers ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setupMoneyMakers(){
		for (int i = 0; i < moneyMakers.transform.childCount; i++) {
			updateMoneyMakerUI (i);

		}
	}

	// Public UI updates
	// takes an id right now, could take nothing and update all
	public void updateMoneyMakerUI(int id){
		GameObject moneyMaker = moneyMakers.transform.GetChild (id).gameObject;

		// Set id at each UpgradeButton so it knows who it is
		UpgradeButton ub = moneyMaker.transform.GetChild(2).GetComponent<UpgradeButton> ();
		ub.id = id;

		// Set each value to zero

		// Count
		Text count = moneyMaker.transform.GetChild(0).GetComponent<Text> ();
		count.text = "0";

		// Load values from Data Controller

		// Upgrade value
		Text upgradeCost = moneyMaker.transform.GetChild(2).transform.GetChild(0).GetComponent<Text> ();
		upgradeCost.text = dataController.getMoneyMakerPrice (id).ToString();

		// Production value
		Text production = moneyMaker.transform.GetChild(3).GetComponent<Text> ();
		production.text = dataController.getMoneyMakerProduction (id).ToString ();
	}

	// Buy/Sell
	public bool attemptBuy(int id){
		if (dataController.getMoneyMakerPrice (id) > dataController.getTotalMoney ()) {
			// Invalid
			return false;
		} else {
			// Valid buy
			dataController.decreaseMoney (dataController.getMoneyMakerPrice (id));
			dataController.upgradeMoneyMaker (id);
			return true;
		}
	}
}
