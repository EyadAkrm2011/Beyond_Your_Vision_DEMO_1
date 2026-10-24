using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    public float reachDistance = 3f;
    public TextMeshProUGUI interactionText;
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
        if (Physics.Raycast(cam.position, cam.forward, out hit, reachDistance))
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
                currentInteractable.Interact();
            }
        }
    }
}