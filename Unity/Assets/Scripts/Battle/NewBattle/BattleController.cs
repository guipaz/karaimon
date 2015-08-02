using UnityEngine;

public class BattleController : IBattleResponder {

	/* Mons */
	PlayerMon player;
	WildMon opponent;

	/* Turn variables */
	MonMove playerMove;
	MonMove opponentMove;

	/* Control variables */
	bool canAttack = false;
	public BattleUIController GUI { get; set; }

	public void StartBattle(PlayerMon player, WildMon opponent)
	{
		this.player = player;
		this.opponent = opponent;
		
		RefreshHealth();
		RefreshInformation();

		BeforeTurn();
	}

	void BeforeTurn()
	{
		//TODO: verify two-turn attacks, dots and conditions
		//TODO: let player choose it's move
		canAttack = true;
	}

	/**
	*  Called when the player chooses a move
	*/
	void IBattleResponder.ChoosePlayerMove(int moveIndex)
	{
		canAttack = false;
		playerMove = player.moves[moveIndex];
		//TODO: verify PP

		ChooseOpponentMove();
	}

	void ChooseOpponentMove()
	{
		//TODO: random to opponent move

		System.Random r = new System.Random ();
		int moveIndex = r.Next (0, opponent.moves.Count());
		opponentMove = opponent.moves[moveIndex];

		ResolveMoves();
	}

	void ResolveMoves()
	{
		//TODO: verify move priority, if both can attack, show messages, etc
		IMoveAction playerAction = MoveAction.Get(playerMove);
		IMoveAction opponentAction = MoveAction.Get(opponentMove);

		MoveResult playerResult = playerAction.ResolveMove(player, opponent);
		MoveResult opponentResult = opponentAction.ResolveMove(player, opponent);

		ApplyResult(playerResult, opponent);
		ApplyResult(opponentResult, player);

		AfterTurn();
	}

	void ApplyResult(MoveResult result, WildMon target)
	{
		//TODO: apply statuses
		target.ReduceLife(result.damageInflicted);
	}

	void AfterTurn()
	{
		RefreshHealth();
		RefreshInformation();
		
		//TODO: show messages, update UI, verify fainted mons
		Debug.Log("AfterTurn");
		BeforeTurn();
	}

	void EndBattle()
	{

	}

	/* Auxiliar responder methods */
	bool IBattleResponder.CanAttack()
	{
		return canAttack;
	}

	/* TODO: GUI calls */
	void RefreshInformation() {
		GUI.playerMonName.text = player.GetName();
		GUI.playerMonLevel.text = "Level: " + player.level;
		GUI.playerMonExp.text = "XP: " + player.experience;
		
		GUI.enemyMonName.text = opponent.GetName();
		GUI.enemyMonLevel.text = "Level: " + opponent.level;

		RefreshHealth ();
	}

	public void RefreshHealth() {
		GUI.playerMonLife.text = "Health: " + player.currentHp.ToString();
		GUI.enemyMonLife.text = "Health: " + opponent.currentHp.ToString();
	}
}

public interface IBattleResponder
{
	void ChoosePlayerMove(int moveIndex);
	bool CanAttack();
}