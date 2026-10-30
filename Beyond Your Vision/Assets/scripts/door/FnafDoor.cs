using UnityEngine;
public class FnafDoor : MonoBehaviour
{
    [SerializeField] private Vector3 off = new Vector3(0f, 3f, 0f);
    [SerializeField] private float speed = 6f;
    [SerializeField] private bool start = true;

    public bool IsOpen { get; private set; }

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Awake()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + off;

        IsOpen = start;
        if (IsOpen) transform.localPosition = openPosition;
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        Debug.Log("Door toggled, IsOpen = " + IsOpen, this);
        if (TryGetComponent(out AudioSource sound)) sound.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 target = IsOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, target, speed * Time.deltaTime);
    }
}