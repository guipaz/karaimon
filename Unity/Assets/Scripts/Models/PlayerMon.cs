using System;
public class PlayerMon : WildMon {

	public int experience { get; set; }
	string alias;

	// EVs
	public int totalEvHp { get; set; }
	public int totalEvAttack { get; set; }
	public int totalEvDefense { get; set; }
	public int totalEvSpAttack { get; set; }
	public int totalEvSpDefense { get; set; }
	public int totalEvSpeed { get; set; }

	public PlayerMon(Karaimon baseMon, int startingLevel) : base(baseMon, startingLevel) { }

	public bool AddExperience(int exp) {
		experience += exp;

		bool leveledUp = RulesController.CalculateLevelUp (level, experience);
		if (leveledUp) {
			level++;
			CalculateStats ();
			CheckMoves(true);
		}

		return leveledUp;
	}

	public override void CalculateStats() {
		hp = RulesController.CalculateHP (ivHp, baseMon.hp, totalEvHp, level);
		attack = RulesController.CalculateStat (ivAttack, baseMon.attack, totalEvAttack, level);
		defense = RulesController.CalculateStat (ivDefense, baseMon.defense, totalEvDefense, level);
		spAttack = RulesController.CalculateStat (ivSpAttack, baseMon.spAttack, totalEvSpAttack, level);
		spDefense = RulesController.CalculateStat (ivSpDefense, baseMon.spDefense, totalEvSpDefense, level);
		speed = RulesController.CalculateStat (ivSpeed, baseMon.speed, totalEvSpeed, level);
	}

	public string GetName() {
		if (alias != null && alias.Length > 2) {
			return alias;
		}

		return baseMon.name;
	}
}