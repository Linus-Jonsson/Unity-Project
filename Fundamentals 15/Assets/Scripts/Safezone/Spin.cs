using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
	[SerializeField] private int spinSpeed = 20;

	// Update is called once per frame
	void Update() {
		transform.Rotate(Vector3.forward, spinSpeed);
	}
}
