using System.Collections.Generic;
using UnityEngine;

public class ItemPool<T> where T : MonoBehaviour
{
    private Queue<T> items = new Queue<T>();
    private GameObject prefab;
    private Transform pool;
    public ItemPool(GameObject prefab, int initialCount) {
        
        this.prefab = prefab;
        GameObject go = new GameObject("ItemPool_" + typeof(T));
        pool = go.transform;
        for(int i = 0; i < initialCount; i++)
        {
            T item = Create();
            items.Enqueue(item);
        }
    }

    private T Create()
    {
        GameObject obj = Object.Instantiate(prefab, pool) as GameObject;
        T item = obj.GetComponent<T>();
        item.gameObject.SetActive(false);
        return item;
    }

    public T Borrow()
    {
        if(items.Count > 0)
        {
            return items.Dequeue();
        }    
        else
        {
            return Create();
        }
    }

    public void Return(T item)
    {
        item.transform.SetParent(pool);
        item.gameObject.SetActive(false);
        items.Enqueue(item);
    }
}

