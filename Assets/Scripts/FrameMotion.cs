using UnityEngine;
using System.Collections;

public class FrameMotion : MonoBehaviour {
	public float spinSpeedPerSec;
	float spinAngle;
	void Start () {

	}
	

	void Update () {
		spinAngle = spinSpeedPerSec * Time.deltaTime * 100;
		transform.Rotate(new Vector3(0, 0, spinAngle / 360 * Mathf.PI));

	}
}
