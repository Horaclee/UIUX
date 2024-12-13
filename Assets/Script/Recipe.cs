using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredients> elements;
    public Ingredients output;
}
