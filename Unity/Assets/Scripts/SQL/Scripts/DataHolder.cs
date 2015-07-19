using System;
using System.Collections.Generic;

public class DataHolder {

	public static List<Karaimon> mons = new List<Karaimon>();
	public static List<Element> elements = new List<Element>();

	public static void LoadData() {
		elements = MonSQLite.Instance.GetElements();
		mons = MonSQLite.Instance.GetKaraimon ();
	}

	public static Element GetElement(int id) {
		if (id == null)
			return null;
		
		foreach (Element e in elements) {
			if (e.id == id)
				return e;
		}
		
		return null;
	}
}

