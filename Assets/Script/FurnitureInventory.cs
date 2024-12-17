using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInventory : MonoBehaviour
{
    public int maxCapacity = 10;
    public List<Ingredients> ingredients;

    public bool AddItem(Ingredients ingredient)
    {
        if (ingredients.Count < maxCapacity)
        {
            ingredients.Add(ingredient);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(Ingredients ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
        }
    }
}
