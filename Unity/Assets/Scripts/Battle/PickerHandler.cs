using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PickerHandler : MonoBehaviour {

	public Text monName;
	public PickerDelegate pickerDelegate;

	List<Karaimon> monList = DataHolder.mons;
	int currentIndex = 0;
	Karaimon currentMon;

	void Start () {
		if (monList.Count > 0)
			currentMon = monList [0];
		Refresh ();
	}

	void Refresh() {
		Debug.Log ("Nome: " + currentMon.name);
		monName.text = currentMon.name;
	}

	public void SendMessage(string message) {
		if ("ok".Equals(message)) {
			pickerDelegate.MonPicked(monList[currentIndex]);
			return;
		}

		if ("next".Equals (message)) {
			currentIndex++;
			if (currentIndex >= monList.Count) {
				currentIndex = 0;
			}
		} else if ("previous".Equals(message)) {
			currentIndex--;
			if (currentIndex < 0)
				currentIndex = monList.Count - 1;
		}

		currentMon = monList [currentIndex];
		Refresh ();
	}
}

public interface PickerDelegate {
	void MonPicked (Karaimon monPicked);
}
