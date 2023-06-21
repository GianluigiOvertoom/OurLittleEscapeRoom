using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Rotator : MonoBehaviour
{
    public Transform linkedDial;

    [SerializeField] private int snapRotationAmount = 25;
    [SerializeField] private float angleTolerance;
    [SerializeField] private Maze maze;

    private XRBaseInteractor interactor;
    private float startAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;

    public void GrabbedBy()
    {
        interactor = GetComponent<XRGrabInteractable>().selectingInteractor;
        interactor.GetComponent<XRDirectInteractor>().hideControllerOnSelect = true;

        shouldGetHandRotation = true;
        startAngle = 0f;
    }

    public void GrabEnd()
    {
        shouldGetHandRotation = false;
        requiresStartAngle = true;
    }

    void Update()
    {
        if (shouldGetHandRotation)
        {
            float rotationAngle = GetInteractorRotation(); //gets the current controller angle
            GetRotationDistance(rotationAngle);
        }
    }

    public float GetInteractorRotation()
    {
        var handRotation = interactor.GetComponent<Transform>().eulerAngles;
        return handRotation.z;
    }

    private void GetRotationDistance(float currentAngle)
    {
        if (!requiresStartAngle)
        {
            var angleDifference = Mathf.Abs(startAngle - currentAngle);

            if (angleDifference > angleTolerance)
            {
                if (angleDifference > 270f) //checking to see if the user has gone from 0-360 - a very tiny movement but will trigger the angletolerance
                {
                    float angleCheck;

                    if (startAngle < currentAngle) //going anticlockwise
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);

                        if (angleCheck < angleTolerance)
                        {
                            return;
                        }
                        else
                        {
                            RotateDialAntiClockwise();
                            startAngle = currentAngle;
                        }
                    }
                    else if (startAngle > currentAngle) //going clockwise;
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);

                        if (angleCheck < angleTolerance)
                        {
                            return;
                        }
                        else
                        {
                            RotateDialClockwise();
                            startAngle = currentAngle;
                        }
                    }
                }
                else
                {
                    if (startAngle < currentAngle)//clockwise
                    {
                        RotateDialClockwise();
                        startAngle = currentAngle;
                    }
                    else if (startAngle > currentAngle)
                    {
                        RotateDialAntiClockwise();
                        startAngle = currentAngle;
                    }
                }
            }
        }
        else
        {
            requiresStartAngle = false;
            startAngle = currentAngle;
        }
    }

    private float CheckAngle(float currentAngle, float startAngle)
    {
        var checkAngleTravelled = (360f - currentAngle) + startAngle;
        return (checkAngleTravelled);
    }

    private void RotateDialClockwise()
    {
        linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x, linkedDial.localEulerAngles.y - snapRotationAmount, linkedDial.localEulerAngles.z);
        GetComponent<IDial>().DialChanged(linkedDial.localEulerAngles.y);
        maze.PlayRotateSound();
    }

    private void RotateDialAntiClockwise()
    {
        linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x, linkedDial.localEulerAngles.y + snapRotationAmount, linkedDial.localEulerAngles.z);
        GetComponent<IDial>().DialChanged(linkedDial.localEulerAngles.y);
        maze.PlayRotateSound();
    }
}
