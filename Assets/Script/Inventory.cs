using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Ingredients> ingredients;
    public void ClearInventory()
    {
        ingredients.Clear();
        if (ingredients.Count == 0)
        {
            ingredients = new List<Ingredients>(2);
        }

        while (ingredients.Count < 2)
        {
            ingredients.Add(null);
        }
    }
}
