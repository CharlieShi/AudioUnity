using UnityEngine;
using System.Collections;

public class PitchCalculator : MonoBehaviour {

	public float[] keyFrequencies = new float[88]; //88 keys on piano
	public int[] keyFreqSpectrumIndices = new int[88];

	GameObject[] keys = new GameObject[88];
	int sampleSize;
	float fMax;
	// Use this for initialization
	void Start () {
		sampleSize = GameObject.Find ("SpriteGenerator").GetComponent<SpriteGenController> ().sampleSize;
		fMax = AudioSettings.outputSampleRate/2;
		for (int i = 0; i < 88; i++) {
			keyFrequencies[i] = Mathf.Pow(2f, (i - 48) / 12f) * 440;
			keyFreqSpectrumIndices[i] = getSpectrumIndex(keyFrequencies[i]);
			Debug.Log("i " + Mathf.FloorToInt(keyFrequencies[i]) + " " + keyFreqSpectrumIndices[i]);
		}
		for (int i = 0; i < 88; i++) {
			keys[i] = GameObject.Find("Key" + (i + 1));
		}
	}

	int getSpectrumIndex(float frequency) {
		return (int)Mathf.Floor(frequency * sampleSize / fMax);
	}
}
