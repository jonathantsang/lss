using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

	// Used to call in the DataController, set by the GameController
	public int id; // 0-5 value for clickers
				   // 6-11 value for upgrades in the shop

	// Self button
	Button btn;

	// Dep
	GameController gameController;
	UpgradeController upgradeController;

	void Awake(){
		GameObject gc = GameObject.FindGameObjectWithTag ("GameController");
		if (gc != null)
			gameController = gc.GetComponent<GameController> ();

		GameObject uc = GameObject.FindGameObjectWithTag ("UpgradeController");
		if (uc != null)
			upgradeController = uc.GetComponent<UpgradeController> ();
	}

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener(attemptBuy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void attemptBuy(){
		// This is used for upgrades and clicker upgrades
		if (id <= 5) {
			bool res = gameController.attemptBuy (id);
			if (res) {
				// Redraw
				gameController.updateUI (id);
			} else {
				// Do nothing, it failed
			}
		} else if (id <= 11) {
			bool res = upgradeController.attemptBuy (id);
			if (res) {
				// Redraw
				upgradeController.updateUI (id);
			} else {
				// Do nothing, it failed
			}
		}


	}
}
