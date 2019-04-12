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
		StartCoroutine (loadProgressBar());
		StartCoroutine (loadTimer ());
	}

	IEnumerator loadProgressBar(){
		// Assume from scale 0 to scale 1, small increments based on datacontroller money maker production time
		int waitTime = dataController.getMoneyMakerWaitTime(id);
		float incrementPerFrame = (1.0f / (float)waitTime) / 60.0f;

		while (greenBar.transform.localScale.x <= 1) {
			float val = (float) (greenBar.transform.localScale.x + incrementPerFrame);
			greenBar.transform.localScale = new Vector3(val, 1.0f, 1.0f);
			yield return new WaitForSeconds(0.01f);
		}
		// At the end reset the progress bar
		greenBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
	}

	IEnumerator loadTimer(){
		// Assume from scale 0 to scale 1, small increments based on datacontroller money maker production time
		int waitTime = dataController.getMoneyMakerWaitTime(id);

		while (waitTime >= 0) {
			// Convert waitTime in seconds to 00:00:00 -> HH:MM:SS format
			string time = secondsToTimeFormat(waitTime);
			waitTime -= 1;
			timer.text = time;
			yield return new WaitForSeconds(1.0f);
		}
		// At the end put back to default
		timer.text = secondsToTimeFormat(dataController.getMoneyMakerWaitTime(id));
	}
}
