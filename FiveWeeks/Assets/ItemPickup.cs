using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

	public GameObject pickedUpItemPosition;
	GameObject nearbyItem;
	bool itemInRange = false;
	bool isItemPickedUp = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0) && itemInRange && !isItemPickedUp ) {
			if (nearbyItem != null) {
				Debug.Log ("Picked Up");
				isItemPickedUp = true;
				nearbyItem.GetComponent<Rigidbody> ().isKinematic = true;
				nearbyItem.GetComponent<BoxCollider>().isTrigger = false;
				nearbyItem.transform.position = pickedUpItemPosition.transform.position;
				nearbyItem.transform.SetParent (pickedUpItemPosition.transform);
			}
		}
	
			
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "Pickup" && !isItemPickedUp) {
			Debug.Log ("Item in range");
			itemInRange = true;
			nearbyItem = coll.gameObject;
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.tag == "Pickup" && !isItemPickedUp) {
			Debug.Log ("No item in range");
			nearbyItem = null;
			itemInRange = false;
		}

	}
}
