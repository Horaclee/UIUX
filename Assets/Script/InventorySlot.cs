using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; 
    [SerializeField] private Button slotButton; 

    private Ingredients currentItem; 

    public void SetItem(Ingredients item)
    {
        currentItem = item;
        if (item != null)
        {
            itemImage.sprite = item.Sprite;  
            itemImage.gameObject.SetActive(true);  
        }
        else
        {
            itemImage.gameObject.SetActive(false);  
        }
    }

    public void SetItem()
    {

    }

    public void OnClick()
    {
        if (currentItem != null)
        {
            Debug.Log("Item sélectionné : " + currentItem.name);
            
        }
    }
}
