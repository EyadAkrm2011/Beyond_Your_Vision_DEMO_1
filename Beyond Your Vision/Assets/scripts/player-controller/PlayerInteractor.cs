using UnityEngine;
using TMPro;
public class PlayerInteractor : MonoBehaviour
{
    public float reachDistance = 3f;
    public TextMeshProUGUI interactionText;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            IInteractable item = hit.collider.GetComponent<IInteractable>();
            if (item != null)
            {
                interactionText.text = "Press E to " + item.Prompt;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    item.Interact();
                }
                return;
            }
        }
        interactionText.text = "";
    }
}