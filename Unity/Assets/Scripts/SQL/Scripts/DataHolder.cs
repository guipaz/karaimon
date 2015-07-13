using System;
using System.Collections.Generic;

public class DataHolder {

	public static List<Karaimon> MonList = new List<Karaimon>();

	public static void LoadData() {
		Element.SetElements (MonSQLite.Instance.GetElements ());
		MonList = MonSQLite.Instance.GetKaraimon ();
	}
}

