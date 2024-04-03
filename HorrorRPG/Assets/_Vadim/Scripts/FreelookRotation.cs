using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Xml.Linq;

public class FreelookRotation : MonoBehaviour
{
    public CinemachineFreeLook vCam;

    [SerializeField] private bool shouldUseMouse = true;
    [SerializeField] private float rotateAmount = 45;
    [SerializeField] private float transitionTime = .3f;

    private float initialPosition = 0;
    private float targetPosition = 0;
    private float t = 0f;
    private float curTransitionTime = 0;
    private float lastActiveMouseInput = 0;

    private int numOfAngles;
    private float[] fixedAngles;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        curTransitionTime = transitionTime;
        numOfAngles = (int) (360 / rotateAmount);
        fixedAngles = new float[numOfAngles];
        for (int i = 0; i < numOfAngles; i++)
        {
            if (rotateAmount * i > 180f)
            {
                fixedAngles[i] = -360 + rotateAmount * i;
            }
            else
            {
                fixedAngles[i] = rotateAmount * i;
            }
            //print(fixedAngles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(vCam.m_XAxis.Value);
        ManageInput();

        
    }
    private void FixedUpdate()
    {
        if (initialPosition == targetPosition)
            return;


        if (t > 1)
        {
            return;
        }
        t += Time.deltaTime / transitionTime;
        float newPosition = Mathf.SmoothStep(initialPosition, targetPosition, t);
        vCam.m_XAxis.Value = newPosition;
        if (targetPosition == vCam.m_XAxis.Value)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

    }
    private void ManageInput()
    {
        if ((vCam.m_XAxis.m_InputAxisValue < .05 && vCam.m_XAxis.m_InputAxisValue > -0.05) == false)
        {
            lastActiveMouseInput = vCam.m_XAxis.m_InputAxisValue;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateCameraRight();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotateCameraLeft();
            return;
        }
        if(shouldUseMouse == false)
        {
            vCam.m_XAxis.m_InputAxisName = "";
            return;
        } 
        else
        {
            vCam.m_XAxis.m_InputAxisName = "Mouse X";
        }
        if (lastActiveMouseInput < 0)
        {
            RotateCameraRight();
            return;
        }
        if(lastActiveMouseInput > 0)
        {
            RotateCameraLeft();
        }
       
    }
    private void RotateCameraRight()
    {
        initialPosition = vCam.m_XAxis.Value;
        //targetPosition = (initialPosition + rotateAmount);
        targetPosition = FindClosestFixedAngle(true);
        lastActiveMouseInput = 0;
        t = 0;
    }

    private void RotateCameraLeft()
    {
        initialPosition = vCam.m_XAxis.Value;
        //targetPosition = (initialPosition - rotateAmount);
        targetPosition = FindClosestFixedAngle(false);
        lastActiveMouseInput = 0;
        t = 0;
    }

    private float FindClosestFixedAngle(bool isPositive)
    {
        float remainder = initialPosition % rotateAmount;
       
            // Calculate the difference to the next angle divisible by 45
            float differenceToNext = rotateAmount -  Mathf.Abs(remainder);
        curTransitionTime = transitionTime * (differenceToNext / rotateAmount);

        if (isPositive)
        {
            if(initialPosition < -0.01f && !remainder.Equals(0))
            {
                return initialPosition + Mathf.Abs(remainder);
            }
            return initialPosition + differenceToNext;
        }
        else
        {
            if(initialPosition > 0.01f && !remainder.Equals(0))
            {
                return initialPosition - Mathf.Abs(remainder);
            }
            return initialPosition - differenceToNext;
        }
            // Return the angle plus the difference to the next divisible angle
        //return isPositive ? initialPosition + differenceToNext : initialPosition - differenceToNext;


    }
    private float FindClosestElement(float value)
    {
        float closestElement = fixedAngles[0];
        float minDifference = Mathf.Abs(value - fixedAngles[0]);

        foreach (float element in fixedAngles)
        {
            float difference = Mathf.Abs(value - element);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestElement = element;
            }
        }

        return closestElement;
    }
}


//if(isPositive)
//{
//    for (int i = 0; i < numOfAngles; i++)
//    {
//        if (initialPosition.Equals(fixedAngles[i]))
//        {
//            newAngle = fixedAngles[i + 1 > numOfAngles - 1 ? 0 : i + 1];
//            break;
//            //print(initialPosition + (rotateAmount * (i+2) - fixedAngles[i + 1]));
//            //return initialPosition + (rotateAmount * (i + 2) - fixedAngles[i + 1]);
//        }
//        if (curAngle < fixedAngles[i + 1] && curAngle > fixedAngles[i]
//            || curAngle > fixedAngles[i + 1] && curAngle < fixedAngles[i])
//        {
//            newAngle = fixedAngles[(i + 1)];
//            break;
//        }

//        if (curAngle >= fixedAngles[i])
//        {

//            newAngle = fixedAngles[i + 1 > numOfAngles - 1 ? 0 + (numOfAngles - i - 1) : i + 1];
//            break;
//        }
//        if (curAngle <= fixedAngles[i])
//        {
//            newAngle = fixedAngles[i];
//            break;
//        }

//    }
//} else
//{
//    for (int i = 0; i < numOfAngles; i++)
//    {
//        if (initialPosition.Equals(fixedAngles[i]))
//        {
//            newAngle = fixedAngles[i - 1 < 0 ? numOfAngles - i - 1 : i - 1];
//            break;
//        }
//        //if (curAngle < fixedAngles[i - 1] && curAngle > fixedAngles[i]
//        //    || curAngle > fixedAngles[i - 1] && curAngle < fixedAngles[i])
//        //{
//        //    newAngle = fixedAngles[i - 1 < 0 ? numOfAngles - i - 1 : i - 1];
//        //    break;
//        //}
//        //if (curAngle <= fixedAngles[i])
//        //{
//        //    newAngle = fixedAngles[i - 1 < 0 ? numOfAngles - i - 1 : i - 1];
//        //    break;
//        //}
//        //if (curAngle >= fixedAngles[i])
//        //{
//        //    newAngle = fixedAngles[i];
//        //    break;
//        //}

//    }
//}