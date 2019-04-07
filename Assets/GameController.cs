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
			GameObject moneyMaker = moneyMakers.transform.GetChild (i).gameObject;

			// Set each value to zero

			// Count
			Text count = moneyMaker.transform.GetChild(0).GetComponent<Text> ();
			count.text = "0";

			// Load values from Data Controller

			// Upgrade value
			Text upgradeCost = moneyMaker.transform.GetChild(2).transform.GetChild(0).GetComponent<Text> ();
			upgradeCost.text = dataController.getMoneyMakerPrice (i).ToString();

			// Production value
			Text production = moneyMaker.transform.GetChild(3).GetComponent<Text> ();
			production.text = dataController.getMoneyMakerProduction (i).ToString();

		}
	}
}
