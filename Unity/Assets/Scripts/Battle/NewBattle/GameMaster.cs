using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour, PickerDelegate
{
	/* Player team */
	PlayerMon player;

	/* Battle references */
	public BattleUIController GUI;
	BattleController battleController;

	/* Assorted variables */
	public int startingLevel = 5;

	void Start()
	{
		DataHolder.LoadData ();
		GUI.LoadPicker(this);
	}

	void PickerDelegate.MonPicked(Karaimon mon)
	{
		player = new PlayerMon(mon, startingLevel);
		PrepareBattle();
	}

	void PrepareBattle()
	{
		Karaimon opponentMon = DataHolder.mons[new System.Random ().Next (0, DataHolder.mons.Count - 1)];
		WildMon opponent = new WildMon(opponentMon, player.level);

		GUI.RefreshMoves (player);

		battleController = new BattleController();
		GUI.SetBattleResponder(battleController);
		battleController.GUI = GUI;
		battleController.StartBattle(player, opponent);
	}
}