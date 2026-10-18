using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform; 

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private float gravity = -20f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float maxLookAngle = 80f;

    [Header("Head Bob")]
    [SerializeField] private float walkBobSpeed = 9f;
    [SerializeField] private float sprintBobSpeed = 13f;
    [SerializeField] private float walkBobAmount = 0.04f;
    [SerializeField] private float sprintBobAmount = 0.07f;
    [SerializeField] private float bobReturnSpeed = 8f;

    [Header("Extra Shake (driven by other scripts)")]
    [Range(0f, 1f)] public float shakeIntensity = 0f;
    [SerializeField] private float shakeSpeed = 25f;
    [SerializeField] private float maxShakeAngle = 1.5f;  

   
    public bool CanMove { get; set; } = true;

    private CharacterController controller;
    private Vector3 cameraStartLocalPos;
    private float pitch;            
    private float verticalVelocity;   
    private float bobTimer;
    private bool isMoving;
    private bool isSprinting;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraStartLocalPos = cameraTransform.localPosition;
    }

    private void Start()
    {
        SetCursorLocked(true);
    }

    private void Update()
    {
        HandleCursor();

        if (!CanMove) return;

        HandleLook();
        HandleMovement();
        HandleHeadBob();
    }

    
    private void HandleLook()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null || Cursor.lockState != CursorLockMode.Locked) return;

        Vector2 delta = mouse.delta.ReadValue() * mouseSensitivity;
        transform.Rotate(Vector3.up * delta.x);
        pitch = Mathf.Clamp(pitch - delta.y, -maxLookAngle, maxLookAngle);
        float t = Time.time * shakeSpeed;
        float shakeX = (Mathf.PerlinNoise(t, 0f) - 0.5f) * 2f;
        float shakeY = (Mathf.PerlinNoise(t, 10f) - 0.5f) * 2f;
        float shakeZ = (Mathf.PerlinNoise(t, 20f) - 0.5f) * 2f;
        Vector3 shake = new Vector3(shakeX, shakeY, shakeZ) * (maxShakeAngle * shakeIntensity);

        cameraTransform.localRotation = Quaternion.Euler(pitch + shake.x, shake.y, shake.z);
    }

    private void HandleMovement()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        Vector2 input = Vector2.zero;
        if (kb.wKey.isPressed) input.y += 1f;
        if (kb.sKey.isPressed) input.y -= 1f;
        if (kb.dKey.isPressed) input.x += 1f;
        if (kb.aKey.isPressed) input.x -= 1f;
        input = Vector2.ClampMagnitude(input, 1f);

        isSprinting = kb.leftShiftKey.isPressed && input.y > 0.1f;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 move = (transform.right * input.x + transform.forward * input.y) * speed;


        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;  
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);

        isMoving = input.sqrMagnitude > 0.01f && controller.isGrounded;
    }

    private void HandleHeadBob()
    {
        Vector3 target = cameraStartLocalPos;

        if (isMoving)
        {
            float speed = isSprinting ? sprintBobSpeed : walkBobSpeed;
            float amount = isSprinting ? sprintBobAmount : walkBobAmount;

            bobTimer += Time.deltaTime * speed;

            target += new Vector3(
                Mathf.Cos(bobTimer * 0.5f) * amount * 0.5f,
                Mathf.Sin(bobTimer) * amount,
                0f);

            cameraTransform.localPosition = target;
        }
        else
        {

            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition, target, Time.deltaTime * bobReturnSpeed);
        }
    }

    private void HandleCursor()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            SetCursorLocked(false);

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame
            && Cursor.lockState != CursorLockMode.Locked)
            SetCursorLocked(true);
    }

    private void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
