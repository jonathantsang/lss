using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour {

	Button btn;

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (destroyGameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// This is called at the button, want to destroy the closeable window parent
	void destroyGameObject(){
		Destroy (gameObject.transform.parent.gameObject);
	}
}
