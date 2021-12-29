using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Purpose: To hold the functionality for each buttons answer to be checked and to move
///          onto the next level wether the answer was correct or wrong
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
/// </summary>
public class Answer : MonoBehaviour
{
    private ColourWord colourWord;
    private Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        colourWord = transform.parent.GetComponent<ColourWord>();
        buttonText = transform.GetChild(0).GetComponent<Text>();
    }

    public void CheckAnswer()
    {
        
        if (buttonText.text == colourWord.eightColourList.colourText[colourWord.currentColourNum])
        {
            Debug.Log("Correct");
            ScoreManager.ScoreTracker();
        }
        else
        {
            Debug.Log("Incorrect");
        }
        ScoreManager.EndTest();
        ScoreManager.m_startAnswerTimer = false;
        ScoreManager.AddAnswerTime();
        colourWord.GenerateTestLevel();
    }
}
