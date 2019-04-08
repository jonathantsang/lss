using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesController : MonoBehaviour {

	GameObject slides;
	// Slides
	GameObject logo;
	GameObject text;
	GameObject disclaimer;

	// Use this for initialization
	void Start () {
		slides = GameObject.FindGameObjectWithTag ("Slides");
		logo = slides.transform.GetChild (0).gameObject;
		disclaimer = slides.transform.GetChild (1).gameObject;

		// Slides all false
		logo.SetActive (false);
		disclaimer.SetActive (false);

		StartCoroutine (showLogo ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Chained coroutines
	IEnumerator showLogo()
	{
		logo.SetActive (true);
		print ("logo");
		yield return new WaitForSeconds(3);
		StartCoroutine (showDisclaimer ());
		logo.SetActive (false);

	}

	IEnumerator showDisclaimer()
	{
		disclaimer.SetActive (true);
		print ("disclaimer");
		yield return new WaitForSeconds(3);
		print(Time.time);
		disclaimer.SetActive (false);
	}
}
