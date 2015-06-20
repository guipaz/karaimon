package models;

import controllers.GenericController;
import java.util.HashMap;
import java.util.Map;
import lombok.Data;

@Data
public class Karaimon implements DBObject {
    Integer id;
    String name;
    Element element;
    AttributeSheet attributes;

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
        
        if (map.containsKey("id_attribute_sheet")) {
            Integer e = Integer.parseInt(map.get("id_attribute_sheet").toString());
            attributes = new GenericController<>(AttributeSheet.class).get(e);
        }
    }

    @Override
    public Map<String, String> toParams() {
        Map<String, String> params = new HashMap<>();
        
        params.put("ds_name", String.format("\'%s\'", name));
        params.put("id_attribute_sheet", attributes.getId().toString());
        params.put("id_element", element.getId().toString());
        
        return params;
    }
}
