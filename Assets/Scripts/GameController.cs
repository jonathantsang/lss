using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	GameObject moneyMakers;
	GameObject stats;
	DataController dataController;

	public GameObject closeableWindow;

	void Awake(){
		print ("awake gc");
		// This is called at each scene change (not persistent, and destroyed at scene change)

		moneyMakers = GameObject.FindGameObjectWithTag ("MoneyMakers");
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();

		// Only on game page
		setupMoneyMakers ();
		updateUI ();
	}

	// Use this for initialization
	void Start () {
		print ("start gc");

		// Wait for load of inventory?

		// create popup about idle
		// createPopUp();
	}
	
	// Update is called once per frame
	void Update () {
		// Safety?, probably update after certain times?
		// updateUI();
	}

	// Used to set up the values in the money makers and linked the respective upgrades
	void setupMoneyMakers(){
		for (int i = 0; i < moneyMakers.transform.childCount; i++) {
			updateMoneyMakerUI (i);
		}
	}

	// Public UI updates
	public void updateUI(int id = 0){
		// Updates MoneyMaker and Stats
		updateStatsUI();

		// Update the MoneyMaker themselves
		for (int i = 0; i < moneyMakers.transform.childCount; i++) {
			updateMoneyMakerUI (i);
		}
	}

	void updateStatsUI(){
		// Update money count
		if (stats == null) {
			stats = GameObject.FindGameObjectWithTag ("Stats"); // stats is reused in stats page and clicker page, so have to relink
		}
		Text money = stats.transform.GetChild(0).GetComponent<Text>();

		SciNum sn = new SciNum(dataController.getMoney ());
		money.text = "$ " + sn.getNum ();
	}

	// takes an id right now, could take nothing and update all
	void updateMoneyMakerUI(int id){
		GameObject moneyMaker = moneyMakers.transform.GetChild (id).gameObject;

		// Turn on the LockedCover if the level is 0
		if (dataController.getMoneyMakerLevel (id) == 0) {
			moneyMaker.transform.GetChild (5).gameObject.SetActive (true);
			// Set the LockedCover text price
			Text unlockPrice = moneyMaker.transform.GetChild(5).transform.GetChild(0).GetComponent<Text> ();
			unlockPrice.text = "$ " + dataController.getMoneyMakerPrice (id).ToString();
		} else {
			moneyMaker.transform.GetChild (5).gameObject.SetActive (false);
		}

		// Set id at each ClickButton so it knows who it is
		ClickButton cb = moneyMaker.transform.GetChild(1).GetComponent<ClickButton> ();
		cb.id = id;

		// Set id at each UpgradeButton so it knows who it is
		UpgradeButton ub = moneyMaker.transform.GetChild(2).GetComponent<UpgradeButton> ();
		ub.id = id;

		// Set id at each LoadBar so it knows who it is
		ProgressBar pb = moneyMaker.transform.GetChild(4).GetComponent<ProgressBar> ();
		pb.id = id;

		// Set id at each LockedCover so it knows who it is
		LockedCover lc = moneyMaker.transform.GetChild(5).GetComponent<LockedCover> ();
		lc.id = id;

		// Set each value to zero

		// Count
		Text count = moneyMaker.transform.GetChild(0).GetComponent<Text> ();
		count.text = dataController.getMoneyMakerLevel (id).ToString ();

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
		if (id <= 5) {
			// money maker click upgrade
			if (dataController.getMoneyMakerPrice (id) > dataController.getMoney ()) {
				// Invalid
				return false;
			} else {
				// Valid buy
				dataController.decreaseMoney (dataController.getMoneyMakerPrice (id));
				dataController.upgradeMoneyMaker (id);
				return true;
			}
		} else if (id <= 11) {
			// Upgrade

		}
		return false;
	}

	// Idle PopUp
	public void createPopUp(){
		GameObject window = Instantiate(closeableWindow, new Vector3(0,0,0), Quaternion.identity);
		// TODO Fix index not uniform
		long res = new DateTime(dataController.getTimeQuit()).ToFileTime();
		long now = System.DateTime.Now.ToFileTime ();

		System.TimeSpan diff = new TimeSpan (now - res);

		string timeFormatted = String.Format ("{0}:{1}:{2}", 
								   diff.Hours, 
			                       diff.Minutes,
			                       diff.Seconds);

		window.transform.GetChild (0).GetComponent<Text> ().text = "You earned $1 after waiting " + timeFormatted;

		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		window.transform.SetParent (canvas.transform);
		// Not sure why scale goes to != 1
		window.transform.localScale = new Vector3(1,1,1);
	}
}
