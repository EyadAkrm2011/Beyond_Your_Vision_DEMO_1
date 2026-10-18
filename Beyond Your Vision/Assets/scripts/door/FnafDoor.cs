using UnityEngine;

public class FnafDoor : MonoBehaviour
{
    [Tooltip("How far the door moves when OPEN (raised). Should be at least as tall as the door.")]
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 3f, 0f);
    [SerializeField] private float speed = 6f;
    [SerializeField] private bool startOpen = true;
    public bool IsOpen { get; private set; }

    private Vector3 closedPosition;
    private Vector3 openPosition;  
    private void Awake()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openOffset;
        IsOpen = startOpen;
        if (IsOpen) transform.localPosition = openPosition;
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        if (TryGetComponent(out AudioSource sound)) sound.Play();
    }

    private void Update()
    {
        Vector3 target = IsOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, target, speed * Time.deltaTime);
    }
}
