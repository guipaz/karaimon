using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattleMaster : MonoBehaviour, PickerDelegate {

	// Main control
	public BattleUIController GUI;
	Karaimon player;
	Karaimon enemy;

	// Booleans
	public bool isEnemyTurn = false;
	public bool shouldEndBattle = false;
	bool playerDefending = false;
	bool enemyDefending = false;
	bool shouldRunEnemyTurn = false;
	bool shouldRunPlayerTurn = false;
	bool playerWon = false;

	// Battle wins
	int battleCount = 0;

	// Flow Control
	void Start() {
		DataHolder.LoadData ();
		GUI.LoadPicker();
	}

	public void RestartBattle(bool playerWon) {
		isEnemyTurn = false;
		shouldEndBattle = false;
		playerDefending = false;
		enemyDefending = false;
		shouldRunEnemyTurn = false;
		shouldRunPlayerTurn = false;
		this.playerWon = false;
		GUI.ClearLog ();

		if (playerWon) {
			player.Reset();
			((PickerDelegate)this).MonPicked (player);
			return;
		} else {
			GUI.LoadPicker();
		}
	}

	public void AfterShowMessage() {
		if (shouldRunEnemyTurn)
			PrepareEnemyTurn ();
		else if (shouldRunPlayerTurn)
			PreparePlayerTurn ();
	}

	public void EndBattle() {
		shouldEndBattle = false;
		shouldRunEnemyTurn = false;
		shouldRunPlayerTurn = false;
		
		if (playerWon) {
			int experience = 50; // dynamic later
			bool leveled = player.AddExperience(experience);
			GUI.AddMessage(new Message(player.name + " gained " +
			                             experience + " experience!", IEnums.MessageType.Generic));
			
			if (leveled) {
				GUI.AddMessage(new Message(player.name + " is now level " +
				                             player.attributes.level + "!", IEnums.MessageType.Generic));
			}
			
			GUI.ShowMessages();
			RefreshInformation();
		}
		
		GUI.EndBattle(playerWon);
	}

	// Information
	void RefreshInformation() {
		GUI.playerMonName.text = player.name;
		GUI.playerMonLevel.text = "Level: " + player.attributes.level;
		GUI.playerMonExp.text = "XP: " + player.attributes.experience;
		
		GUI.enemyMonName.text = enemy.name;
		GUI.enemyMonLevel.text = "Level: " + enemy.attributes.level;

		RefreshHealth ();
	}

	public void RefreshHealth() {
		GUI.playerMonLife.text = "Health: " + player.attributes.currentLife.ToString();
		GUI.enemyMonLife.text = "Health: " + enemy.attributes.currentLife.ToString();
	}

	public bool RefreshAlive() {
		if (!player.isAlive ()) {
			GUI.playerMonName.text = player.name + " fainted!";
			return false;
		} else if (!enemy.isAlive ()) {
			GUI.enemyMonName.text = enemy.name + " fainted!";
			return false;
		}
		
		return false;
	}

	// Player flow
	void PreparePlayerTurn() {
		shouldRunPlayerTurn = false;
		playerDefending = false;
		isEnemyTurn = false;
	}

	public void Attack(int attack) {
		Attack (player, enemy, attack);
	}

	void Attack(Karaimon attacker, Karaimon defender, int attack) {
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
			GUI.AddMessage(new Message(attackObj.name + " can't be used anymore!", IEnums.MessageType.Generic));
			GUI.ShowMessages();
			return;
		}
		
		float damage = AttackController.resolveAttack(attackObj, attacker, defender,
		                                              defender == player ? playerDefending : enemyDefending);
		int effectiveness = AttackController.GetEffectiveness (attackObj, defender);
		
		GUI.AddMessage (new Message(string.Format("{0} used {1}! ({2} uses left)",
		                                            attacker.name, attackObj.name, attackObj.usesLeft),
		                              IEnums.MessageType.Generic));
		
		if (effectiveness == 1) {
			GUI.AddMessage (new Message ("It's super effective!", IEnums.MessageType.Generic));
		} else if (effectiveness == -1) {
			GUI.AddMessage (new Message ("It's not very effective...", IEnums.MessageType.Generic));
		}
		
		GUI.AddMessage (new Message(defender.name + " lost " + damage + " of health!", IEnums.MessageType.LostHP));
		
		if (!defender.isAlive()) {
			playerWon = defender != player;
			GUI.AddMessage (new Message(defender.name + " fainted!", IEnums.MessageType.Fainted));
		}
		
		if (attacker == player) {
			shouldRunEnemyTurn = true;
			isEnemyTurn = true;
		} else {
			shouldRunPlayerTurn = true;
			isEnemyTurn = false;
		}
		
		GUI.ShowMessages();
	}

	public void Defend() {
		playerDefending = true;
		GUI.AddMessage (new Message(player.name + " defended!", IEnums.MessageType.Generic));
		
		shouldRunEnemyTurn = true;
		GUI.ShowMessages ();
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		GUI.AddMessage(new Message(mon.name + " picked! He has " + mon.attributes.currentTou + " toughness!", IEnums.MessageType.Generic));
		
		player = mon.Clone();
		enemy = DataHolder.MonList[new System.Random ().Next (0, DataHolder.MonList.Count - 1)].Clone();
		GUI.AddMessage (new Message(enemy.name + " is his enemy! He has " + enemy.attributes.currentTou + " toughness!", IEnums.MessageType.Generic));
		
		GUI.ShowMessages ();
		
		RefreshInformation ();
		PreparePlayerTurn ();
	}

	// Enemy flow
	void PrepareEnemyTurn() {
		shouldRunEnemyTurn = false;
		if (!enemy.isAlive ())
			return;
		
		enemyDefending = false;
		
		StartCoroutine ("EnemyTurn");
	}

	IEnumerator EnemyTurn() {
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
		
		EnemyFinished (defended, attackNumber);
	}

	public void EnemyFinished(bool defended, int attackNumber) {
		shouldRunPlayerTurn = true;
		
		if (defended) {
			GUI.AddMessage (new Message(enemy.name + " defended!", IEnums.MessageType.Generic));
			GUI.ShowMessages();
		} else {
			Attack (enemy, player, attackNumber);
		}
	}
}

