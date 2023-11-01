using System;
using System.Collections.Generic;

public class ObjectPoolGeneric<T>
{
    private Dictionary<T, Item> pooledItems = new Dictionary<T, Item>();

    public T GetItemFromPool()
    {
        foreach (var pooledItem in pooledItems)
        {
            if (!pooledItem.Value.IsUsed)
            {
                pooledItem.Value.IsUsed = true;
                return pooledItem.Key;
            }
        }

        T item = CreateItem();
        Item newItem = new Item();
        newItem.IsUsed = true;
        pooledItems[item] = newItem;

        return item;
    }

    public void ReturnItem(T itemToReturn)
    {
        if (pooledItems.TryGetValue(itemToReturn, out var pooledItem))
        {
            pooledItem.IsUsed = false;
            Console.WriteLine("Item returned to the pool: " + itemToReturn);
        }
    }

    protected virtual T CreateItem()
    {
        return default(T);
    }

    public void Initialize(T item)
    {
        // item will be used to assign the member variable in child classes
    }

    public void CleanupUnusedItems()
    {
        var unusedItems = new List<T>();
        foreach (var kvp in pooledItems)
        {
            if (!kvp.Value.IsUsed)
            {
                unusedItems.Add(kvp.Key);
            }
        }

        foreach (var item in unusedItems)
        {
            pooledItems.Remove(item);
        }
    }

    private class Item
    {
        public bool IsUsed;
    }
}