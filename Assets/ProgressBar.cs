using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public int id;
	Image greenBar;

	// Dep
	DataController dataController;

	// Use this for initialization
	void Start () {
		greenBar = GetComponent<Image> ();
		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();

		// Start empty
		greenBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void animateProgressBar(){
		StartCoroutine (loadProgressBar());
	}

	IEnumerator loadProgressBar(){
		// Assume from scale 0 to scale 1, small increments based on datacontroller money maker production time
		int waitTime = dataController.getMoneyMakerWaitTime(id);
		float incrementPerFrame = (1.0f / (float)waitTime) / 60.0f;

		print (waitTime);
		print (incrementPerFrame);

		while (greenBar.transform.localScale.x < 1) {
			float val = (float) (greenBar.transform.localScale.x + incrementPerFrame);
			greenBar.transform.localScale = new Vector3(val, 1.0f, 1.0f);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
