using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public Inventory playerInventory;
    public Recipe recipe;
    public void Craft()
    {
        if (recipe.ingredients1 == playerInventory.ingredients[0] && recipe.ingredients2 == playerInventory.ingredients[1])
        {
            playerInventory.ClearInventory();
            playerInventory.ingredients[0] = recipe.output;
        }
    }
}
