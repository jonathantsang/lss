using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour {

	public GameObject shownBox;
	Text shown;

	// Use this for initialization
	void Start () {
		shown = shownBox.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
