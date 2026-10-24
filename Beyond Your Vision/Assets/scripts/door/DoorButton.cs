using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    public FnafDoor door;
    public FuseBox fuseBox;

    public string Prompt => "Use door button";

    public void Interact()
    {
        if (door == null)
        {
            Debug.LogWarning("The Door slot is empty on this button!", this);
            return;
        }

        if (fuseBox != null && fuseBox.IsBroken) return;

        door.Toggle();
    }
}