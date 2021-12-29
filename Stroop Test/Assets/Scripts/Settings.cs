using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Purpose: To hold the functionality of Customizing the test
/// Created Date: 29th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 29th of December 2021: Core functionialty added
/// </summary>
public class Settings : MonoBehaviour
{
    public static bool fourColours = true;
    private static GameObject fourColourCheck;
    private static GameObject eightColourCheck;

    private void Start()
    {
        fourColourCheck = transform.Find("FourColours").Find("Button").Find("Image").gameObject;
        eightColourCheck = transform.Find("EightColours").Find("Button").Find("Image").gameObject;

    }

    public static void FourColours()
    {
        eightColourCheck.SetActive(false);
        fourColours = true;
        fourColourCheck.SetActive(true);
    }
    public static void EightColours()
    {
        fourColourCheck.SetActive(false);
        fourColours = false;
        eightColourCheck.SetActive(true);
    }

   
}
