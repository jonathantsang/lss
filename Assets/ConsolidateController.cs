using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsolidateController : MonoBehaviour {

	public GameObject ConsolidateText;

	private GameObject stats;
	private DataController dataController;

	// Use this for initialization
	void Start () {
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();

		updateStatsUI ();

		setupConsolidateText ();
	}
	
	// Update is called once per frame
	void Update () {
		
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

	void setupConsolidateText(){
		Transform consolidateTextTransform = ConsolidateText.transform;
		// Message, Gain, Total

		// Gain
		Text Gain = consolidateTextTransform.GetChild(1).GetComponent<Text>();

		Gain.text = "You will gain: " + "WORK IN PROGRESS";

		// Total
		Text Total = consolidateTextTransform.GetChild(2).GetComponent<Text>();

		Total.text = "You currently have: " + dataController.getExclusivesCount().ToString();

		// Worth
		Text Worth = consolidateTextTransform.GetChild(3).GetComponent<Text>();

		Worth.text = "They are worth:\n" + ((float) dataController.getExclusivesWorth() / 1000.0f).ToString() + "%";
	}
}
