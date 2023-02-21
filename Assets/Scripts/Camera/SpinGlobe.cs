using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGlobe : MonoBehaviour
{
    // !FIXME needs serious refactor
    public float zoomSpeed;
    public float rotationSpeed = 1000;
    bool dragging = false;
    Vector3 normalSize = new Vector3(11f, 11f, 11f);
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SettingsManager.OnSettingsChanged += SelectFlatEarth;
    }

    private void OnDisable()
    {
        SettingsManager.OnSettingsChanged -= SelectFlatEarth;
    }

    private void SelectFlatEarth()
    {
        // If the earth should be flat earth then make flat
        if (SettingsManager.Instance.FlatEarthModel)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        else
            transform.localScale = normalSize;

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
            if (transform.localScale.x < 13)
            {
                if (SettingsManager.Instance.FlatEarthModel)
                    transform.localScale = new Vector3(transform.localScale.x + zoomSpeed, transform.localScale.y + zoomSpeed, 1f);
                else
                    transform.localScale = new Vector3(transform.localScale.x + zoomSpeed, transform.localScale.y + zoomSpeed, transform.localScale.z + zoomSpeed);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.localScale.x > 4)
            {
                if (SettingsManager.Instance.FlatEarthModel)
                    transform.localScale = new Vector3(transform.localScale.x - zoomSpeed, transform.localScale.y - zoomSpeed, 1f);
                else
                    transform.localScale = new Vector3(transform.localScale.x - zoomSpeed, transform.localScale.y - zoomSpeed, transform.localScale.z - zoomSpeed);

            }
        }

        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * transform.localScale.x - 4 * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * transform.localScale.y - 4 * Time.deltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }

    
}
