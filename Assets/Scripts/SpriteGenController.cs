using UnityEngine;
using System.Collections;

public class SpriteGenController : MonoBehaviour {

	public int sampleSize = 1024;
	public float threshold = 0.5f;
	public float lowFreqBound;
	public GameObject spritePrefab;

	GameObject spriteParentNode;
	GameObject[] keys = new GameObject[88];
	AudioListener listener;
	float cameraSize;
	float[] spectrum;
	float fMax;
	int lowFreqIndex;
	int[] keyFreqSpectrumIndices;
	string last, curr;
	int lastIndex, currIndex;
	// Use this for initialization
	void Start () {
		listener = GameObject.Find ("Main Camera").GetComponent<AudioListener> ();
		cameraSize = GameObject.Find ("Main Camera").GetComponent<Camera> ().orthographicSize;
		spriteParentNode = GameObject.Find ("SpriteMotionFrame");
		keyFreqSpectrumIndices = GameObject.Find ("PitchCalculator").GetComponent<PitchCalculator> ().keyFreqSpectrumIndices;
		spectrum = new float[sampleSize];
		Random.seed = (int)Time.time;
		Random.Range (0, 1);
		fMax = AudioSettings.outputSampleRate/2;
		lowFreqIndex = getSpectrumIndex (lowFreqBound);
		last = "";
		curr = "";
		lastIndex = 0;
		for (int i = 0; i < 88; i++) {
			keys[i] = GameObject.Find("Key" + (i + 1));
		}
	}

	void Update () {


		AudioListener.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);

		lastIndex = currIndex;
		currIndex = findMaxVolumnPitchIndex ();
		last = curr;
		curr = convKeyIndexToPitchName (currIndex);
		if (last != null && !last.Equals (curr)) {
			Debug.Log (curr);
			Debug.Log ("Key index: " + currIndex);
			if (keys[currIndex] != null) {
				keys [currIndex].GetComponent<KeyPressed>().press();
			}
			if (keys[lastIndex] != null) {
				keys [lastIndex].GetComponent<KeyPressed> ().release ();
			}
		}

	}

	int findMaxVolumnPitchIndex() {
		float max = 0;
		int maxIndex = 0;
		for (int i = 0; i < keyFreqSpectrumIndices.Length; i++)
		{
			if (max < spectrum[keyFreqSpectrumIndices[i]]) {
				max = spectrum[keyFreqSpectrumIndices[i]];
				maxIndex = i;
			}
		}
		return maxIndex;
	}

	string convKeyIndexToPitchName(int index) {
		string result;
		index += 1;

		if (index <= 3) {
			return convTwelvePitchToString(index + 9);
		}
		return convTwelvePitchToString ((index - 3) % 12);
	}

	string convTwelvePitchToString(int indexInTwelve) {
		switch (indexInTwelve) {
		case 1:	return "C";
		case 2: return "#C";
		case 3: return "D";
		case 4:	return "#D";
		case 5: return "E";
		case 6: return "F";
		case 7:	return "#F";
		case 8: return "G";
		case 9: return "#G";
		case 10: return "A";
		case 11: return "#A";
		case 12: return "B";
		}
		return "";

	}

	void genNewSprite() {
		GameObject newSprite = Instantiate(spritePrefab);
		Vector3 pos = newSprite.transform.position;
		pos = new Vector3(Random.value * 2 * cameraSize - cameraSize,
		                  Random.value * 2 * cameraSize - cameraSize, 0);
		newSprite.transform.position = pos;
		newSprite.transform.SetParent (spriteParentNode.transform);
	}
	
	int getSpectrumIndex(float frequency) {
			return (int)Mathf.Floor(frequency * sampleSize / fMax);
	}

	float getRMSValue(int size) {
		float[] samples = new float[size];
		AudioListener.GetOutputData (samples, 0);
		float sum = 0;
		for (int i = 0; i < size; i++) {
			sum += Mathf.Pow(samples[i], 2f);
		}
		return Mathf.Sqrt (sum / size);
	}
	
	void getFFTSpectrum(int size) {
		float[] spectrum = new float[size];
		AudioListener.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
		int i = 1;
		while (i < size - 1) {
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
			i++;
		}

	}

}
