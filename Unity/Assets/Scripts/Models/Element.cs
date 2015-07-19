using UnityEngine;
using System;
using System.Collections.Generic;

public class Element : DBObject {

	public int id { get; set; }
	public string name { get; set; }
	public int level { get; set; }
	public Dictionary<int, int> relations { get; set; }
	
	public Element(Dictionary<string, object> hash) {
		dbHash = hash;

		id = ParseInt("id");
		name = ParseStr("ds_name");
		level = ParseInt("vl_level");
	}

	public int GetEffectiveness(int targetId) {
		foreach (KeyValuePair<int, int> pair in relations)
			if (pair.Key == targetId)
				return pair.Value;

		return 0;
	}
}