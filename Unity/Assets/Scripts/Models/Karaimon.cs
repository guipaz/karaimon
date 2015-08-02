using System;
using UnityEngine;
using System.Collections.Generic;

public class Karaimon : DBObject {
	public int id { get; set; }
	public string name { get; set; }

	public List<MonMove> moves { get; set; }
	public int experience { get; set; }
	public Element element1 { get; set; }
	public Element element2 { get; set; }

	// Attributes
	public int hp { get; set; }
	public int attack { get; set; }
	public int defense { get; set; }
	public int spAttack { get; set; }
	public int spDefense { get; set; }
	public int speed { get; set; }

	// EV
	public int evHp { get; set; }
	public int evAttack { get; set; }
	public int evDefense { get; set; }
	public int evSpAttack { get; set; }
	public int evSpDefense { get; set; }
	public int evSpeed { get; set; }

	public Karaimon() { }
	
	public Karaimon(Dictionary<string, object> hash) {
		dbHash = hash;

		// Basic info
		id = ParseInt("id");
		name = ParseStr("ds_name");
		element1 = DataHolder.GetElement(ParseInt("id_element1"));
		element2 = DataHolder.GetElement(ParseInt("id_element2"));
		experience = ParseInt("vl_experience");

		// Attributes
		hp = ParseInt("vl_hp");
		attack = ParseInt("vl_attack");
		defense = ParseInt("vl_defense");
		spAttack = ParseInt("vl_sp_attack");
		spDefense = ParseInt("vl_sp_defense");
		speed = ParseInt("vl_speed");

		// EVs
		evHp = ParseInt("vl_ev_attack");
		evAttack = ParseInt("vl_ev_attack");
		evDefense = ParseInt("vl_ev_defense");
		evSpAttack = ParseInt("vl_ev_sp_attack");
		evSpDefense = ParseInt("vl_ev_sp_defense");
		evSpeed = ParseInt("vl_ev_speed");

		Debug.Log (string.Format("HP: {0} Attack: {1} Defense: {2} Sp. Attack: {3} Sp. Defense: {4} Speed: {5}",
		                         hp, attack, defense, spAttack, spDefense, speed));
	}
	
	public Karaimon Clone() {
		Karaimon newKaraimon = new Karaimon ();
		newKaraimon.id = id;
		newKaraimon.moves = new List<MonMove> (moves);
		newKaraimon.name = name;
		newKaraimon.element1 = DataHolder.GetElement (element1.id);
		newKaraimon.element2 = DataHolder.GetElement (element2.id);
		newKaraimon.hp = hp;
		newKaraimon.attack = attack;
		newKaraimon.defense = defense;
		newKaraimon.spAttack = spAttack;
		newKaraimon.spDefense = spDefense;
		newKaraimon.speed = speed;
		newKaraimon.evHp = evHp;
		newKaraimon.evAttack = evAttack;
		newKaraimon.evDefense = evDefense;
		newKaraimon.evSpAttack = evSpAttack;
		newKaraimon.evSpDefense = evSpDefense;
		newKaraimon.evSpeed = evSpeed;

		return newKaraimon;
	}
}

