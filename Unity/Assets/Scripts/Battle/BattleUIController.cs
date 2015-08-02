using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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

	public BattleMaster controller;

	GameObject picker;
	GameObject endBattle;

	List<Message> messageQueue = new List<Message>();

	PickerDelegate pickerDelegate;

	/* New, better and stronger */
	IBattleResponder responder;

	public void SetBattleResponder(IBattleResponder responder)
	{
		this.responder = responder;
	}

	public void SendMessage(string msg)
	{
		if (!responder.CanAttack())
			return;

		int move = 0;
		if (msg.StartsWith("move"))
			move = int.Parse(msg.Substring(4, 1)) - 1;

		responder.ChoosePlayerMove(move);
	}

	public void ClearLog()
	{
		log.text = "";
		messageQueue.Clear ();
	}

	public void RestartBattle(bool won)
	{
		Destroy (endBattle);
		controller.RestartBattle(won);
	}

	public void LoadPicker(PickerDelegate pDelegate)
	{
		picker = Instantiate (pickerObject);
		picker.GetComponent<PickerHandler> ().pickerDelegate = this;
		picker.transform.SetParent (canvas.transform);
		picker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		pickerDelegate = pDelegate;
	}

	public void EndBattle(bool won)
	{
		endBattle = Instantiate (endBattlePrefab);
		endBattle.transform.SetParent (canvas.transform);
		endBattle.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		endBattle.GetComponent<EndBattleHandler>().SetWon(won);
	}

	void PickerDelegate.MonPicked(Karaimon mon)
	{
		Destroy (picker);
		pickerDelegate.MonPicked (mon);
	}

	public void RefreshMoves(PlayerMon mon)
	{
		attack1.gameObject.SetActive(false);
		attack2.gameObject.SetActive(false);
		attack3.gameObject.SetActive(false);
		attack4.gameObject.SetActive(false);

		for (int i = 0; i < mon.moves.Count(); i++) {
			MonMove attack = mon.moves[i];

			if (attack == null)
				continue;

			Button button = null;
			switch (i) {
			case 0:
				button = attack1;
				break;
			case 1:
				button = attack2;
				break;
			case 2:
				button = attack3;
				break;
			case 3:
				button = attack4;
				break;
			}

			button.gameObject.SetActive(true);
			button.GetComponentInChildren<Text>().text = attack.name;
		}
	}

	public void AddLog(string text)
	{
		log.text = string.Format ("{0}\n{1}", log.text, text);
	}

	public void AddMessage(Message message)
	{
		messageQueue.Add (message);
	}

	public void ShowMessages()
	{
		StartCoroutine ("ShowMessagesRoutine");
	}
	
	IEnumerator ShowMessagesRoutine()
	{
		List<Message> messageTemp = new List<Message>();
		messageTemp.AddRange (messageQueue);
		messageQueue.Clear ();
		
		foreach (Message message in messageTemp) {
			AddLog(message.text);
			
			switch (message.type) {
			case IEnums.MessageType.LostHP:
				controller.RefreshHealth();
				break;
			case IEnums.MessageType.Fainted:
				controller.shouldEndBattle = !controller.RefreshAlive();
				break;
			case IEnums.MessageType.Generic:
				break;
			}
			
			yield return new WaitForSeconds(0.5f);
		}
		
		AfterShowMessage ();
	}

	public void AfterShowMessage()
	{
		if (controller.shouldEndBattle) {
			controller.EndBattle();
			return;
		}
		
		controller.AfterShowMessage ();
	}
}
