using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    public FnafDoor door;

    public string Prompt => "Use door";

    public void Interact()
    {
        if (door == null)
        {
            Debug.LogWarning("The Door : ", this);
            return;
        }
        door.Toggle();
    }
}