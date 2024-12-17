using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/Recipe")]
public class Recipe : ScriptableObject
{
    public Ingredients ingredients1;
    public Ingredients ingredients2;
    public Ingredients output;
}
