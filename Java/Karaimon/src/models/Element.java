package models;

import controllers.DatabaseController;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import lombok.Data;

@Data
public class Element implements DBObject, Comparable<Element> {
    Integer id;
    String name;
    Integer strenght_id;
    Integer weakness_id;
    Integer multiplier;

    @Override
    public void parse(Map<String, Object> map) {
        if (map.containsKey("id")) {
            id = Integer.parseInt(map.get("id").toString());
        }
        
        if (map.containsKey("ds_name")) {
            name = map.get("ds_name").toString();
        }
        
        if (map.containsKey("id_strenght_against")) {
            strenght_id = Integer.parseInt(map.get("id_strenght_against").toString());
        }
        
        if (map.containsKey("id_weakened_by")) {
            weakness_id = Integer.parseInt(map.get("id_weakened_by").toString());
        }
    }

    @Override
    public int compareTo(Element o) {
        return Integer.compare(id, o.id);
    }

    @Override
    public Map<String, String> toParams() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    
    
}
