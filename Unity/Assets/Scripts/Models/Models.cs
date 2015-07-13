using UnityEngine;
using System.Collections.Generic;

public class Karaimon {
	public int id { get; set; }
	public string name { get; set; }
	public AttributeSheet attributes { get; set; }
	public AttackSheet attacks { get; set; }
	public Element element { get; set; }

	public Karaimon() { }

	public Karaimon(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("id")) {
			id = int.Parse(hash["id"].ToString());
		}

		if (hash.ContainsKey ("id_attribute_sheet")) {
			AttributeSheet at = new AttributeSheet(hash);
			if (at != null)
				attributes = at;
		}

		if (hash.ContainsKey ("ds_name")) {
			name = hash["ds_name"].ToString();
		}

		if (hash.ContainsKey ("id_element")) {
			element = Element.Get(int.Parse(hash["id_element"].ToString()));
		}
	}

	public Karaimon Clone() {
		Karaimon newKaraimon = new Karaimon ();
		newKaraimon.id = id;
		newKaraimon.attributes = attributes.Clone ();
		newKaraimon.attacks = attacks.Clone ();
		newKaraimon.name = name;
		newKaraimon.element = Element.Get (element.id);
		return newKaraimon;
	}

	public void Reset() {
		foreach (MonAttack attack in attacks.attacks) {
			attack.usesLeft = attack.totalUses;
		}

		attributes.currentLife = attributes.totalLife;
	}

	public void reduceLife(int amount) {
		attributes.ReduceLife (amount);
	}

	public bool isAlive() {
		return attributes.currentLife > 0;
	}

	public bool AddExperience(int exp) {
		return attributes.AddExperience (exp);
	}
}

public class AttributeSheet {
	public int experience { get; set; }
	public int level { get; set; }
	public float currentStr { get; set; }
	public float currentAgi { get; set; }
	public float currentTou { get; set; }
	public float currentLife { get; set; }
	public float totalLife { get; set; }

	float baseStr;
	float baseAgi;
	float baseTou;
	float strAdd;
	float agiAdd;
	float touAdd;

	public AttributeSheet () { }

	public AttributeSheet(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("vl_strenght")) {
			baseStr = float.Parse(hash["vl_strenght"].ToString());
		}

		if (hash.ContainsKey ("vl_agility")) {
			baseAgi = float.Parse(hash["vl_agility"].ToString());
		}

		if (hash.ContainsKey ("vl_toughness")) {
			baseTou = float.Parse(hash["vl_toughness"].ToString());
		}

		if (hash.ContainsKey ("vl_strenght_add")) {
			strAdd = float.Parse(hash["vl_strenght_add"].ToString());
		}
		
		if (hash.ContainsKey ("vl_agility_add")) {
			agiAdd = float.Parse(hash["vl_agility_add"].ToString());
		}
		
		if (hash.ContainsKey ("vl_toughness_add")) {
			touAdd = float.Parse(hash["vl_toughness_add"].ToString());
		}

		if (hash.ContainsKey ("vl_experience")) {
			experience = int.Parse(hash["vl_experience"].ToString());
		}

		CalculateAttributes ();
	}

	public AttributeSheet Clone() {
		AttributeSheet at = new AttributeSheet ();
		at.baseStr = baseStr;
		at.baseAgi = baseAgi;
		at.baseTou = baseTou;
		at.level = level;
		at.experience = experience;
		at.strAdd = strAdd;
		at.agiAdd = agiAdd;
		at.touAdd = touAdd;
		at.CalculateAttributes ();
		return at;
	}

	public bool AddExperience(int exp) {
		int currentExperience = experience % 100;
		experience += exp;
		if (experience % 100 < currentExperience) { // se passar mais de 1 nivel vai dar pau
			CalculateAttributes();
			return true;
		}

		return false;
	}

	public void CalculateAttributes() {
		level = 1 + Mathf.FloorToInt(experience / 100);
		if (level == 0)
			level = 1;

		currentStr = baseStr + ((level-1) * strAdd);
		currentAgi = baseAgi + ((level-1) * agiAdd);
		currentTou = baseTou + ((level-1) * touAdd);

		totalLife = currentTou * 5;
		currentLife = totalLife;
	}

	public void ReduceLife(int life) {
		currentLife -= life;
		if (currentLife < 0)
			currentLife = 0;
	}
}

public class Element {
	public int id;
	public string name;
	public int id_strong_against;
	public int id_weak_against;
	static List<Element> elements;

	public Element(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("id")) {
			id = int.Parse(hash["id"].ToString());
		}
		
		if (hash.ContainsKey ("id_strenght_against")) {
			id_strong_against = int.Parse(hash["id_strenght_against"].ToString());
		}
		
		if (hash.ContainsKey ("id_weakened_by")) {
			id_weak_against = int.Parse(hash["id_weakened_by"].ToString());
		}
	}

	public static void SetElements(List<Element> elements) {
		Element.elements = elements;
	}

	public static Element Get(int id) {
		foreach (Element e in Element.elements) {
			if (e.id == id)
				return e;
		}

		return null;
	}
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

		if (hash.ContainsKey ("id_element")) {
			element = Element.Get(int.Parse(hash["id_element"].ToString()));
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
