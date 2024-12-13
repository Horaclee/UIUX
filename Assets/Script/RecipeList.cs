using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/RecipeList")]
public class RecipeList : ScriptableObject
{
   public List<Recipe> recipes;
}
