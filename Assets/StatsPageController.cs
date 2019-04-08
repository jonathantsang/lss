using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPageController : MonoBehaviour {

	GameObject stats;
	DataController dataController;

	// Use this for initialization
	void Start () {
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		stats = GameObject.FindGameObjectWithTag ("Stats");
		Text money = stats.transform.GetChild (0).GetComponent<Text> ();
		money.text = "Total Money: " + dataController.getTotalMoney ().ToString ();
	
		Text clicks = stats.transform.GetChild (1).GetComponent<Text> ();
		clicks.text = "Total Clicks: " + dataController.getTotalClicks ().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
