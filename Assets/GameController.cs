using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	GameObject moneyMakers;
	GameObject dataController;

	void Awake(){
		moneyMakers = GameObject.FindGameObjectWithTag ("MoneyMakers");
		dataController = GameObject.FindGameObjectWithTag ("DataController");
	}

	// Use this for initialization
	void Start () {
		setupMoneyMakers ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setupMoneyMakers(){
		for (int i = 0; i < moneyMakers.transform.childCount; i++) {
			GameObject moneyMaker = moneyMakers.transform.GetChild (i).gameObject;

			// Set each value to zero

			// Count
			Text count = moneyMaker.transform.GetChild(0).GetComponent<Text> ();
			count.text = "0";
		}
	}
}
