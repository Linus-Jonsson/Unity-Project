using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRescue : MonoBehaviour {
	[SerializeField] private GameObject chopper;
	[SerializeField] private int maxRescueSpots = 10;
	[SerializeField] private int rescuedHumansGoal = 3;

	private bool incomingChopper = false;
	private GameObject[] escortedHumans;
	private int rescuedHumans;
	private RescuesUI rescuesUI;

	private void Start() {
		escortedHumans = new GameObject[maxRescueSpots];
		rescuedHumans = 0;
		rescuesUI = GameObject.FindGameObjectWithTag("RescuesUI").GetComponent<RescuesUI>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag("Human"))
			return;

		if (!AssignHumanToChopper(other.gameObject))
			return;

		HumanController controller = other.GetComponent<HumanController>();
		controller.isRescued = true;

		if (controller.target.CompareTag("Waypoint"))
			Destroy(controller.target.gameObject);

		controller.target = transform;

		if (!incomingChopper)
			CallChopper();
	}

	private bool AssignHumanToChopper(GameObject human) {
		for (int i = 0; i < escortedHumans.Length; i++) {
			if (escortedHumans[i] != null)
				continue;

			escortedHumans[i] = human;
			return true;
		}

		return false;
	}

	private void CallChopper() {
		incomingChopper = true;

		GameObject calledChopper = Instantiate(chopper, transform.position, Quaternion.identity);
		calledChopper.GetComponent<ChopperController>().rescuePoint = transform;
	}

	public void RescueHumans() {
		for (int i = 0; i < escortedHumans.Length; i++) {
			if (!escortedHumans[i])
				continue;

			Destroy(escortedHumans[i]);
			escortedHumans[i] = null;
			rescuedHumans++;
			rescuesUI.AddOneUI(rescuedHumans);
            if (rescuedHumans >= rescuedHumansGoal)
            {
				FindObjectOfType<LevelController>().HandleWinCondition();
            }
		}
		incomingChopper = false;
	}
}
