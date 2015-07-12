using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndBattleHandler : MonoBehaviour {

	public BattleUIController UIController;
	public Text text;
	public Text buttonText;
	bool won;

	public void Awake() {
		UIController = GameObject.Find ("UIController").GetComponent<BattleUIController>();
		text = transform.FindChild ("Text").GetComponent<Text>();
		buttonText = transform.FindChild ("Button").FindChild("Text").GetComponent<Text>();
	}

	public void SendMessage(string msg) {
		if (msg.Equals ("again")) {
			UIController.RestartBattle (won);
		}
	}

	public void SetWon(bool won) {
		this.won = won;

		if (won) {
			text.text = "You won!";
			buttonText.text = "Next battle";
		} else {
			text.text = "You lost!";
			buttonText.text = "Restart";
		}
	}
}
