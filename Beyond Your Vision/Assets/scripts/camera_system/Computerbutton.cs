using UnityEngine;

public class Computerbutton : MonoBehaviour, IInteractable
{
    public Cameras cameras;

    public string Prompt => "Use computer";

    public void Interact()
    {
        if (cameras == null)
        {
            Debug.LogWarning("The Cameras :", this);
            return;
        }

        cameras.Toggle();
    }
}
