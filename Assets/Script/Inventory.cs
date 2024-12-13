using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Ingredients> ingredients;
}
