using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Purpose: To hold the functionality of changing Scenes
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
/// </summary>
public class LoadScene : MonoBehaviour
{
    public static void LoadTestScene()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadResultScene()
    {
        SceneManager.LoadScene(2);
    }
}
