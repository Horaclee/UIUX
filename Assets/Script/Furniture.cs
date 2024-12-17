using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private InventoryUi inventoryUi; 
    [SerializeField] private FurnitureInventory furnitureInventory;

    public void ShowInventory()
    {
        if (inventoryUi != null)
        {
            
            inventoryUi.OpenInventory(furnitureInventory);
        }
    }

    public void HideInventory()
    {
        if (inventoryUi != null)
        {
            inventoryUi.CloseInventory();
        }
    }
}
