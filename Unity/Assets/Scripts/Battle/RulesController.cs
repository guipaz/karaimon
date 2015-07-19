using System;
using UnityEngine;

public class RulesController
{
	public static int CalculateHP(int iv, int baseHp, int ev, int level) {
		float a = 2f * baseHp;
		float b = ev / 4f;
		float c = (float)iv + a + b + 100;
		float d = c * level / 100f;
		float finalHp = d + 10;
		return (int)Math.Round(finalHp);
	}

	public static int CalculateStat(int iv, int baseStat, int ev, int level) {
		float a = 2f * baseStat;
		float b = ev / 4f;
		float c = (float)iv + a + b;
		float d = c * level / 100f;
		float finalStat = d + 5;
		//TODO: nature
		return (int)Math.Round(finalStat);
	}

	public static int CalculateDamage(MonMove attack, WildMon attacker, WildMon defender) {
		int attackStat = 0;
		int defenseStat = 1;

		if (attack.category == IEnums.MoveType.Physical) {
			attackStat = attacker.attack;
			defenseStat = defender.defense;
		} else if (attack.category == IEnums.MoveType.Special) {
			attackStat = attacker.spAttack;
			defenseStat = defender.spDefense;
		} else {
			return 0;
		}
		
		int strenght = attack.strenght;

		float a = (2 * attacker.level + 10) / 250.0f;
		float b = (float)attackStat / (float)defenseStat;
		float c = (a * b * (float)strenght) + 2f;

		float stab = 1;
		if (attacker.IsSameType(attack.element))
			stab = 1.5f;

		System.Random r = new System.Random ();
		float m = stab * CalculateElementalQuotient(attack, defender) * r.Next(85, 100) / 100f;
		//TODO: critical and random

		float damage = c * m;

		return (int) Math.Round(damage);
	}

	private static float CalculateElementalQuotient(MonMove attack, WildMon defender) {
		switch (GetEffectiveness(attack, defender)) {
		case 1:
			return 2;
        case 2:
            return 4;
		case -1:
			return 0.5f;
        case -2:
            return 0.25f;
		case -3:
			return 0f;
		default:
			return 1;
		}
	}

	/**
	 * 0 - Normal
	 * 1 - Not Very Effective
	 * 2 - Very Effective
	 * 3 - No Effect
	 */
	public static int GetEffectiveness(MonMove attack, WildMon defender) {
        Element attackerElement = attack.element;

		int effectiveness1 = attackerElement.GetEffectiveness (defender.GetElement1 ().id);
		int effectiveness2 = defender.GetElement2 () != null ? defender.GetElement2 ().id : 0;

		if (effectiveness1 == 3 || effectiveness2 == 3) // no effect
			return -3;

		int effectiveness = 0;
		for (int i = 0; i < 2; i++) {
			int eff = i == 0 ? effectiveness1 : effectiveness2;
			switch (eff) {
			case 0:
				continue;
			case 1:
				effectiveness--;
				continue;
			case 2:
				effectiveness++;
				continue;
			}
		}

		return effectiveness;
	}

	public static int CalculateExperience(int baseExp) {
		//TODO: calculate other variables
		float exp = (float)baseExp / 7f;
		return (int)Math.Round(exp);
	}

	public static bool CalculateLevelUp(int currentLevel, int currentExperience) {
		//TODO: implement other groups

		// Erratic <= 50
		float l = currentLevel + 1f;
		float a = l * l * l;
		float b = (100 - l) * a;
		float exp = b / 50f;

		return currentExperience >= exp;
	}
}

