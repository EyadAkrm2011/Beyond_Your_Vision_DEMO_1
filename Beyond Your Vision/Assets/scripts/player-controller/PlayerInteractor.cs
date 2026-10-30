using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private TextMeshProUGUI interactionText;

    private Transform cam;
    private IInteractable currentInteractable;

    void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogError("no cam");
        }
        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }

    void Update()
    {
        if (cam == null) return;
        IInteractable itemFound = null;
        RaycastHit hit;

        Vector3 rayOrigin = cam.position + (cam.forward * 0.3f);

        if (Physics.Raycast(rayOrigin, cam.forward, out hit, reachDistance))
        {
            itemFound = hit.collider.GetComponentInParent<IInteractable>();
        }

        currentInteractable = itemFound;

        if (interactionText != null)
        {
            if (currentInteractable != null)
            {
                interactionText.text = "Press E to " + currentInteractable.Prompt;
            }
            else
            {
                interactionText.text = "";
            }
        }

        if (currentInteractable != null)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("E key pressed!");
                currentInteractable.Interact();
            }
        }
    }
}