using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("ActionRef Settings")]
    [SerializeField] private InputActionReference PlayerActionRef;
    [SerializeField] private InputActionReference LookActionRef;

    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 2f;
    public Vector2 maxLookAngle = new Vector2(-80f, 80f);
    public Vector2 currentRotation;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Camera playerCamera;

    [Header("Inventory Settings")]
    public Inventory playerInventory;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        PlayerActionRef.action.performed += OnMove;
        PlayerActionRef.action.canceled += OnMove;
        LookActionRef.action.performed += OnLook;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        LookAround();
    }

    private void OnEnable()
    {
        PlayerActionRef.action.Enable();
        LookActionRef.action.Enable();
    }

    private void OnDisable()
    {
        PlayerActionRef.action.Disable();
        LookActionRef.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Move()
    {

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
    }

    private void LookAround()
    {
        // Lire la souris (delta mouvement de la souris)
        float lookX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float lookY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Appliquer la rotation horizontale au joueur (autour de Y)
        currentRotation.x += lookX;

        // Appliquer la rotation verticale à la caméra (autour de X), limité avec Clamp
        currentRotation.y -= lookY;
        currentRotation.y = Mathf.Clamp(currentRotation.y, maxLookAngle.x, maxLookAngle.y);

        // Appliquer la rotation horizontale au joueur
        transform.localRotation = Quaternion.Euler(0, currentRotation.x, 0);

        // Appliquer la rotation verticale à la caméra (la caméra peut seulement bouger verticalement)
        playerCamera.transform.localRotation = Quaternion.Euler(currentRotation.y, 0, 0);
    }
}

