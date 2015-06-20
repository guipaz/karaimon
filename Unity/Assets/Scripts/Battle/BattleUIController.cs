using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour, PickerDelegate {
	public Canvas canvas;

	public GameObject pickerObject;

	public Text playerMonName;
	public Text playerMonLife;

	public Text enemyMonName;
	public Text enemyMonLife;

	public BattleController controller;

	GameObject picker;

	public void SendMessage(string msg) {
		if (msg.Equals ("attack")) {
			controller.playerAttacked();
		} else {
			controller.playerDefended();
		}
	}

	public void loadPicker() {
		picker = Instantiate (pickerObject);
		picker.GetComponent<KaraimonPickerController> ().pickerDelegate = this;
		picker.transform.SetParent (canvas.transform);
		picker.GetComponent<RectTransform> ().offsetMin = new Vector2 (50, 50);
		picker.GetComponent<RectTransform> ().offsetMax = new Vector2 (-50, -50);
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		Destroy (picker);
		((PickerDelegate)controller).MonPicked (mon);
	}
}
