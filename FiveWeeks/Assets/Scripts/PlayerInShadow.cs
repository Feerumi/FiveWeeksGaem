using UnityEngine;
using System.Collections;

public class PlayerInShadow : IlluminationWatcher.IlluminationListener {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onIlluminated (float illuminance) {
		Debug.Log ("Illuminance: " + illuminance);
	}
}
