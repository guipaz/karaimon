package controllers;

import com.google.common.base.CaseFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.logging.Level;
import java.util.logging.Logger;
import lombok.Data;
import models.DBObject;

@Data
public class GenericController<T> implements ObjectControllerInterface<T> {

    Class<T> c;
    String tableName;
    
    public GenericController(Class<T> c) {
        this.c = c;
        this.tableName = c.getSimpleName();
        this.tableName = CaseFormat.UPPER_CAMEL.to(CaseFormat.LOWER_UNDERSCORE, tableName);
    }
    
    public List<T> get(String... conditions) {
        String query = String.format("SELECT * FROM %s", tableName);
        if (conditions != null && conditions.length > 0) {
            query += " WHERE ";
        }
        
        for (int i = 0; i < conditions.length; i++) {
            String condition = conditions[i];
            query += condition;
            if (i + 1 < conditions.length)
                query += ", ";
        }
        
        List<Map<String, Object>> mapList = DatabaseController.executeQuery(query);
        List<T> list = new ArrayList<>();
        for (Map<String, Object> map : mapList) {
            if (map != null && !map.isEmpty()) {
                T object = null;
                try {
                    object = c.newInstance();
                    ((DBObject)object).parse(map);
                    list.add(object);
                } catch (Exception ex) {
                    Logger.getLogger(GenericController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
        
        return list;
    }
    
    @Override
    public T get(Integer id) {
        String query = String.format("SELECT * FROM %s WHERE id = %d", tableName, id);
        List list = DatabaseController.executeQuery(query);
        if (list.isEmpty())
            return null;
        
        Map<String, Object> map = (Map<String, Object>) list.get(0);
        T object = null;
        if (map != null && !map.isEmpty()) {
            try {
                object = c.newInstance();
                ((DBObject)object).parse(map);
            } catch (Exception ex) {
                Logger.getLogger(GenericController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

        return object;
    }

    @Override
    public T insert(T object) {
        Map<String, String> params = ((DBObject)object).toParams();
        String keys = "";
        String values = "";
        
        for (Entry<String, String> e : params.entrySet()) {
            if (!keys.isEmpty())
                keys += ", ";
            
            if (!values.isEmpty())
                values += ", ";
            
            keys += e.getKey();
            values += e.getValue();
        }
        
        String query = String.format("INSERT INTO %s (%s) VALUES (%s)", tableName, keys, values);
        DatabaseController.executeQuery(query);
        Map<String, Object> obj = DatabaseController.executeQuery(String.format("SELECT * FROM %s WHERE id = %d", tableName, DatabaseController.lastInsertRowId)).get(0);
        
        T newObject = null;
        try {
            newObject = c.newInstance();
            ((DBObject)newObject).parse(obj);
        } catch (Exception ex) {
            Logger.getLogger(GenericController.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        return newObject;
    }

    @Override
    public void update(T object, int id) {
        Map<String, String> params = ((DBObject)object).toParams();
        String query = String.format("UPDATE %s SET ", tableName);
        
        for (Entry<String, String> e : params.entrySet()) {
            query += String.format("%s=%s,", e.getKey(), e.getValue());
        }
        
        query = query.substring(0, query.length() - 1);
        query += String.format(" WHERE id = %d", id);
        
        DatabaseController.executeQuery(query);
    }

    @Override
    public void delete(Integer id) {
        String query = String.format("DELETE FROM %s WHERE id = %d", tableName, id);
        DatabaseController.executeQuery(query);
    }
}
