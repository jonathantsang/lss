﻿using System.Collections;
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

	void Awake(){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
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
		bool res = gameController.attemptBuy (id);
		if (res) {
			// Redraw
			gameController.updateUI (id);
		} else {
			// Do nothing, it failed
		}
	}
}
