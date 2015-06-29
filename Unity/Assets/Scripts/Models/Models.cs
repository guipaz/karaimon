using UnityEngine;
using System.Collections.Generic;

public class Karaimon {
	public int id { get; set; }
	public string name { get; set; }
	public float totalLife { get; set; }
	public AttributeSheet attributes { get; set; }
	public AttackSheet attacks { get; set; }

	public Karaimon() { }

	public Karaimon(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("id")) {
			id = int.Parse(hash["id"].ToString());
		}

		if (hash.ContainsKey ("id_attribute_sheet")) {
			AttributeSheet at = new AttributeSheet(hash);
			if (at != null)
				attributes = at;

			totalLife = at.tou * 5;
		}

		if (hash.ContainsKey ("ds_name")) {
			name = hash["ds_name"].ToString();
		}
	}

	public Karaimon Clone() {
		Karaimon newKaraimon = new Karaimon ();
		newKaraimon.id = id;
		newKaraimon.attributes = attributes.Clone ();
		newKaraimon.attacks = attacks.Clone ();
		newKaraimon.name = name;
		newKaraimon.totalLife = totalLife;
		return newKaraimon;
	}

	public void resetAttacksUses() {
		foreach (MonAttack attack in attacks.attacks) {
			attack.usesLeft = attack.totalUses;
		}
	}

	public void reduceLife(int amount) {
		totalLife -= amount;
		if (totalLife < 0)
			totalLife = 0;
	}

	public bool isAlive() {
		return totalLife > 0;
	}
}

public class AttributeSheet {
	public float str  { get; set; }
	public float agi { get; set; }
	public float tou { get; set; }

	public AttributeSheet () { }

	public AttributeSheet(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("vl_strenght")) {
			str = float.Parse(hash["vl_strenght"].ToString());
		}

		if (hash.ContainsKey ("vl_agility")) {
			agi = float.Parse(hash["vl_agility"].ToString());
		}

		if (hash.ContainsKey ("vl_toughness")) {
			tou = float.Parse(hash["vl_toughness"].ToString());
		}
	}

	public AttributeSheet Clone() {
		AttributeSheet at = new AttributeSheet ();
		at.str = str;
		at.agi = agi;
		at.tou = tou;
		return at;
	}

	public void setAttributes(int s, int a, int t) {
		this.str = s;
		this.agi = a;
		this.tou = t;
	}
}

public class Element {
	public int id;
	public string name;
	public int id_strong_against;
	public int id_weak_against;
}

public class MonAttack {
	public int id;
	public string name;
	public int usesLeft;
	public int totalUses;
	public float strenght;
	public Element element;

	public MonAttack() { }

	public MonAttack(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("ds_name")) {
			name = hash["ds_name"].ToString();
		}
		
		if (hash.ContainsKey ("vl_uses")) {
			totalUses = int.Parse(hash["vl_uses"].ToString());
			usesLeft = totalUses;
		}
		
		if (hash.ContainsKey ("vl_strenght")) {
			strenght = float.Parse(hash["vl_strenght"].ToString());
		}
	}

	public MonAttack Clone() {
		MonAttack at = new MonAttack ();
		at.id = id;
		at.name = name;
		at.totalUses = totalUses;
		at.usesLeft = usesLeft;
		at.strenght = strenght;
		at.element = element;
		return at;
	}
}

public class AttackSheet {
	public List<MonAttack> attacks = new List<MonAttack>();

	public AttackSheet Clone() {
		List<MonAttack> nAttacks = new List<MonAttack> ();
		foreach (MonAttack at in attacks) {
			nAttacks.Add(at.Clone());
		}

		AttackSheet nAttackSheet = new AttackSheet ();
		nAttackSheet.attacks = nAttacks;
		return nAttackSheet;
	}

	public int Count() {
		return attacks.Count;
	}

	public AttackSheet() { }

	public AttackSheet(List<Dictionary<string, object>> hashes) {
		foreach (Dictionary<string, object> entry in hashes) {
			attacks.Add(new MonAttack(entry));
		}
	}

	public MonAttack getAttack1() {
		if (attacks.Count > 0)
			return attacks [0];
		return null;
	}

	public MonAttack getAttack2() {
		if (attacks.Count > 1)
			return attacks [1];
		return null;
	}

	public MonAttack getAttack3() {
		if (attacks.Count > 2)
			return attacks [2];
		return null;
	}

	public MonAttack getAttack4() {
		if (attacks.Count > 3)
			return attacks [3];
		return null;
	}
}
