using UnityEngine;
using System.Collections;

	public class Spectrum : MonoBehaviour {
	public AudioSource asource;
	public int channel;
	float[] spectrum; 
	float[] volume;
	public int numSamples;
	public GameObject enem;

	GameObject[] bars;
	// Use this for initialization
	void Start () {
		volume = new float[numSamples];
		spectrum = new float[numSamples];
		bars = new GameObject[numSamples];
		for (int i = 0; i < numSamples; i++)
		{
			GameObject enemyClone = Instantiate(enem,new Vector3(i * 256f / numSamples,0,0), transform.rotation) as GameObject;

			bars[i] = enemyClone;
		}
	}
	// Update is called once per frame
	void Update () {
		asource.GetOutputData(volume, channel);
    	asource.GetSpectrumData(spectrum, channel, FFTWindow.Rectangular);
	
		for (int i = 0; i < numSamples; i++) {
			bars[i].transform.localScale= new Vector3(256f / numSamples,10000*spectrum[i],1);
			bars[i].GetComponent<MeshRenderer>().material.color= new Color(1000*spectrum[i],1f/(20*spectrum[i]),1f/(20*spectrum[i]),1);
		}
	}
	

}
