using UnityEngine;
using System.Collections;

public class FlikeringLight : MonoBehaviour {
	Light light;
	string waveFunction = "sqr"; // possible values: sin, tri(angle), sqr(square), saw(tooth), inv(verted sawtooth), noise (random)
	float start = 0.0f; // start
	float amplitude = 1.0f; // amplitude of the wave
	float phase = 0.0f; // start point inside on wave cycle
	float frequency = 0.2f; // cycle frequency per second
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

		if (waveFunction=="sin") {
			y = Mathf.Sin(x*2*Mathf.PI);
		}
		else if (waveFunction=="tri") {
			if (x < 0.5)
				y = (float)(4.0 * x - 1.0);
			else
				y = (float)(-4.0 * x + 3.0);  
		}    
		else if (waveFunction=="sqr") {
			if (x < 0.5 + Random.Range(-0.15f, 0.15f))
				y = 1.0f;
			else
				y = -1.0f;  
		}    
		else if (waveFunction=="saw") {
			y = x;
		}    
		else if (waveFunction=="inv") {
			y = (float)(1.0 - x);
		}    
		else if (waveFunction=="noise") {
			y = (float)(1 - (Random.value*2));
		}
		else {
			y = 1.0f;
		}        
		return (y*amplitude)+start;   
	}
}
