using UnityEngine;
public class FuseBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] lightsToCut;

    public bool IsBroken { get; private set; }

    public string Prompt => IsBroken ? "Fix fuse box" : "Fuse box (working)";

    public void Break()
    {
        IsBroken = true;
        SetLights(false);
    }

    public void Interact()
    {
        if (!IsBroken) return;
        IsBroken = false;
        SetLights(true);
    }

    private void SetLights(bool on)
    {
        foreach (GameObject lightObject in lightsToCut)
        {
            if (lightObject != null) lightObject.SetActive(on);
        }
    }
}
