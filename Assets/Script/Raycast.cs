using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour
{
    [SerializeField] private RectTransform crosshairUI;
    private Canvas targetCanvas;
    [SerializeField] private InputActionReference ClickActionRef;

    [SerializeField] private Button button1;      
    [SerializeField] private Button button2;

    private PickableItem currentPickableItem;


    [SerializeField] private GameObject leftHandCanvas;
    [SerializeField] private GameObject rightHandCanvas;
    [SerializeField] private Image LeftHandImage;
    [SerializeField] private Image RightHandImage;

    [SerializeField] private Player player;

    private GameObject hitObject;
    private GameObject lastObject;

    private void Start()
    {
        ClickActionRef.action.performed += OnClick;
        ClickActionRef.action.canceled += OnClick;
    }

    void OnEnable()
    {
        ClickActionRef.action.Enable();
    }

    void OnDisable()
    {
        ClickActionRef.action.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (crosshairUI == null)
        {
            Debug.LogWarning("CrosshairUI n'est pas assigné !");
            return;
        }

        Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshairUI.position);

        Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Pickable", "Interactable")))
        {
            hitObject = hitInfo.transform.gameObject;

            if (hitObject.layer == LayerMask.NameToLayer("Pickable"))
            {
                targetCanvas = hitObject.GetComponentInChildren<Canvas>(true);
                currentPickableItem = hitObject.GetComponent<PickableItem>();
                ShowCanva();
            }
            if (hitObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                InteractableOject();
            }
        }
    }

    private void InteractableOject()
    {
        Furniture furniture = hitObject.GetComponentInChildren<Furniture>();
        Bowl bowl = hitObject.GetComponentInChildren<Bowl>();
        RecipeBookManager recipeBook = hitObject.GetComponentInChildren<RecipeBookManager>();
        Crate magicCrate = hitObject.GetComponentInChildren<Crate>();
        if (furniture != null)
        {
            furniture.ShowInventory();
        }
        if (bowl != null)
        {
            bowl.Craft();
        }
        if (recipeBook != null)
        {
            recipeBook.OpenBook();
        }
        if (magicCrate != null)
        {
            magicCrate.ShowInventory();
        }
    }

    private void ShowCanva()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        targetCanvas.gameObject.SetActive(true);
    }

    private void CloseCanvas()
    {
        targetCanvas.gameObject.SetActive(false); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LeftHand()
    {
        Debug.Log("Action : Main gauche !");
        Debug.Log($"Action Main Gauche sur : {currentPickableItem.name}");
        MoveItemToHand(LeftHandImage);
        player.playerInventory.ingredients[0] = currentPickableItem.ingredients;
        CloseCanvas();
    }

    public void RightHand()
    {
        Debug.Log("Action : Main Droite !");
        Debug.Log($"Action Main Droite sur : {currentPickableItem.name}");
        MoveItemToHand(RightHandImage);
        player.playerInventory.ingredients[1] = currentPickableItem.ingredients;
        CloseCanvas();
    }

    private void MoveItemToHand(Image handImage)
    {
        if (handImage != null && currentPickableItem != null)
        {
            handImage.sprite = currentPickableItem.sprite;

            handImage.transform.SetParent(handImage.transform.root, false);
        }
    }
    private void Update()
    {
        Vector2 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshairUI.position);

        Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 10f, LayerMask.GetMask("Pickable", "Interactable")))
        {
            GameObject hitParentObject = hitInfo.transform.root.gameObject; 

            
            if (lastObject != null && lastObject != hitParentObject)
            {
                ResetObjectColor(lastObject);
            }

            SetObjectColor(hitParentObject, Color.red);

            lastObject = hitParentObject;
        }
        else
        {
            if (lastObject != null)
            {
                ResetObjectColor(lastObject);
                lastObject = null;
            }
        }
    }

    private void SetObjectColor(GameObject obj, Color color)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    private void ResetObjectColor(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.white;
        }
    }
}