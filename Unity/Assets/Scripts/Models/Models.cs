using UnityEngine;
using System.Collections.Generic;

public class Karaimon {
	public int id { get; set; }
	public string name { get; set; }
	public int totalLife { get; set; }
	public AttributeSheet attributes { get; set; }

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
	public int str  { get; set; }
	public int agi { get; set; }
	public int tou { get; set; }

	public AttributeSheet () { }

	public AttributeSheet(Dictionary<string, object> hash) {
		if (hash.ContainsKey ("vl_strenght")) {
			str = int.Parse(hash["vl_strenght"].ToString());
		}

		if (hash.ContainsKey ("vl_agility")) {
			agi = int.Parse(hash["vl_agility"].ToString());
		}

		if (hash.ContainsKey ("vl_toughness")) {
			tou = int.Parse(hash["vl_toughness"].ToString());
		}
	}

	public void setAttributes(int s, int a, int t) {
		this.str = s;
		this.agi = a;
		this.tou = t;
	}
}

public class Element {
	int id;
	string name;
	int id_strong_against;
	int id_weak_against;
}
