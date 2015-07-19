using System;
public class WildMon {

	protected Karaimon baseMon;

	public int level { get; set; }

	// Moves
	public MoveSheet moves { get; set; }

	// Current battle stats
	public int currentHp { get; set; }
	public int currentAttack { get; set; }
	public int currentDefense { get; set; }
	public int currentSpAttack { get; set; }
	public int currentSpDefense { get; set; }
	public float currentAccuracy { get; set; }
	public float currentEvasion { get; set; }

	// Calculated stats
	public int hp { get; set; }
	public int attack { get; set; }
	public int defense { get; set; }
	public int spAttack { get; set; }
	public int spDefense { get; set; }
	public int speed { get; set; }

	// Individual Values
	protected int ivHp;
	protected int ivAttack;
	protected int ivDefense;
	protected int ivSpAttack;
	protected int ivSpDefense;
	protected int ivSpeed;

	public WildMon(Karaimon baseMon) : this(baseMon, 1) { }

	public WildMon(Karaimon baseMon, int level) {
		this.baseMon = baseMon;
		moves = new MoveSheet ();
		this.level = level;
		CheckMoves ();
		CalculateStats ();
	}

	protected void CheckMoves() {
		foreach (MonMove move in baseMon.moves) {
			if (move.levelAcquired == level) {
				MonMove m = move.Clone();
				m.ResetPP();
				moves.AddMove(m);
			}
		}
	}

	public int GetExperienceGiven() {
		return baseMon.experience;
	}

	public void ResetCurrent() {
		currentHp = hp;
		currentAttack = attack;
		currentDefense = defense;
		currentSpAttack = spAttack;
		currentSpDefense = spDefense;
		currentAccuracy = 1f;
		currentEvasion = 1f;
	}

	public bool ReduceLife(int damage) {
		currentHp -= damage;
		if (currentHp <= 0)
			currentHp = 0;
		return IsAlive();
	}

	public void CalculateStats() {
		hp = RulesController.CalculateHP (ivHp, hp, 0, level);
		attack = RulesController.CalculateStat (ivAttack, baseMon.attack, 0, level);
		defense = RulesController.CalculateStat (ivDefense, baseMon.defense, 0, level);
		spAttack = RulesController.CalculateStat (ivSpAttack, baseMon.spAttack, 0, level);
		spDefense = RulesController.CalculateStat (ivSpDefense, baseMon.spDefense, 0, level);
		speed = RulesController.CalculateStat (ivSpeed, baseMon.speed, 0, level);
	}

	public string GetName() {
		return baseMon.name;
	}

	public bool IsSameType(Element e) {
		return baseMon.element1 == e || baseMon.element2 == e;
	}

	public Element GetElement1() {
		return baseMon.element1;
	}

	public Element GetElement2() {
		return baseMon.element2;
	}

	public bool IsAlive() {
		return currentHp > 0;
	}
}

