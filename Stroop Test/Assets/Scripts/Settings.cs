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

    public static bool m_fourColours = true;
    private static GameObject fourColourTick;
    private static GameObject eightColourTick;

    private void Start()
    {
        fourColourTick = transform.Find("FourColours").Find("Button").Find("Image").gameObject;
        eightColourTick = transform.Find("EightColours").Find("Button").Find("Image").gameObject;

    }

    public static void FourColours()
    {
        eightColourTick.SetActive(false);
        m_fourColours = true;
        fourColourTick.SetActive(true);
    }
    public static void EightColours()
    {
        fourColourTick.SetActive(false);
        m_fourColours = false;
        eightColourTick.SetActive(true);
    }

   
}
