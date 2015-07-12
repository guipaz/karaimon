using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour, PickerDelegate {
	public Canvas canvas;

	public GameObject pickerObject;
	public GameObject endBattlePrefab;

	public Text playerMonName;
	public Text playerMonLife;
	public Text playerMonLevel;
	public Text playerMonExp;

	public Text enemyMonName;
	public Text enemyMonLife;
	public Text enemyMonLevel;

	public Button attack1;
	public Button attack2;
	public Button attack3;
	public Button attack4;

	public Text log;

	public BattleController controller;

	GameObject picker;
	GameObject endBattle;

	public void SendMessage(string msg) {
		if (controller.isEnemyTurn || controller.shouldEndBattle) {
			return;
		}

		if (msg.Equals ("defend")) {
			controller.playerDefended();
			return;
		}

		int attack = 0;
		if (msg.Equals ("attack1")) {
			attack = 1;
		} else if (msg.Equals ("attack2")) {
			attack = 2;
		} else if (msg.Equals ("attack3")) {
			attack = 3;
		} else if (msg.Equals ("attack4")) {
			attack = 4;
		}

		if (attack != 0)
			controller.playerAttacked(attack);
	}

	public void ClearLog() {
		log.text = "";
	}

	public void RestartBattle(bool won) {
		Destroy (endBattle);
		controller.Restart(won);
	}

	public void loadPicker() {
		picker = Instantiate (pickerObject);
		picker.GetComponent<KaraimonPickerController> ().pickerDelegate = this;
		picker.transform.SetParent (canvas.transform);
		picker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
	}

	public void EndBattle(bool won) {
		endBattle = Instantiate (endBattlePrefab);
		endBattle.transform.SetParent (canvas.transform);
		endBattle.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		endBattle.GetComponent<EndBattleHandler>().SetWon(won);
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		MonAttack attack = mon.attacks.getAttack1 ();
		if (attack != null) {
			attack1.gameObject.SetActive(true);
			attack1.GetComponentInChildren<Text>().text = attack.name;
		} else {
			attack1.gameObject.SetActive(false);
		}

		attack = mon.attacks.getAttack2 ();
		if (attack != null) {
			attack2.gameObject.SetActive(true);
			attack2.GetComponentInChildren<Text>().text = attack.name;
		} else {
			attack2.gameObject.SetActive(false);
		}

		attack = mon.attacks.getAttack3 ();
		if (attack != null) {
			attack3.gameObject.SetActive(true);
			attack3.GetComponentInChildren<Text>().text = attack.name;
		} else {
			attack3.gameObject.SetActive(false);
		}

		attack = mon.attacks.getAttack4 ();
		if (attack != null) {
			attack4.gameObject.SetActive(true);
			attack4.GetComponentInChildren<Text>().text = attack.name;
		} else {
			attack4.gameObject.SetActive(false);
		}

		Destroy (picker);
		((PickerDelegate)controller).MonPicked (mon);
	}

	public void addLog(string text) {
		log.text = string.Format ("{0}\n{1}", log.text, text);
	}
}
