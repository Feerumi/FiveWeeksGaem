using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerVisibility : IlluminationWatcher.IlluminationListener {

	enum Action {
		SATURATE, DESATURATE, IDLE
	}

	private bool mIsVisible = false;
	private float min = 0.1f;
	private float max = 1f;
	private float duration = 0.35f;
	private float elapsed = 0f;
	private Action action = Action.IDLE;

	private bool IsVisible {
		get {
			return mIsVisible;
		}

		set {
			if (value != mIsVisible) {
				mIsVisible = value;
				elapsed = 0;
				action = (IsVisible) ? Action.SATURATE : Action.DESATURATE;
			}
		}
	}

	private float mIlluminationTotal;

	[SerializeField] private float visibilityTreshold;
	[SerializeField] private Camera camera;
	private ColorCorrectionCurves curve;




	// Use this for initialization
	void Start () {
		curve = camera.GetComponent<ColorCorrectionCurves> ();
		IsVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
		IsVisible = mIlluminationTotal >= visibilityTreshold;
		mIlluminationTotal = 0;

		switch (action) {

		case Action.SATURATE:
			curve.saturation = Mathf.Lerp (curve.saturation, max, elapsed);
			if (elapsed <= 1) {
				elapsed += Time.deltaTime / duration;
			} else {
				action = Action.IDLE;
			}
			break;

		case Action.DESATURATE:
			curve.saturation = Mathf.Lerp (curve.saturation, min, elapsed);
			if (elapsed <= 1) {
				elapsed += Time.deltaTime / duration;
			} else {
				action = Action.IDLE;
			}
			break;
		}
	}

	override public void onIlluminated (float illuminance) {
		mIlluminationTotal += illuminance;
	}

	public bool isVisible() {
		return mIsVisible;
	}
}
