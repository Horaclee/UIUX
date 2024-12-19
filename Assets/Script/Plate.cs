using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public Image leftHandImage;
    public Image rightHandImage;
    public Sprite rightHandDefaultSprite;
    public Inventory playerInventory;
    public Recipe recipe;
    public void Craft()
    {
        if (recipe.ingredients1 == playerInventory.ingredients[0] && recipe.ingredients2 == playerInventory.ingredients[1])
        {
            playerInventory.ClearInventory();
            playerInventory.ingredients[0] = recipe.output;
            ChangeHandsSprite();
            ResetRightHandSprite();
        }
    }

    private void ChangeHandsSprite()
    {
        if (leftHandImage != null && recipe.output != null)
        {
            // Assigner le sprite de l'élément de sortie à l'image des mains
            leftHandImage.sprite = recipe.output.Sprite;  // En supposant que 'recipe.output' a un champ 'sprite'
        }
    }

    private void ResetRightHandSprite()
    {
        if (rightHandImage != null && rightHandDefaultSprite != null)
        {
            // Réinitialiser le sprite de la main gauche
            rightHandImage.sprite = rightHandDefaultSprite;
        }
    }
}
