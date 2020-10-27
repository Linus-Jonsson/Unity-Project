using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRescue : MonoBehaviour {
	private int savedHumans = 0;

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag("Human")) return;
		savedHumans++;

		HumanController controller = other.GetComponent<HumanController>();
		controller.isRescued = true;
		controller.target = transform;

		Destroy(other.gameObject, 5);
	}
}
