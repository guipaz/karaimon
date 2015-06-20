package controllers;

import java.util.List;

public interface ObjectControllerInterface<T> {
    public List<T> get(String... conditions);
    public T get(Integer id);
    public T insert(T object);
    public void update(T object, int id);
    public void delete(Integer id);
}
