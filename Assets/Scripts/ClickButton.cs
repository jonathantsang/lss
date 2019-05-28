using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {

	private readonly object atomicLock = new object();
	bool busy = false;

	// Used to call in the DataController, set by the GameController
	public int id; // 0-5 value

	// Self button
	Button btn;

	// Dep
	GameController gameController;
	DataController dataController;
	ProgressBar progressBar;

	void Awake(){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		progressBar = gameObject.transform.parent.GetChild (4).GetComponent<ProgressBar> ();
	}

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (addMoney);
	}

	// Update is called once per frame
	void Update () {

	}

	void addMoney(){
		// print ("addMoney");
		// Need a lock mechanism so only one coroutine can be running at a time
		// RACE CONDITION
		if (dataController.getMoneyMakerMutex (id) == false && busy == false) {
			if (busy == true) {
				throw new System.Exception("race condition");
			}
			lock (atomicLock) {
				busy = true;
			}

			dataController.setMoneyMakerMutex (id, true);
			dataController.increaseTotalClicks (); // +1
			StartCoroutine (addDelayedMoney ());
		} else {
			// print ("mutex needed");
		}

	}

	IEnumerator addDelayedMoney(){
		int waitTime = dataController.getMoneyMakerWaitTime (id);
		// Animation in ProgressBar
		progressBar.animateProgressBar();

		yield return new WaitForSecondsRealtime(waitTime);
		//yield return new WaitForSeconds(0);

		// The update UI delay seems awkward
		dataController.increaseMoney (dataController.getMoneyMakerProduction (id));
		// print ("added delayed money");
		gameController.updateUI ();
		dataController.setMoneyMakerMutex (id, false);

		busy = false; // Allow next barger
	}
}
