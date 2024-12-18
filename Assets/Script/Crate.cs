using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private MagicCrate crateInventory;

    public void ShowInventory()
    {
        if (crateInventory != null)
        {

            crateInventory.OpenCrate();
        }
    }

    public void HideInventory()
    {
        if (crateInventory != null)
        {
            crateInventory.CloseCrate();
        }
    }
}
