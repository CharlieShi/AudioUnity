using UnityEngine;
using System.Collections;

public class KeyPressed : MonoBehaviour {

	Material mat;
	Color orignalColor;
	Vector3 pos;
	void Start() {
		mat = GetComponent<MeshRenderer> ().material;
		if (mat.color.Equals(Color.white)) {
			orignalColor = Color.white;
		} else {
			orignalColor = Color.black;
		}
		pos = transform.position;
	}

	public void press() {
		mat.color = Color.yellow;
		pos.z -= 0.5f;
		transform.position = pos;
	}

	public void release() {
		mat.color = orignalColor;
		pos.z += 0.5f;
		transform.position = pos;
	}
}
