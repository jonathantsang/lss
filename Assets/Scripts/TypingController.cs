using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingController : MonoBehaviour {

	public GameObject typedBox;
	Text typed;

	// Use this for initialization
	void Start () {
		typed = typedBox.GetComponent<Text> ();
		typed.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			typed.text += Input.inputString;
		}
	}
}
