﻿using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

	public GameObject pickedUpItemPosition;
	GameObject nearbyItem;
	bool itemInRange = false;
	bool isItemPickedUp = false;
	public float mThrowForce;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
        //Pick up item
		if (Input.GetMouseButtonDown (0) && itemInRange && !isItemPickedUp ) {
			if (nearbyItem != null) {
				Debug.Log ("Picked Up");

				isItemPickedUp = true;
				nearbyItem.GetComponent<Rigidbody> ().isKinematic = true;

                // Move picked up item to pickupitem location.
				nearbyItem.transform.position = pickedUpItemPosition.transform.position;

                // Set pickupitem position as parent for picked up item.
				nearbyItem.transform.SetParent (pickedUpItemPosition.transform);
			}
        //Throw picked up item.
		} else if(isItemPickedUp && Input.GetMouseButtonDown (0)) {
			Debug.Log ("Shoot");

            // Make item non kinemati and set it non-trigger.
			nearbyItem.GetComponent<Rigidbody> ().isKinematic = false;

            // Remove parent anchor from picked up item.
			nearbyItem.transform.SetParent (null);

			nearbyItem.GetComponent<Rigidbody> ().AddForce (pickedUpItemPosition.transform.forward * mThrowForce);
			isItemPickedUp = false;
			itemInRange = false;
		}
	
			
	}

	void OnTriggerEnter(Collider coll) {

        // Check if collider is pickup item.
		if (coll.gameObject.tag == "Pickup") {
			Debug.Log ("Item in range");
			itemInRange = true;
			nearbyItem = coll.gameObject;

			// TODO non static layer referene.
			nearbyItem.layer = LayerMask.NameToLayer("Highlight");
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Pickup") {
			itemInRange = false;

			// TODO non static layer referene.
			nearbyItem.layer = LayerMask.NameToLayer("Default");
		}
	}
}
