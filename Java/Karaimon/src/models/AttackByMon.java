package models;

import java.util.HashMap;
import java.util.Map;
import lombok.Data;

@Data
public class AttackByMon implements DBObject {
    
    public Integer id;
    public Integer monId;
    public Integer attackId;

    @Override
    public void parse(Map<String, Object> map) {
        if (map.containsKey("id")) {
            id = Integer.parseInt(map.get("id").toString());
        }
        
        if (map.containsKey("id_mon")) {
            monId = Integer.parseInt(map.get("id_mon").toString());
        }
        
        if (map.containsKey("id_attack")) {
            attackId = Integer.parseInt(map.get("id_attack").toString());
        }
    }

    @Override
    public Map<String, String> toParams() {
        Map<String, String> params = new HashMap<>();
        
        params.put("id_mon", monId.toString());
        params.put("id_attack", attackId.toString());
        
        return params;
    }
    
}
