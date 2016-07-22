using UnityEngine;
using System.Collections;

public class FlikeringLight : MonoBehaviour {

	enum WaveFunction {
		SIN, TRIANGE, SQUARE, SAWTOOTH, INVERTED_SAWTOOTH, NOISE
	}

	Light light;
	[SerializeField] private WaveFunction waveFunction;
	[SerializeField] private float start; // start
	[SerializeField] private float amplitude; // amplitude of the wave
	[SerializeField] private float phase; // start point inside on wave cycle
	[SerializeField] private float frequency; // cycle frequency per second
	[SerializeField] private float randomness;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		originalColor = light.color;
	}
	
	// Update is called once per frame
	void Update () {
		light.color = originalColor * (EvalWave());
	}

	public float EvalWave () {
		float x = (Time.time + phase)*frequency;
		float y = 0;

		x = x - Mathf.Floor(x); // normalized value (0..1)

		switch (waveFunction) {
		case WaveFunction.SIN:
			y = Mathf.Sin(x*2*Mathf.PI);
			break;

		case WaveFunction.TRIANGE:
			y = (float)((x < 0.5) ? (4.0 * x - 1.0): (-4.0 * x + 3.0));
			break;

		case WaveFunction.SQUARE:
			y = (float)((x < 0.5 + Random.Range (-randomness, randomness)) ? 1.0f : -1.0f);
			break;

		case WaveFunction.SAWTOOTH:
			y = x;
			break;

		case WaveFunction.INVERTED_SAWTOOTH:
			y = (float)(1.0 - x);
			break;

		case WaveFunction.NOISE:
			y = (float)(1 - (Random.value*2));
			break;

		default:
			y = 1.0f;
			break;
		}

		light.enabled = (y != -1);

		return (y*amplitude)+start;   
	}
}
