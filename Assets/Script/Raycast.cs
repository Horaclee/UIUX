using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
                GameObject hitObject = hitInfo.transform.gameObject;
                targetCanvas = hitObject.GetComponentInChildren<Canvas>(true);
                currentPickableItem = hitObject.GetComponent<PickableItem>();

                ShowCanva();
            if (hitInfo.transform.gameObject.layer == LayerMask.GetMask("Pickable"))
            {
            }
            if (hitInfo.transform.gameObject.layer == LayerMask.GetMask("Interactable"))
            {
                
            }
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
        
    }
}