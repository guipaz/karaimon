package models;

import controllers.GenericController;
import java.util.HashMap;
import java.util.Map;
import lombok.Data;

@Data
public class MonAttack implements DBObject {
    Integer id;
    String name;
    Integer uses;
    Float strenght;
    
    //TODO
    Element element;
    String status;

    @Override
    public void parse(Map<String, Object> map) {
        if (map.containsKey("id")) {
            id = Integer.parseInt(map.get("id").toString());
        }
        
        if (map.containsKey("ds_name")) {
            name = map.get("ds_name").toString();
        }
        
        if (map.containsKey("id_element")) {
            Integer e = Integer.parseInt(map.get("id_element").toString());
            element = new GenericController<>(Element.class).get(e);
        }
        
        if (map.containsKey("vl_uses")) {
            uses = Integer.parseInt(map.get("vl_uses").toString());
        }
        
        if (map.containsKey("vl_strenght")) {
            strenght = Float.parseFloat(map.get("vl_strenght").toString());
        }
    }

    @Override
    public Map<String, String> toParams() {
        Map<String, String> params = new HashMap<>();
        
        params.put("ds_name", String.format("\'%s\'", name));
        params.put("id_element", element.getId().toString());
        params.put("vl_uses", uses.toString());
        params.put("vl_strenght", strenght.toString());
        
        return params;
    }
}
