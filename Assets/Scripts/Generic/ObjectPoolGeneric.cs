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
                MarkItemAsUsed(pooledItem.Key);
                return pooledItem.Key;
            }
        }

        T newItem = CreateAndInitializeItem();
        MarkItemAsUsed(newItem);

        return newItem;
    }

    public void ReturnItem(T itemToReturn)
    {
        if (pooledItems.TryGetValue(itemToReturn, out var pooledItem))
        {
            MarkItemAsUnused(itemToReturn);
            Console.WriteLine("Item returned to the pool: " + itemToReturn);
        }
    }

    public void CleanupUnusedItems()
    {
        var unusedItems = FindUnusedItems();
        RemoveUnusedItems(unusedItems);
    }

    protected virtual T CreateItem()
    {
        return default(T);
    }

    private void MarkItemAsUsed(T item)
    {
        pooledItems[item].IsUsed = true;
    }

    private void MarkItemAsUnused(T item)
    {
        pooledItems[item].IsUsed = false;
    }

    private T CreateAndInitializeItem()
    {
        T newItem = CreateItem();
        Initialize(newItem);
        pooledItems[newItem] = new Item { IsUsed = true };
        return newItem;
    }

    private List<T> FindUnusedItems()
    {
        var unusedItems = new List<T>();
        foreach (var kvp in pooledItems)
        {
            if (!kvp.Value.IsUsed)
            {
                unusedItems.Add(kvp.Key);
            }
        }
        return unusedItems;
    }

    private void RemoveUnusedItems(List<T> unusedItems)
    {
        foreach (var item in unusedItems)
        {
            pooledItems.Remove(item);
        }
    }

    public void Initialize(T item)
    {
        // item will be used to assign the member variable in child classes
    }

    private class Item
    {
        public bool IsUsed;
    }
}
