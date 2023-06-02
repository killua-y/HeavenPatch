using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Converting the mouse position to a point in 3D-space
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (point.z - cam.transform.position.z) + cam.transform.position.z);
        
        transform.LookAt(finalPoint, Vector3.up);
    }
}
