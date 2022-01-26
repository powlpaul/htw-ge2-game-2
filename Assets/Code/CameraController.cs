using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * inspired by: https://www.youtube.com/watch?v=rnqF6S7PfFA
 * 
 * implementing movement for a camera is not that hard, but doing it smoothly is pretty difficult.
 * this is the reason I used this tutorial as a reference. 
 */
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;

    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private Vector3 newPosition;
    private Vector3 newZoom;
    
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        newZoom += zoomAmount * Input.mouseScrollDelta.y;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
