public enum IStatusType {
	Burn, Freeze, Paralysis, Poison, Sleep, Confusion, Curse, Encore, Flinch, HealBlock,
	Infatuation, PartiallyTrapped, Seeding, AquaRing, Bracing, DefenseCurl, Glowing,
	Rooting, Protection, SemiInvulnerable, StatRaised, StatReduced
}

public class Status {
	public IStatusType type { get; set; }
	public int turnsLeft { get; set; }
	public bool isVolatile { get; set; }
	public int damagePerTurn { get; set; }
	public int healPerTurn { get; set; }

	/* Stat modifier */
	public string statRaised { get; set; }
	public string statReduced { get; set; }
	public int statModifierAmount { get; set; }
}