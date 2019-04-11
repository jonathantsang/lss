using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCover : MonoBehaviour {

	public int id; // 0-5 for the money makers

	GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
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

	void OnMouseDown(){
		print ("click cover " + id.ToString ());
		// Send to Data Controller
		attemptBuy();
	}
}