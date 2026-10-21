using UnityEditor.XR;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRework : MonoBehaviour
{
    private bool FrontBool;
    private bool BehindBool;
    private bool RightBool;
    private bool LeftBool;
    public float movementSpeed;
    public void FrontMove()
    {
        transform.position = transform.position -  new Vector3(0 , 0 , movementSpeed) * Time.deltaTime;
    }
    public void BackMove()
    {
        transform.position = transform.position + new Vector3(0, 0, movementSpeed) * Time.deltaTime;
    }
    public void RightMove()
    {
        transform.position = transform.position - new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
    }
    public void LeftMove()
    {
        transform.position = transform.position + new Vector3(movementSpeed , 0 , 0) * Time.deltaTime;
    }
    public void Forward(InputAction.CallbackContext cntx)
    {
        if (cntx.started || cntx.performed)
        {
            FrontBool = true;
        }
        else
        {
            FrontBool = false;
        }

    }

    public void Backward(InputAction.CallbackContext cntx)
    {
        if (cntx.started || cntx.performed)
        {
            BehindBool = true;
        }
        else
        {
            BehindBool = false;
        }

    }

    public void Right(InputAction.CallbackContext cntx)
    {
        if (cntx.started || cntx.performed)
        {
            RightBool = true;
        }
        else
        {
            RightBool = false;
        }

    }

    public void Left(InputAction.CallbackContext cntx)
    {
        if (cntx.started || cntx.performed)
        {
            LeftBool = true;
        }
        else
        {
            LeftBool = false;
        }

    }
    private void Update()
    {
        if (FrontBool == true)
        {
            FrontMove();
        }
        if (BehindBool == true)
        {
            BackMove();
        }
        if (RightBool == true)
        {
            RightMove();
        }
        if (LeftBool == true) {

            LeftMove();
        }
    }
}



