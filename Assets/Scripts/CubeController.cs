using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {


	public int sampleSize = 8192;
	AudioListener listener;
	// Use this for initialization
	void Start () {
		listener = GameObject.Find ("Main Camera").GetComponent<AudioListener> ();
	}
	
	// Update is called once per frame
	void Update () {

		float[] samples = new float[sampleSize];
		AudioListener.GetOutputData (samples, 0);
		float sum = 0;
		for (int i = 0; i < sampleSize; i++) {
			sum += Mathf.Pow(samples[i], 2f);
		}
		float rmsValue = Mathf.Sqrt (sum / sampleSize);
		Vector3 scale = transform.localScale;
		scale.y = 1 + rmsValue * 10;
		transform.localScale = scale;
	}
}
