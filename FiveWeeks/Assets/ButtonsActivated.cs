using UnityEngine;
using System.Collections;

public class ButtonsActivated : MonoBehaviour {

	bool button1pressed = false;
	bool button2pressed = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (button1pressed && button2pressed) {
			//Open doors
		}
	}

	public void buttonPressed(GameObject button) {
		if (button.name == "button1") {
			button1pressed = true;
		} else if (button.name == "button2") {
			button2pressed = true;
		}
			
	}
}
