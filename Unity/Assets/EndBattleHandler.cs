using UnityEngine;
using System.Collections;

public class EndBattleHandler : MonoBehaviour {

	public BattleUIController UIController;

	public void Awake() {
		UIController = GameObject.Find ("UIController").GetComponent<BattleUIController>();
	}

	public void SendMessage(string msg) {
		if (msg.Equals ("again")) {
			UIController.RestartBattle ();
		}
	}
}
