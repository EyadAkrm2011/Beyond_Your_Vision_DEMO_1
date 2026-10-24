using System;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Doortry2 : MonoBehaviour
{
    [SerializeField] private Vector3 OpenPos;
    [SerializeField] private Vector3 ClosePos;
    public bool IsOpen;

    [SerializeField] private float DoorSpeed;
    void Start()
    {
        transform.position = OpenPos;
        IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpen)
        {
            if (transform.position != OpenPos)
            {
                if (Vector3.Distance(transform.position, OpenPos) <= 0.5f)
                {
                    transform.position = OpenPos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, OpenPos, DoorSpeed * Time.deltaTime);
                }

            }

        }
        else
        {
            if (transform.position != ClosePos)
            {
                if (Vector3.Distance(transform.position, ClosePos) <= 0.5f)
                {
                    transform.position = ClosePos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, ClosePos, DoorSpeed * Time.deltaTime);
                }
            }
        }
    }
}
