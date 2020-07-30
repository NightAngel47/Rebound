/* Primary Author: Trent Lewis
 * Co-Author: Alex Cline - Setup variables
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapControls : MonoBehaviour
{

    [Header("Keyboard Input")]
    [SerializeField] private GameObject[] kbLftRght = null;
    [SerializeField] private GameObject kbJump = null;
    [SerializeField] private GameObject kbStomp = null;

    [Header("Controller Input")]
    [SerializeField] private GameObject[] cntrlLftRght = null;
    [SerializeField] private GameObject cntrlJump = null;
    [SerializeField] private GameObject cntrlStomp = null;


    void Update()
    {
        //https://answers.unity.com/questions/1100642/joystick-runtime-plugunplug-detection.html
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int j = 0; j < temp.Length; ++j)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[j]))
                {
                    for (int i = 0; i < cntrlLftRght.Length; i++)
                    {
                        cntrlLftRght[i].SetActive(true);
                    }
                    cntrlJump.SetActive(true);
                    cntrlStomp.SetActive(true);

                    for (int i = 0; i < kbLftRght.Length; i++)
                    {
                        kbLftRght[i].SetActive(false);
                    }
                    kbJump.SetActive(false);
                    kbStomp.SetActive(false);
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + j + " is connected using: " + temp[j]);
                }
                else
                {
                    for (int i = 0; i < cntrlLftRght.Length; i++)
                    {
                        cntrlLftRght[i].SetActive(false);
                    }
                    cntrlJump.SetActive(false);
                    cntrlStomp.SetActive(false);

                    for (int i = 0; i < kbLftRght.Length; i++)
                    {
                        kbLftRght[i].SetActive(true);
                    }
                    kbJump.SetActive(true);
                    kbStomp.SetActive(true);
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + j + " is disconnected.");

                }
            }
        }
    } 
}