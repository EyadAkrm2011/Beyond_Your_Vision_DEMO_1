using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Cameras : MonoBehaviour
{
    [SerializeField] private GameObject moniter;
    [SerializeField] private Camera player;
    [SerializeField] private GameObject[] security;
    [SerializeField] private TextMeshProUGUI camnumb;

    private bool isMonitorOpen = false;
    private int currentCamIndex = 0;

    void Start()
    {
        CloseMonitor();
    }

    void Update()
    {
        if (isMonitorOpen == false) return;

        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.qKey.wasPressedThisFrame)
        {
            CloseMonitor();
            return;
        }

        CheckCameraInput();
    }

    public void Toggle()
    {
        if (isMonitorOpen)
        {
            CloseMonitor();
        }
        else
        {
            OpenMonitor();
        }
    }

    void OpenMonitor()
    {
        if (security.Length == 0)
        {
            Debug.LogWarning("The array is empty", this);
            return;
        }

        isMonitorOpen = true;

        if (moniter != null) moniter.SetActive(true);
        if (player != null) player.enabled = false;

        SwitchToCamera(currentCamIndex);
    }

    void CloseMonitor()
    {
        isMonitorOpen = false;

        if (moniter != null) moniter.SetActive(false);
        if (player != null) player.enabled = true;

        foreach (GameObject camObj in security)
        {
            if (camObj != null)
            {
                Camera cam = camObj.GetComponentInChildren<Camera>(true);
                if (cam != null) cam.enabled = false;
            }
        }

        if (camnumb != null) camnumb.text = "";
    }

    void CheckCameraInput()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.digit1Key.wasPressedThisFrame || kb.numpad1Key.wasPressedThisFrame) SwitchToCamera(0);
        if (kb.digit2Key.wasPressedThisFrame || kb.numpad2Key.wasPressedThisFrame) SwitchToCamera(1);
        if (kb.digit3Key.wasPressedThisFrame || kb.numpad3Key.wasPressedThisFrame) SwitchToCamera(2);
        if (kb.digit4Key.wasPressedThisFrame || kb.numpad4Key.wasPressedThisFrame) SwitchToCamera(3);
        if (kb.digit5Key.wasPressedThisFrame || kb.numpad5Key.wasPressedThisFrame) SwitchToCamera(4);
        if (kb.digit6Key.wasPressedThisFrame || kb.numpad6Key.wasPressedThisFrame) SwitchToCamera(5);
        if (kb.digit7Key.wasPressedThisFrame || kb.numpad7Key.wasPressedThisFrame) SwitchToCamera(6);
        if (kb.digit8Key.wasPressedThisFrame || kb.numpad8Key.wasPressedThisFrame) SwitchToCamera(7);
    }

    void SwitchToCamera(int index)
    {
        if (index < 0 || index >= security.Length) return;
        currentCamIndex = index;

        foreach (GameObject camObj in security)
        {
            if (camObj != null)
            {
                Camera cam = camObj.GetComponentInChildren<Camera>(true);
                if (cam != null) cam.enabled = false;
            }
        }

        if (security[index] != null)
        {
            Camera selectedCam = security[index].GetComponentInChildren<Camera>(true);
            if (selectedCam != null)
            {
                Transform parent = selectedCam.transform;
                while (parent != null)
                {
                    parent.gameObject.SetActive(true);
                    parent = parent.parent;
                }

                selectedCam.enabled = true;
            }
            else
            {
                Debug.LogWarning("No Camera :" + security[index].name);
            }
        }

        if (camnumb != null)
        {
            camnumb.text = "CAM 0" + (index + 1);
        }
    }
}