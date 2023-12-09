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

    // For optimizing the the memory usage during runtime by removing Unused items. For example : Let's say Player is in a specific area of the level and is has specific type of enemies
    // and we are spawning enemies from the pool. let's say we created pool of 10 enemies. after some time player is in the different area of the level now 
    // it has different type of enemies than before now we are spawing enemy from the pool and this time these are different enemies and let's say we created 
    // 15 enemies this time. Now enemies that we created before are no longer in need but still occupying space in the memory which need to be get rid of to optimizing the memory usage.
    // this is what this fuction is doing. will be used to remove unused items.
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