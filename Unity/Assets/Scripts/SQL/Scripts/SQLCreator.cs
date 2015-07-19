public class SQLCreator {

	public const string MON_SELECT = "SELECT * FROM karaimon";

	public const string ATT_SELECT = "SELECT at.*, abm.id_attack, abm.id_mon, abm.vl_level " +
		"FROM attack_by_mon abm, mon_attack at " +
		"WHERE abm.id_mon = {0} AND abm.id_attack = at.id";

	public const string ELEMENT_SELECT = "SELECT * FROM element";

	public const string ELEMENT_RELATION_SELECT = "SELECT * FROM element_relation WHERE id_element = {0}";

}

