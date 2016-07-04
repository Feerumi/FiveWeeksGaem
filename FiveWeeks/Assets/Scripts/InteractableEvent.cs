using UnityEngine;
using System.Collections;

public class InteractableEvent : MonoBehaviour {


	bool usableItemInRange = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (usableItemInRange && Input.GetKeyDown (KeyCode.E)) {
			this.gameObject.transform.localScale = new Vector3 (2, 2, 2);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "Usable") {
			Debug.Log ("Usable item in range");
			usableItemInRange = true;
		}
	}

	void OnTriggerExit(Collider coll) {
		usableItemInRange = false;
	}
}
