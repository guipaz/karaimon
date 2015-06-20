package controllers;

import com.almworks.sqlite4java.SQLiteConnection;
import com.almworks.sqlite4java.SQLiteException;
import com.almworks.sqlite4java.SQLiteStatement;
import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import lombok.Data;

@Data
public class DatabaseController {
    
    static int lastInsertRowId = -1;
    
    public void testConnection() throws SQLiteException {
        List<Map<String, Object>> list = executeQuery("SELECT * FROM element");
        for (Map<String, Object> obj : list) {
            System.out.println(obj.get("id") + " - " + obj.get("ds_name"));
        }
        
    }
    
    public static List<Map<String, Object>> executeQuery(String query) {
        List<Map<String, Object>> list = new ArrayList<>();
        SQLiteStatement st = null;
        SQLiteStatement st2 = null;
        SQLiteConnection connection = null;
        try {
            connection = getConnection();
            st = connection.prepare(query);
            while (st.step()) {
                Map<String, Object> map = getObjects(st);
                if (map != null && !map.isEmpty())
                    list.add(map);
            }
            
            if (query.contains("INSERT")) {
                st2 = connection.prepare("SELECT last_insert_rowid()");
                if (st2.step()) {
                    lastInsertRowId = st2.columnInt(0);
                }
            }
        } catch (SQLiteException ex) {
            
        } finally {
            if (st != null)
                st.dispose();
            
            if (st2 != null)
                st2.dispose();
        }
        
        return list;
    }
    
    static SQLiteConnection getConnection() throws SQLiteException {
        SQLiteConnection db = new SQLiteConnection(new File("../../Unity/Assets/Scripts/SQL/Databases/karaimon.db"));
        db.open(true);
        return db;
    }
    
    static Map<String, Object> getObjects(SQLiteStatement st) throws SQLiteException {
        Map<String, Object> map = new HashMap<>();
        
        for (int i = 0; i < st.columnCount(); i++) {
            Object obj = st.columnValue(i);
            if (obj != null) {
                map.put(st.getColumnName(i), obj);
            }
        }
        
        return map;
    }
    
}
