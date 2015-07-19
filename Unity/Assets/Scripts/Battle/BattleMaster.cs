using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattleMaster : MonoBehaviour, PickerDelegate {

	// Main control
	public BattleUIController GUI;
	PlayerMon player;
	WildMon enemy;

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
			player.ResetCurrent();
			((PickerDelegate)this).MonPicked (null);
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
			int experience = RulesController.CalculateExperience(enemy.GetExperienceGiven());
			bool leveled = player.AddExperience(experience);
			GUI.AddMessage(new Message(player.GetName() + " gained " +
			                             experience + " experience!", IEnums.MessageType.Generic));
			
			if (leveled) {
				GUI.AddMessage(new Message(player.GetName () + " is now level " +
				                             player.level + "!", IEnums.MessageType.Generic));
			}
			
			GUI.ShowMessages();
			RefreshInformation();
		}
		
		GUI.EndBattle(playerWon);
	}

	// Information
	void RefreshInformation() {
		GUI.playerMonName.text = player.GetName();
		GUI.playerMonLevel.text = "Level: " + player.level;
		GUI.playerMonExp.text = "XP: " + player.experience;
		
		GUI.enemyMonName.text = enemy.GetName();
		GUI.enemyMonLevel.text = "Level: " + enemy.level;

		RefreshHealth ();
	}

	public void RefreshHealth() {
		GUI.playerMonLife.text = "Health: " + player.currentHp.ToString();
		GUI.enemyMonLife.text = "Health: " + enemy.currentHp.ToString();
	}

	public bool RefreshAlive() {
		if (!player.IsAlive()) {
			GUI.playerMonName.text = player.GetName() + " fainted!";
			return false;
		} else if (!enemy.IsAlive()) {
			GUI.enemyMonName.text = enemy.GetName() + " fainted!";
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

	void Attack(WildMon attacker, WildMon defender, int attack) {
		MonMove attackObj;
		attackObj = attacker.moves [attack-1];
		
		if (attackObj.usesLeft <= 0) {
			GUI.AddMessage(new Message(attackObj.name + " can't be used anymore!", IEnums.MessageType.Generic));
			GUI.ShowMessages();
			return;
		}
		
		int damage = RulesController.CalculateDamage(attackObj, attacker, defender);
		defender.ReduceLife (damage);
		attackObj.usesLeft--;

		int effectiveness = RulesController.GetEffectiveness (attackObj, defender);
		
		GUI.AddMessage (new Message(string.Format("{0} used {1}! ({2} uses left)",
		                                            attacker.GetName(), attackObj.name, attackObj.usesLeft),
		                              IEnums.MessageType.Generic));
		
		if (effectiveness == 1) {
			GUI.AddMessage (new Message ("It's super effective!", IEnums.MessageType.Generic));
		} else if (effectiveness == -1) {
			GUI.AddMessage (new Message ("It's not very effective...", IEnums.MessageType.Generic));
		}
		
		GUI.AddMessage (new Message(defender.GetName() + " lost " + damage + " of health!", IEnums.MessageType.LostHP));
		
		if (!defender.IsAlive()) {
			playerWon = defender != player;
			GUI.AddMessage (new Message(defender.GetName() + " fainted!", IEnums.MessageType.Fainted));
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
		GUI.AddMessage (new Message(player.GetName() + " defended!", IEnums.MessageType.Generic));
		
		shouldRunEnemyTurn = true;
		GUI.ShowMessages ();
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		if (mon != null)
			player = new PlayerMon(mon);

		Karaimon enemyMon = DataHolder.mons[new System.Random ().Next (0, DataHolder.mons.Count - 1)];
		enemy = new WildMon(enemyMon);

		GUI.RefreshMoves (player);

		GUI.AddMessage(new Message(player.GetName() + " picked!", IEnums.MessageType.Generic));
		GUI.AddMessage (new Message(enemy.GetName() + " is his enemy!", IEnums.MessageType.Generic));
		GUI.ShowMessages ();

		player.ResetCurrent ();
		enemy.ResetCurrent ();

		RefreshInformation ();
		PreparePlayerTurn ();
	}

	// Enemy flow
	void PrepareEnemyTurn() {
		shouldRunEnemyTurn = false;
		if (!enemy.IsAlive ())
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
			attackNumber = r.Next (1, enemy.moves.Count());
		} else {
			defended = true;
		}
		
		EnemyFinished (defended, attackNumber);
	}

	public void EnemyFinished(bool defended, int attackNumber) {
		shouldRunPlayerTurn = true;
		
		if (defended) {
			GUI.AddMessage (new Message(enemy.GetName() + " defended!", IEnums.MessageType.Generic));
			GUI.ShowMessages();
		} else {
			Attack (enemy, player, attackNumber);
		}
	}
}

