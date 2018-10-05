using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    //private variables
    private bool IsDragging;
    private Vector3 TouchPosition;
    private Vector3 PreviousPosition;
    private Vector3 Rotation;
    private Vector3 FirstVector;

    //public variables
    public float MinimumDistance = 1.0f;
    public Transform Car;
    public float Scale = 3;

    void Update()
    {
        //Get the initial touch position and set dragging to true
        if (Input.GetMouseButtonDown(0))
        {
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PreviousPosition = transform.position;
            IsDragging = true;
        }

        //Set dragging to false when touch is lifted
        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }

        //Logic: Signedangle is always calculated between two points in the camera space, the pivot point being (x,y) = (0,0)
        //Touch position is always re-calculated after every update
        if (IsDragging)
        {
            //calculate touch distance from the center - the wheel won't move if the touch is too close to the center
            float distance = Vector2.Distance(transform.position, TouchPosition);

            if (distance > MinimumDistance)
            {
                float angle = Vector2.SignedAngle(TouchPosition - transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position - (transform.position - PreviousPosition));
                Rotation.z = angle;
                transform.Rotate(Rotation);

                //Calculate target position
                Vector3 moveAmount = new Vector3(Rotation.z * Scale/180, 0, 0);
                Vector3 targetPos = Car.position - moveAmount;

                Car.position = Vector3.MoveTowards(Car.position, targetPos, 1);
                Debug.Log(TouchPosition);
            }
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PreviousPosition = transform.position;
        }
        

    }
}
