using UnityEngine;
using System.Collections.Generic;

public class MonMove : DBObject {
	public int id { get; set; }
	public string name { get; set; }
	public int strenght { get; set; }
	public int totalUses { get; set; }
	public Element element { get; set; }
	public IEnums.MoveType category { get; set; }
	public int levelAcquired { get; set; }
	public int usesLeft { get; set; }

	public MonMove() { }

	public MonMove(Dictionary<string, object> hash) {
		dbHash = hash;
		
		name = ParseStr("ds_name");
		totalUses = ParseInt("vl_uses");
		levelAcquired = ParseInt("vl_level");
		strenght = ParseInt("vl_strenght");
		element = DataHolder.GetElement(ParseInt("id_element"));
		category = (IEnums.MoveType)ParseInt("vl_category");

		ResetPP ();
	}

	public void ResetPP() {
		usesLeft = totalUses;
	}

	public MonMove Clone() {
		MonMove m = new MonMove ();
		m.name = name;
		m.totalUses = totalUses;
		m.levelAcquired = levelAcquired;
		m.strenght = strenght;
		m.element = element;
		m.category = category;
		return m;
	}
}

public class MoveSheet : DBObject {
	List<MonMove> moves = new List<MonMove>();

	public bool AddMove(MonMove move) {
		if (moves.Count == 4)
			return false;
		moves.Add (move);
		return true;
	}

	public void RemoveMove(int moveId) {
		int index = -1;
		for (int i = 0; i < moves.Count; i++) {
			MonMove move = moves[i];
			if (moveId == move.id) {
				index = i;
				break;
			}
		}

		if (index > -1)
			moves.RemoveAt(index);
	}

	public MonMove this[int i] {
		get { return moves[i]; }
		set { moves [i] = value; }
	}

	public int Count() {
		return moves.Count;
	}
}