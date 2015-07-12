using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class BattleController : MonoBehaviour, PickerDelegate {

	public BattleUIController uiController;
	public static List<Karaimon> monList = new List<Karaimon>();

	Karaimon player;
	Karaimon enemy;

	bool playerDefending = false;
	bool enemyDefending = false;
	public bool isEnemyTurn = false;

	List<Message> messageQueue = new List<Message>();

	bool shouldRunEnemyTurn = false;
	bool shouldRunPlayerTurn = false;
	public bool shouldEndBattle = false;
	bool playerWon = false;

	int battleCount = 0;

	public void Restart(bool won) {
		playerDefending = false;
		enemyDefending = false;
		isEnemyTurn = false;
		messageQueue.Clear ();
		shouldRunEnemyTurn = false;
		shouldRunPlayerTurn = false;
		shouldEndBattle = false;
		uiController.ClearLog ();
		enemy = null;

		if (won) {
			player.resetMon();
			((PickerDelegate)this).MonPicked (player);
		} else {
			battleCount = 0;
			uiController.loadPicker();
		}
	}

	void Start () {
		loadElements ();
		loadMons ();
	}

	void loadElements() {
		Element.SetElements (MonSQLite.Instance.GetElements ());
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		messageQueue.Add(new Message(mon.name + " picked! He has " + mon.attributes.currentTou + " toughness!", IEnums.MessageType.Generic));

		player = mon.Clone();
		enemy = monList[new System.Random ().Next (0, monList.Count - 1)].Clone();
		messageQueue.Add (new Message(enemy.name + " is his enemy! He has " + enemy.attributes.currentTou + " toughness!", IEnums.MessageType.Generic));

		ShowMessages ();

		refreshInfo ();
		preparePlayerTurn ();
	}

	void refreshInfo() {
		refreshAlive ();
		refreshHealth ();
		refreshNames ();
	}

	void ShowMessages() {
		StartCoroutine ("ShowMessagesRoutine");
	}

	IEnumerator ShowMessagesRoutine() {
		List<Message> messageTemp = new List<Message>();
		messageTemp.AddRange (messageQueue);
		messageQueue.Clear ();

		foreach (Message message in messageTemp) {
			uiController.addLog(message.text);

			switch (message.type) {
			case IEnums.MessageType.LostHP:
				refreshHealth();
				break;
			case IEnums.MessageType.Fainted:
				shouldEndBattle = !refreshAlive();
				break;
			case IEnums.MessageType.Generic:
				break;
			}

			yield return new WaitForSeconds(0.5f);
		}

		AfterShowMessage ();
	}

	void loadMons() {
		monList = MonSQLite.Instance.GetKaraimon ();
		uiController.loadPicker ();
	}

	public void playerAttacked(int attackId) {
		attack (player, enemy, attackId);
	}

	public void AfterShowMessage() {
		if (shouldEndBattle) {
			EndBattle();
			return;
		}

		if (shouldRunEnemyTurn)
			runEnemyTurn ();
		else if (shouldRunPlayerTurn)
			preparePlayerTurn ();
	}

	void EndBattle() {
		shouldEndBattle = false;
		shouldRunEnemyTurn = false;
		shouldRunPlayerTurn = false;

		if (playerWon) {
			int experience = 50; // dynamic later
			bool leveled = player.AddExperience(experience);
			messageQueue.Add(new Message(player.name + " gained " +
			                             experience + " experience!", IEnums.MessageType.Generic));

			if (leveled) {
				messageQueue.Add(new Message(player.name + " is now level " +
				                             player.attributes.level + "!", IEnums.MessageType.Generic));
			}

			ShowMessages();
			refreshNames();
		}

		uiController.EndBattle(playerWon);
	}

	public void playerDefended() {
		playerDefending = true;
		messageQueue.Add (new Message(player.name + " defended!", IEnums.MessageType.Generic));

		shouldRunEnemyTurn = true;
		ShowMessages ();
	}

	void attack(Karaimon attacker, Karaimon defender, int attack) {
		MonAttack attackObj;
		switch (attack) {
		case 2:
			attackObj = attacker.attacks.getAttack2();
			break;
		case 3:
			attackObj = attacker.attacks.getAttack3();
			break;
		case 4:
			attackObj = attacker.attacks.getAttack4();
			break;
		default:
			attackObj = attacker.attacks.getAttack1();
			break;
		}

		if (attackObj.usesLeft <= 0) {
			messageQueue.Add(new Message(attackObj.name + " can't be used anymore!", IEnums.MessageType.Generic));
			ShowMessages();
			return;
		}

		float damage = AttackController.resolveAttack(attackObj, attacker, defender,
		                               defender == player ? playerDefending : enemyDefending);
		int effectiveness = AttackController.GetEffectiveness (attackObj, defender);

		messageQueue.Add (new Message(string.Format("{0} used {1}! ({2} uses left)",
		                              attacker.name, attackObj.name, attackObj.usesLeft),
		                              IEnums.MessageType.Generic));

		if (effectiveness == 1) {
			messageQueue.Add (new Message ("It's super effective!", IEnums.MessageType.Generic));
		} else if (effectiveness == -1) {
			messageQueue.Add (new Message ("It's not very effective...", IEnums.MessageType.Generic));
		}

		messageQueue.Add (new Message(defender.name + " lost " + damage + " of health!", IEnums.MessageType.LostHP));

		if (!defender.isAlive()) {
			playerWon = defender != player;
			messageQueue.Add (new Message(defender.name + " fainted!", IEnums.MessageType.Fainted));
		}

		if (attacker == player) {
			shouldRunEnemyTurn = true;
			isEnemyTurn = true;
		} else {
			shouldRunPlayerTurn = true;
			isEnemyTurn = false;
		}

		ShowMessages();
	}

	void runEnemyTurn() {
		shouldRunEnemyTurn = false;
		if (!enemy.isAlive ())
			return;

		enemyDefending = false;

		StartCoroutine ("enemyTurn");
	}

	public void enemyReturned(bool defended, int attackNumber) {
		shouldRunPlayerTurn = true;

		if (defended) {
			messageQueue.Add (new Message(enemy.name + " defended!", IEnums.MessageType.Generic));
			ShowMessages();
		} else {
			attack (enemy, player, attackNumber);
		}
	}

	void preparePlayerTurn() {
		shouldRunPlayerTurn = false;
		playerDefending = false;
		isEnemyTurn = false;
	}

	void refreshNames() {
		uiController.playerMonName.text = player.name;
		uiController.playerMonLevel.text = "Level: " + player.attributes.level;
		uiController.playerMonExp.text = "XP: " + player.attributes.experience;

		uiController.enemyMonName.text = enemy.name;
		uiController.enemyMonLevel.text = "Level: " + enemy.attributes.level;
	}

	void refreshHealth() {
		uiController.playerMonLife.text = "Health: " + player.attributes.currentLife.ToString();
		uiController.enemyMonLife.text = "Health: " + enemy.attributes.currentLife.ToString();
	}

	bool refreshAlive() {
		if (!player.isAlive ()) {
			uiController.playerMonName.text = player.name + " fainted!";
			return false;
		} else if (!enemy.isAlive ()) {
			uiController.enemyMonName.text = enemy.name + " fainted!";
			return false;
		}

		return false;
	}

	IEnumerator enemyTurn() {
		bool defended = false;
		int attackNumber = -1;

		yield return new WaitForSeconds(1);

		System.Random r = new System.Random ();
		bool willAttack = r.Next (1, 10) > 8 ? false : true;
		if (willAttack) {
			attackNumber = r.Next (1, enemy.attacks.Count());
		} else {
			defended = true;
		}

		enemyReturned (defended, attackNumber);
	}
}
