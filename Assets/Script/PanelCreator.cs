using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreator : MonoBehaviour
{
    public GameObject canvasParent; // Le Canvas o� les panneaux seront ajout�s
    public InventorySlot buttonPrefab; // Le prefab du bouton
    private int panelCount = 0; // Compteur pour identifier les panneaux cr��s
    public Ingredients item;

    public GameObject CreatePanel()
    {
        panelCount++;

        // === Cr�er un nouveau panneau ===
        GameObject panel = new GameObject("Panel_" + panelCount, typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(canvasParent.transform, false);

        // Configurer le RectTransform
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(0, 0); // Taille du panneau
        panelRect.anchoredPosition = new Vector2(0, 250); // D�calage vertical
        panelRect.anchorMin = new Vector2(0, 0.5f); // Ancrage � gauche
        panelRect.anchorMax = new Vector2(0, 0.5f); // Ancrage � gauche
        panelRect.pivot = new Vector2(0, 0.5f); // Pivot � gauche du panel

        // Configurer l�image de fond
        Image panelImage = panel.GetComponent<Image>();
        panelImage.color = new Color(255, 255, 255, 100); // Gris transparent

        // Ajouter un GridLayoutGroup
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(100, 100);
        grid.spacing = new Vector2(20, 20);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 15;
        grid.padding = new RectOffset(20,0,20,0);

        // === G�n�rer des boutons dynamiques ===
        int objectCount = Random.Range(1, 50 + 1);
        for (int i = 0; i < objectCount; i++)
        {
            InventorySlot button = Instantiate(buttonPrefab, panelRect); // Instancier une copie du prefab
            button.name = "Button_" + (i + 1);
            button.transform.SetParent(panel.transform, false); // Ajouter au panel

            button.SetItem(item);
        }
        return panel;
    }
}
