using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private FnafDoor door;          
    [SerializeField] private Renderer indicator;     
    [SerializeField] private Color openColor = Color.green;
    [SerializeField] private Color closedColor = Color.red;

    public string Prompt => (door != null && door.IsOpen) ? "Close door" : "Open door";

    private void Awake()
    {
        if (indicator == null) indicator = GetComponent<Renderer>();
    }

    private void Start()
    {
        UpdateIndicator();
    }

    public void Interact()
    {
        if (door == null) return;
        door.Toggle();
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (indicator == null || door == null) return;
        indicator.material.color = door.IsOpen ? openColor : closedColor;
    }
}
