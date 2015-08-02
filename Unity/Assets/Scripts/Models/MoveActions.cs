using System.Collections;
using System.Collections.Generic;

public interface IMoveAction
{
	MoveResult ResolveMove(WildMon attacker, WildMon defender);
}

public class MoveResult
{
	public int damageInflicted { get; set; }
	public List<Status> statusInflicted { get; set; }
}

public class MoveAction : IMoveAction
{
	MonMove move;

	public static IMoveAction Get(MonMove move)
	{
		return new MoveAction(move);
	}

	public MoveAction(MonMove move)
	{
		this.move = move;
	}
 
	public MoveResult ResolveMove(WildMon attacker, WildMon defender)
	{
		int damage = Rules.CalculateDamage(move, attacker, defender);

		MoveResult result = new MoveResult();
		result.damageInflicted = damage;

		return result;
	}

}