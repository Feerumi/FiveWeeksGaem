using UnityEngine;
using System.Collections;

public class PlayerVisibility : IlluminationWatcher.IlluminationListener {

	private bool mIsVisible;
	private float mIlluminationTotal;

	[SerializeField] private float visibilityTreshold;



	// Use this for initialization
	void Start () {
		mIsVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		mIsVisible = mIlluminationTotal >= visibilityTreshold;
		mIlluminationTotal = 0;
	}

	override public void onIlluminated (float illuminance) {
		mIlluminationTotal += illuminance;
	}

	public bool isVisible() {
		return mIsVisible;
	}
}
