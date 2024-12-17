using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public Canvas inventoryPanel;
    public Transform itemGrid;       
    public InventorySlot itemSlot;

    public Transform playerGrid;

    private FurnitureInventory currentInventory;
    public Inventory playerInventory;

    public void OpenInventory(FurnitureInventory inventory)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        currentInventory = inventory;
        inventoryPanel.gameObject.SetActive(true);
        RefreshInventory();
    }

    public void CloseInventory()
    {
        inventoryPanel.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void RefreshInventory()
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in playerGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (Ingredients item in currentInventory.ingredients)
        {
            InventorySlot slot = Instantiate(itemSlot, itemGrid);
            slot.SetItem(item);
        }

        foreach(Ingredients item in playerInventory.ingredients)
        {
            InventorySlot slot = Instantiate(itemSlot, playerGrid);
            slot.SetItem(item);
        }
    }
}
