using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour, PickerDelegate {

	public BattleUIController uiController;
	public static List<Karaimon> monList = new List<Karaimon>();

	Karaimon player;
	Karaimon enemy;

	bool playerDefending = false;
	bool enemyDefending = false;

	void Start () {
		loadMons ();
	}

	void PickerDelegate.MonPicked(Karaimon mon) {
		Debug.Log (mon.name + " picked! He has " + mon.attributes.tou + " toughness!");
		player = mon;
		enemy = monList[new System.Random ().Next (0, monList.Count - 1)];
		Debug.Log (enemy.name + " is his enemy! He has " + enemy.attributes.tou + " toughness!");

		refreshInfo ();
		preparePlayerTurn ();
	}

	void loadMons() {
		monList = MonSQLite.Instance.GetKaraimon ();
		uiController.loadPicker ();
	}

	public void playerAttacked() {
		attack (player, enemy);
		runEnemyTurn ();
	}

	public void playerDefended() {
		playerDefending = true;
		Debug.Log (player.name + " defendeu!");

		runEnemyTurn ();
	}

	void attack(Karaimon attacker, Karaimon defender) {
		int damage = attacker.attributes.str;
		if ((defender == enemy && enemyDefending) ||
		     defender == player && playerDefending)
			damage /= 2;
		defender.reduceLife (damage);
		refreshInfo ();

		Debug.Log (attacker.name + " atacou! " + defender.name + " perdeu " + damage + " de vida!");

		if (!defender.isAlive()) {
			Debug.Log (defender.name + " desmaiou!");
			if (defender == enemy)
				uiController.enemyMonName.text = enemy.name + " desmaiado!";
			else
				uiController.playerMonName.text = player.name + " desmaiado!";

		}
	}

	void runEnemyTurn() {
		if (!enemy.isAlive ())
			return;

		enemyDefending = false;

		System.Random r = new System.Random ();
		bool willAttack = r.Next (1, 10) > 6 ? false : true;
		if (willAttack) {
			attack (enemy, player);
		} else {
			enemyDefending = true;
			Debug.Log (enemy.name + " defendeu!");
		}

		preparePlayerTurn ();
	}

	void preparePlayerTurn() {
		playerDefending = false;
	}

	void refreshInfo() {
		uiController.playerMonName.text = player.name;
		uiController.enemyMonName.text = enemy.name;
		
		uiController.playerMonLife.text = "Life: " + player.totalLife.ToString();
		uiController.enemyMonLife.text = "Life: " + enemy.totalLife.ToString();
	}
}
