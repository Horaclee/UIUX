using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCrate : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Transform etagereGrid;

    [SerializeField] private InventorySlot shelfSlotPrefab; 
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
            InventorySlot slot = Instantiate(shelfSlotPrefab, etagereGrid);
            slot.SetItem(item);
        }
        int objectCount = Random.Range(minObjectsPerShelf, maxObjectsPerShelf + 1);
        for (int i = 0; i < objectCount; i++ )
        {
            InventorySlot slot = Instantiate(objectSlotPrefab, itemGrid);
            slot.SetItem(item);
        }

    }
}
