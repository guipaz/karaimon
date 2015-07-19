using System;
using System.Collections;
using System.Collections.Generic;

public class DBObject {

	public Dictionary<string, object> dbHash;

	protected int ParseInt(string key) {
		return Contains (key) ? int.Parse (dbHash[key].ToString()) : 0;
	}

	protected string ParseStr(string key) {
		return Contains (key) ? dbHash[key].ToString() : null;
	}

	protected float ParseFloat(string key) {
		return Contains (key) ? float.Parse (dbHash[key].ToString()) : 0;
	}

	bool Contains(string key) {
		return dbHash.ContainsKey(key);
	}
}

