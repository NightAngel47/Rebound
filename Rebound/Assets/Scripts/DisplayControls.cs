/* Primary Author: Trent Lewis
 * Co-Author: Alex Cline - Setup Variables
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControls : MonoBehaviour
{
    [Header("Keyboard Input")]
    [SerializeField] private GameObject kbControls0 = null;
    [SerializeField] private GameObject kbControls3 = null;

    [Header("Controller Input")]
    [SerializeField] private GameObject cntrlrControls0 = null;
    [SerializeField] private GameObject cntrlrControls3 = null;


    private void Start()
    {
        if (GameManager.instance.nextLevel.Equals(0))
        {
            kbControls0.SetActive(true);
        }
        if (GameManager.instance.nextLevel.Equals(3))
        {
            kbControls3.SetActive(true);
        }
    }

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
                    if (GameManager.instance.nextLevel.Equals(0))
                    {
                        cntrlrControls0.SetActive(true);
                        kbControls0.SetActive(false);
                    }
                    if (GameManager.instance.nextLevel.Equals(3))
                    {
                        cntrlrControls3.SetActive(true);
                        kbControls3.SetActive(false);
                    }
        
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + j + " is connected using: " + temp[j]);
                }
                else
                {
                    if (GameManager.instance.nextLevel.Equals(0))
                    {
                        cntrlrControls0.SetActive(false);
                        kbControls0.SetActive(true);
                    }
                    if (GameManager.instance.nextLevel.Equals(3))
                    {
                        cntrlrControls3.SetActive(false);
                        kbControls3.SetActive(true);
                    }

                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + j + " is disconnected.");

                }
            }
        }
    }
}    