using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRescue : MonoBehaviour {
	private int savedHumans = 0;

	private void OnTriggerEnter2D(Collider2D other) {
		string otherTag = other.tag;

		if (otherTag != "Human") return;
		savedHumans++;

		HumanController controller = other.GetComponent<HumanController>();
		controller.target = transform;

		Destroy(other.gameObject, 5);
	}
}
