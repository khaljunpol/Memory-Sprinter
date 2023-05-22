using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool invertControls  = false;
    [SerializeField] private float rotationExponent = 1f;
    [SerializeField] private float inertiaFactor = 0.95f;
    [SerializeField] private float slowdownFactor = 0.98f;

    private Vector2 dragStartPosition;
    private bool isDragging = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = rotationSpeed * 10f;
    }

    private void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     HandleTouchInput();
        // }
        // else if (Input.GetMouseButtonDown(0))
        // {
        //     HandleMouseInput();
        // }
        // else if (!isDragging)
        // {
        //     rb.angularVelocity *= slowdownFactor;
        // }

        
        HandleMouseInput();

        if (!isDragging)
        {
            rb.angularVelocity *= slowdownFactor;
        }
    }

    private void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                dragStartPosition = touch.position;
                isDragging = true;
                rb.angularVelocity = Vector3.zero;
                break;
            case TouchPhase.Moved:
                Vector2 dragCurrentPosition = touch.position;
                HandleDrag(dragCurrentPosition);
                break;
            case TouchPhase.Ended:
                isDragging = false;
                rb.angularVelocity *= inertiaFactor;
                break;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
        
        dragStartPosition = Input.mousePosition;
        isDragging = true;
        rb.angularVelocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 dragCurrentPosition = Input.mousePosition;
            HandleDrag(dragCurrentPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.angularVelocity *= inertiaFactor;
        }
    }

    private void HandleDrag(Vector2 dragCurrentPosition)
    {
        Vector2 dragDelta = dragCurrentPosition - dragStartPosition;

        float rotationAmount = Mathf.Pow(Mathf.Abs(dragDelta.magnitude), rotationExponent);
        float adjustedRotationSpeed = rotationSpeed * rotationAmount;

        Vector3 dragDirection = new Vector3(dragDelta.y, -dragDelta.x, 0f).normalized;
        float rotationVelocity = (invertControls ? -1f : 1f) * adjustedRotationSpeed * Time.deltaTime;

        rb.AddTorque(dragDirection * rotationVelocity, ForceMode.VelocityChange);
    }
}
