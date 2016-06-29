using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Determines if a lightsource is capable of illuminating the player.
 * 
 * If light is able to reach the player, notifies it with necessary info
 * to calculate a rough estimate of how much light is actually shone on
 * the player. AreaLights are not supported.
 */
public class IlluminationWatcher : MonoBehaviour {
	[SerializeField] private IlluminationListener listener;	// Callback object.
	[SerializeField] private List<GameObject> probes; 		// Objects to test against.
	private List<Light> lights = new List<Light>(); 		// Attached ligths.

	// Use this for initialization
	void Start () {
		Light[] lightsInChildren = GetComponentsInChildren<Light> ();
		Light[] lightsInRoot = GetComponents<Light> (); 

		// Add all lights from child elements.
		for (int i = 0; i < lightsInChildren.Length; i++) {
			lights.Add (lightsInChildren [i]);
		}

		// Add all lights within the root element.
		for (int j = 0; j < lightsInRoot.Length; j++) {
			lights.Add (lightsInRoot[j]);
		}
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;	
		Vector3 direction; 	// Raycast direction.
		float distance = 0; 	// Distance from the light origin to probe.
		bool canLightProbe; // Can the light from origin reach the probe directly.

		foreach (Light light in lights) {
			foreach (GameObject probe in probes) {
				canLightProbe = false;

				// Determine which kind of light is in question and
				// evaluate, is the light able to shine on player.
				switch (light.type) {
				case LightType.Directional:
					// TODO Angle affects the luminosity.
					canLightProbe = true;
					distance = Mathf.Infinity;
					break;

				case LightType.Point:
					distance = Vector3.Distance (light.transform.position, probe.transform.position);
					canLightProbe = distance <= light.range;
					break;

				case LightType.Spot:
					distance = Vector3.Distance (light.transform.position, probe.transform.position);
					float angleToProbe = Vector3.Angle ((-light.transform.forward),
						                     light.transform.position - probe.transform.position);
					canLightProbe = (light.spotAngle / 2 >= angleToProbe && distance <= light.range);
					break;
				}

				if (canLightProbe) {
					direction = probe.transform.position - light.transform.position;
					Debug.DrawRay (light.transform.position, direction);
					// Has the ray collided with an object and is of intrest.
					if (Physics.Raycast (light.transform.position, direction, out hit, Mathf.Infinity)
						&& hit.transform.tag.Equals (listener.tag)) {
						// TODO calculate illumination. Distance should reduce illumination exponentially.
						listener.onIlluminated (0);
					}
				}
			}
		}
	}
		
	public abstract class IlluminationListener : MonoBehaviour {

		/**
		 * Notifies the listener of the quantity of light hitting the object.
		 * 
		 * To note is that the abstract class already inherits MonoBehaviour. Thus, implementing
		 * classes shouldn't do that.
		 */
		public abstract void onIlluminated (float illuminance);
	}
}
