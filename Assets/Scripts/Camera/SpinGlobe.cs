using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGlobe : MonoBehaviour
{
    public float zoomSpeed;
    public float rotationSpeed = 1000;
    bool dragging = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(transform.localScale.x < 13)
            transform.localScale = new Vector3(transform.localScale.x + zoomSpeed, transform.localScale.y + zoomSpeed, transform.localScale.z + zoomSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(transform.localScale.x > 4)
            transform.localScale = new Vector3(transform.localScale.x - zoomSpeed, transform.localScale.y - zoomSpeed, transform.localScale.z - zoomSpeed);
        }

    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * transform.localScale.x - 4 * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * transform.localScale.y - 4 * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }

    
}
