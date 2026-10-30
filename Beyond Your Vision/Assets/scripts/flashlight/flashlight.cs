using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class flashlight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light flash;
    public TextMeshProUGUI text;
    private bool flash1;
    public GameObject model;
    void Start()
    {
        flash1 = false;

        if (flash == null)
        {
            enabled = false;
            return;
        }

        SetFlashlight(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current!=null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            SetFlashlight(!flash1);
        }
    }

    void SetFlashlight(bool on)
    {
        flash1 = on;
        flash.enabled = flash1;
        flash.enabled = flash1;
        if (model != null) model.SetActive(flash1);

        if (text != null)
        {
            if (flash1)
            {
                text.text = "Press F to disable flashlight";
            }
            else
            {
                text.text = "Press F to enable flashlight";
            }
        }
    }
}

