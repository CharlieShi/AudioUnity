using UnityEngine;
using System.Collections;

public class PianoController : MonoBehaviour {

	public GameObject whiteKey;
	public GameObject blackKey;
	// Use this for initialization
	void Start () {
		generateKey ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateKey() {
		bool[] isWhiteKey = {true, false, true, false, true, true, false, true, false, true, false, true};

		for (int i = 0; i < 88; i++) {
			int index = (i + 9) % 12;
			GameObject key;
			if (isWhiteKey[index]) {
				key = Instantiate(whiteKey);
			} else {
				key = Instantiate(blackKey);
			}
			Vector3 pos = key.transform.position;
			pos.x = i * 0.5f;
			key.transform.position = pos;
			key.name = "Key" + (i + 1);
		}
	}
}
