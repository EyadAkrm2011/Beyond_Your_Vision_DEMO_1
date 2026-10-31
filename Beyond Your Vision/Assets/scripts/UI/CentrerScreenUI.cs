
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CenterScreenUI : MonoBehaviour
{
    private EventSystem eventSystem;
    private PointerEventData pointer;
    private List<RaycastResult> results = new List<RaycastResult>();

    private GameObject currentHover;

    void Start()
    {
        eventSystem = EventSystem.current;

        if (eventSystem == null)
        {
            Debug.LogError("No EventSystem found!");
            enabled = false;
            return;
        }

        pointer = new PointerEventData(eventSystem);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (eventSystem == null || Mouse.current == null)
            return;

        // Use the center of the screen as the pointer
        pointer.position = new Vector2(
            Screen.width / 2f,
            Screen.height / 2f
        );

        // Find UI elements under the crosshair
        results.Clear();
        eventSystem.RaycastAll(pointer, results);

        GameObject hit = results.Count > 0
            ? results[0].gameObject
            : null;

        pointer.pointerCurrentRaycast =
            results.Count > 0 ? results[0] : new RaycastResult();

        // Handle hover enter and exit
        if (hit != currentHover)
        {
            if (currentHover != null)
            {
                ExecuteEvents.Execute(
                    currentHover,
                    pointer,
                    ExecuteEvents.pointerExitHandler
                );
            }

            if (hit != null)
            {
                ExecuteEvents.Execute(
                    hit,
                    pointer,
                    ExecuteEvents.pointerEnterHandler
                );
            }

            currentHover = hit;
            pointer.pointerEnter = hit;
        }

        // Handle mouse press
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            pointer.pointerPress = null;
            pointer.rawPointerPress = hit;
            pointer.eligibleForClick = true;

            if (hit != null)
            {
                pointer.pointerPress =
                    ExecuteEvents.ExecuteHierarchy(
                        hit,
                        pointer,
                        ExecuteEvents.pointerDownHandler
                    );

                if (pointer.pointerPress == null)
                {
                    pointer.pointerPress =
                        ExecuteEvents.GetEventHandler<IPointerClickHandler>(hit);
                }
            }
        }

        // Handle mouse release
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (pointer.pointerPress != null)
            {
                ExecuteEvents.Execute(
                    pointer.pointerPress,
                    pointer,
                    ExecuteEvents.pointerUpHandler
                );

                GameObject clickHandler =
                    ExecuteEvents.GetEventHandler<IPointerClickHandler>(hit);

                if (clickHandler == pointer.pointerPress)
                {
                    ExecuteEvents.Execute(
                        pointer.pointerPress,
                        pointer,
                        ExecuteEvents.pointerClickHandler
                    );
                }
            }

            pointer.pointerPress = null;
            pointer.rawPointerPress = null;
            pointer.eligibleForClick = false;
        }
    }
}
