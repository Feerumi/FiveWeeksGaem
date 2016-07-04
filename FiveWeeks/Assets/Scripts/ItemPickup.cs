using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

	public GameObject pickedUpItemPosition;
	GameObject nearbyItem;
	bool itemInRange = false;
	bool isItemPickedUp = false;
	public float verticalThrowSpeed;
	public float horizontalThrowSpeed;

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
		} else if(isItemPickedUp && Input.GetMouseButtonDown (0)) {
			Debug.Log ("Shoot");
			nearbyItem.GetComponent<Rigidbody> ().isKinematic = false;
			nearbyItem.GetComponent<BoxCollider>().isTrigger = false;
			nearbyItem.transform.SetParent (null);
			nearbyItem.GetComponent<Rigidbody> ().AddForce (pickedUpItemPosition.transform.forward * horizontalThrowSpeed);
			nearbyItem.GetComponent<Rigidbody> ().AddForce (pickedUpItemPosition.transform.up * verticalThrowSpeed);
			isItemPickedUp = false;
			itemInRange = false;

		}
	
			
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Pickup") {
			Debug.Log ("Item in range");
			itemInRange = true;
			nearbyItem = coll.gameObject;
		}
	}

	void OnTriggerExit(Collider coll) {
		itemInRange = false;
	}
}
