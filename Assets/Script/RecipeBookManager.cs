using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI leftPageRecipeNameText;
    public TextMeshProUGUI leftPageIngredientsText;
    public TextMeshProUGUI leftPageDescText;
    public TextMeshProUGUI rightPageRecipeNameText;
    public TextMeshProUGUI rightPageIngredientsText;
    public TextMeshProUGUI rightPageDescText;

    [Header("Recipes")]
    public RecipeList recipeList;

    private int currentIndex = 0;

    void Start()
    {
        // Affiche les deux premières pages au démarrage
        UpdateUI();
    }

    public void OpenBook()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextPage()
    {
        if (recipeList.recipes.Count == 0) return;

        // Passe à la page suivante (deux recettes de plus)
        currentIndex += 2;
        if (currentIndex >= recipeList.recipes.Count) currentIndex = 0; // Reboucle au début si dépassement

        UpdateUI();
    }

    public void PreviousPage()
    {
        if (recipeList.recipes.Count == 0) return;

        // Reculer de deux recettes
        currentIndex -= 2;
        if (currentIndex < 0) currentIndex = Mathf.Max(0, recipeList.recipes.Count - 2); // Reboucle à la fin si dépassement

        UpdateUI();
    }

    public void CloseBook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UpdateUI()
    {
        if (recipeList.recipes.Count == 0)
        {
            // Pas de recettes, affichage par défaut
            leftPageRecipeNameText.text = "Pas de recette";
            leftPageIngredientsText.text = "";
            rightPageRecipeNameText.text = "Pas de recette";
            rightPageIngredientsText.text = "";
            return;
        }

        // Affiche la recette de la page gauche
        Recipe leftPageRecipe = recipeList.recipes[currentIndex];
        leftPageRecipeNameText.text = leftPageRecipe.name;
        leftPageIngredientsText.text = leftPageRecipe.ingredients1.name + " et " + leftPageRecipe.ingredients2.name;
        leftPageDescText.text = leftPageRecipe.desc;
        

        // Affiche la recette de la page droite si disponible
        if (currentIndex + 1 < recipeList.recipes.Count)
        {
            Recipe rightPageRecipe = recipeList.recipes[currentIndex + 1];
            rightPageRecipeNameText.text = rightPageRecipe.name;
            rightPageIngredientsText.text = rightPageRecipe.ingredients1.name + " et " + rightPageRecipe.ingredients2.name;
            rightPageDescText.text = rightPageRecipe.desc;
        }
        else
        {
            // Si aucune recette pour la page droite
            rightPageRecipeNameText.text = "Pas de recette";
            rightPageIngredientsText.text = "";
            rightPageDescText.text = "";
        }
    }
}
