using UnityEngine;

public class DoorButtonnew : MonoBehaviour, IInteractable
{
    public FnafDoor door;

    public string Prompt => "Use door button";

    public void Interact()
    {
        Debug.Log("BUTTON PRESSED", this);

        if (door == null)
        {
            Debug.LogWarning("The Door", this);
            return;
        }

        door.Toggle();
    }
}