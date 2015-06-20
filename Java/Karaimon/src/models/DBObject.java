package models;

import java.util.List;
import java.util.Map;

public interface DBObject {
    public void parse(Map<String, Object> map);
    public Map<String, String> toParams();
}
