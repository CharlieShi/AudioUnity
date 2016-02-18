using UnityEngine;
using System.Collections;

public class MonoSpriteController : MonoBehaviour {

	public int fadeInFrameCount = 100;
	int count = 0;
	bool finished = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (finished) {
			return;
		}
		count = Time.frameCount;
		Color col = GetComponent<SpriteRenderer> ().color;
		col.a = count * 1f / fadeInFrameCount;
		GetComponent<SpriteRenderer> ().color = col;
		if (count >= fadeInFrameCount) {
			finished = true;
		}
	}
}
