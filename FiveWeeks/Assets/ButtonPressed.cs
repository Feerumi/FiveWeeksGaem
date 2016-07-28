using UnityEngine;
using System.Collections;

public class ButtonPressed : MonoBehaviour {

	private Animator animator;
	private OpenDoorsWithButton bA;
	private GameObject currentButton;
	public GameObject button;
	public GameObject scriptHook;


	void Awake() {
		animator = button.GetComponent<Animator> ();
		bA = scriptHook.GetComponent<OpenDoorsWithButton> ();
		currentButton = this.gameObject;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "PickUpArea") {
			Debug.Log ("Button pressed");
			animator.SetTrigger ("Pressed");
			bA.ButtonPressed(currentButton);
		}
	}
}
