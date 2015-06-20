package models;

import java.util.HashMap;
import java.util.Map;
import lombok.Data;

@Data
public class AttributeSheet implements DBObject {
    Integer id;
    Float strenght;
    Float agility;
    Float toughness;

    @Override
    public void parse(Map<String, Object> map) {
        if (map.containsKey("id")) {
            id = Integer.parseInt(map.get("id").toString());
        }
        
        if (map.containsKey("vl_strenght")) {
            strenght = Float.parseFloat(map.get("vl_strenght").toString());
        }
        
        if (map.containsKey("vl_agility")) {
            agility = Float.parseFloat(map.get("vl_agility").toString());
        }
        
        if (map.containsKey("vl_toughness")) {
            toughness = Float.parseFloat(map.get("vl_toughness").toString());
        }
    }

    @Override
    public Map<String, String> toParams() {
        Map<String, String> params = new HashMap<>();
        
        params.put("vl_strenght", strenght.toString());
        params.put("vl_agility", agility.toString());
        params.put("vl_toughness", toughness.toString());
        
        return params;
    }
}
