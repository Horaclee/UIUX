using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCrate : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Transform etagereGrid;

    [SerializeField] private Button shelfSlotPrefab; 
    [SerializeField] private InventorySlot objectSlotPrefab;

    [SerializeField] private Canvas cratePanel;

    [SerializeField] private Ingredients item;

    [Header("Randomization Settings")]
    [SerializeField] private int minShelves = 1;
    [SerializeField] private int maxShelves = 9;
    [SerializeField] private int minObjectsPerShelf = 1;
    [SerializeField] private int maxObjectsPerShelf = 90;

    public static bool isCrateOpen = false;

    private bool isInventoryGenerated = false;

    [SerializeField] private PanelCreator panelcreator;

    private Dictionary<Button, GameObject> buttonPanelMap = new Dictionary<Button, GameObject>();

    private GameObject currentActivePanel;
    public GameObject basePanel;
    private List<GameObject> createdPanels = new List<GameObject>();

    public void OpenCrate()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cratePanel.gameObject.SetActive(true);
        isCrateOpen = true;

        if (!isInventoryGenerated)
        {
            GenerateCrate();
            isInventoryGenerated = true;
        }
    }

    public void CloseCrate()
    {
        cratePanel.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCrateOpen = false;
        isInventoryGenerated = false;

        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
        }

        // D�truire tous les panneaux cr��s
        foreach (var panel in createdPanels)
        {
            Destroy(panel);
        }

        // Vider la liste des panneaux cr��s
        createdPanels.Clear();
    }

    public void GenerateCrate()
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in etagereGrid)
        {
            Destroy(child.gameObject);
        }

        int shelfCount = Random.Range(minShelves, maxShelves + 1);
        for (int i = 0; i < shelfCount; i++)
        {
            Button slot = Instantiate(shelfSlotPrefab, etagereGrid);

            Button slotButton = slot.GetComponentInChildren<Button>(); // Si le Button est enfant de InventorySlot

            // Ajouter la m�thode CreatePanel � l'�v�nement OnClick du Button
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() => OnButtonClick(slotButton));  // Ajouter CreatePanel � l'�v�nement OnClick
            }
            if (i == 0)
            {
                // Cr�er un panneau de base pour le premier bouton
                buttonPanelMap[slotButton] = basePanel;

                currentActivePanel = basePanel;
                basePanel.SetActive(true);
            }
        }
        GeneratePanel();
    }

    public void GeneratePanel()
    {
        int objectCount = Random.Range(minObjectsPerShelf, maxObjectsPerShelf + 1);
        for (int i = 0; i < objectCount; i++ )
        {
            InventorySlot slot = Instantiate(objectSlotPrefab, itemGrid);
            slot.SetItem(item);
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
        }

        // V�rifier si ce bouton a d�j� un panneau associ�
        if (buttonPanelMap.ContainsKey(clickedButton))
        {
            // Afficher le panneau associ� au bouton
            currentActivePanel = buttonPanelMap[clickedButton];
            currentActivePanel.SetActive(true);
        }
        else
        {
            // Cr�er un nouveau panneau pour ce bouton
            GameObject newPanel = panelcreator.CreatePanel();

            // Associer ce panneau au bouton
            buttonPanelMap[clickedButton] = newPanel;

            // Afficher le nouveau panneau
            currentActivePanel = newPanel;
            currentActivePanel.SetActive(true);
            createdPanels.Add(newPanel);
        }
    }
}
