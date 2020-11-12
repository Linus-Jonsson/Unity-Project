using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescuesUI : MonoBehaviour {
	TextMeshProUGUI savedHumansText;

	void Start() {
		savedHumansText = GetComponent<TextMeshProUGUI>();
		UpdateDisplay(0);
	}

	private void UpdateDisplay(int savedHumans) {
		savedHumansText.text = "Saved Humans  " + savedHumans.ToString();
	}

	public void AddOneUI(int savedHumans) {
		UpdateDisplay(savedHumans);
	}
}
