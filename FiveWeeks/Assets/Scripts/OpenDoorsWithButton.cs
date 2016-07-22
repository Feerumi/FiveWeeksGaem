using UnityEngine;
using System.Collections;

public class OpenDoorsWithButton : MonoBehaviour {

	public GameObject negativeDoor;
	public GameObject positiveDoor;
	public float openTime;
	public float openLength;
	private float timeToOpen;
	private float translateAmount;
	private float translateChunk;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Open");
	}

	IEnumerator Close() {

		timeToOpen = openTime;
		translateAmount = openLength;
		translateChunk = translateAmount / timeToOpen;

		while(translateAmount > 0f){
			negativeDoor.transform.Translate (new Vector3 (translateChunk, 0, 0) * Time.deltaTime);
			positiveDoor.transform.Translate (new Vector3 (-translateChunk, 0, 0) * Time.deltaTime);
			translateAmount -= translateChunk * Time.deltaTime;
			timeToOpen -= Time.deltaTime;
			yield return null;
		}

		timeToOpen = 0f;

		negativeDoor.transform.Translate (translateAmount, 0, 0);
		positiveDoor.transform.Translate (-translateAmount, 0, 0);
	}

	IEnumerator Open() {

		timeToOpen = openTime;
		translateAmount = openLength;
		translateChunk = translateAmount / timeToOpen;

		while(translateAmount > 0f){
			negativeDoor.transform.Translate (new Vector3 (-translateChunk, 0, 0) * Time.deltaTime);
			positiveDoor.transform.Translate (new Vector3 (translateChunk, 0, 0) * Time.deltaTime);
			translateAmount -= translateChunk * Time.deltaTime;
			timeToOpen -= Time.deltaTime;
			yield return null;
		}

		timeToOpen = 0f;

		negativeDoor.transform.Translate (-translateAmount, 0, 0);
		positiveDoor.transform.Translate (translateAmount, 0, 0);
	}
}
