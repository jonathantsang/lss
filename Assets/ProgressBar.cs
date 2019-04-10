using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	Image greenBar;

	// Use this for initialization
	void Start () {
		greenBar = GetComponent<Image> ();

	}
	
	// Update is called once per frame
	void Update () {
		float val = (float) ((greenBar.transform.localScale.x + 0.1) % 1);
		greenBar.transform.localScale = new Vector3(val, 1.0f, 1.0f);
	}
}
