using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public int id;
	Image greenBar;
	Text timer;

	// Dep
	DataController dataController;
	GameController gameController;

	// Use this for initialization
	void Start () {
		greenBar = transform.GetChild(0).GetComponent<Image> ();
		timer = transform.GetChild (1).GetComponent<Text> ();
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		// Start empty
		greenBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		timer.text = secondsToTimeFormat (dataController.getMoneyMakerWaitTime (id));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	string secondsToTimeFormat(int waitTime){
		string time = "";
		int hours = waitTime / 3600;
		int minutes = (waitTime % 3600) / 60;
		int seconds = (waitTime % 3600 ) % 60;
		if (hours.ToString().Length == 1) {
			time += "0" + hours.ToString();
		}	else {
			time += hours.ToString ();
		}
		time += ":";
		if (minutes.ToString().Length == 1) {
			time += "0" + minutes.ToString();
		}	else {
			time += minutes.ToString ();
		}
		time += ":";
		if (seconds.ToString().Length == 1) {
			time += "0" + seconds.ToString();
		}	else {
			time += seconds.ToString ();
		}
		return time;
	}

	public void animateProgressBar(){
		StartCoroutine (loadProgressAll ());
	}

	IEnumerator loadProgressAll(){
		// Assume from scale 0 to scale 1, small increments based on datacontroller money maker production time
		int waitTime = dataController.getMoneyMakerWaitTime(id);

		WaitForEndOfFrame yieldInstruction = new WaitForEndOfFrame();
		float ticks = 60 * waitTime; // Still finnicky

		for (float k = 0; k < ticks; k++) {
			float val = k / ticks;

			greenBar.transform.localScale = new Vector3(val, 1.0f, 1.0f);

			string time = secondsToTimeFormat(waitTime - (int)k / 60);
			timer.text = time;

			yield return yieldInstruction;

		}
		// At the end put back to default
		greenBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		timer.text = secondsToTimeFormat(dataController.getMoneyMakerWaitTime(id));
	}
}
