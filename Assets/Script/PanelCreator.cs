using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreator : MonoBehaviour
{
    public GameObject canvasParent; // Le Canvas où les panneaux seront ajoutés
    public InventorySlot buttonPrefab; // Le prefab du bouton
    private int panelCount = 0; // Compteur pour identifier les panneaux créés
    public Ingredients item;

    public GameObject CreatePanel()
    {
        panelCount++;

        // === Créer un nouveau panneau ===
        GameObject panel = new GameObject("Panel_" + panelCount, typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(canvasParent.transform, false);

        // Configurer le RectTransform
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(0, 0); // Taille du panneau
        panelRect.anchoredPosition = new Vector2(0, 250); // Décalage vertical
        panelRect.anchorMin = new Vector2(0, 0.5f); // Ancrage à gauche
        panelRect.anchorMax = new Vector2(0, 0.5f); // Ancrage à gauche
        panelRect.pivot = new Vector2(0, 0.5f); // Pivot à gauche du panel

        // Configurer l’image de fond
        Image panelImage = panel.GetComponent<Image>();
        panelImage.color = new Color(255, 255, 255, 100); // Gris transparent

        // Ajouter un GridLayoutGroup
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(100, 100);
        grid.spacing = new Vector2(20, 20);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 15;
        grid.padding = new RectOffset(20,0,20,0);

        // === Générer des boutons dynamiques ===
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
