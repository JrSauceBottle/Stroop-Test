using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Purpose: To hold the functionality of the Generating each Test
///          by Setting up the Colour word and Buttons
/// Created Date: 18th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 18th of December 2021: Core functionialty added  
/// </summary>

public class ColourWord : MonoBehaviour
{

    [System.Serializable]
    public struct Colours
    {
        public List<string> colourText;
        public List<Color> colours;
    }

    [HideInInspector] public int currentColourNum;
    private int currentColourWordNum;
    private int previousColourNum;

    [System.Serializable]
    private struct ButtonTexts
    {
        public Text[] buttonTexts;
        public Text correctButtonText;
    }

    [Tooltip("Add a colour, the text and colour need to be the same element number")]
    [SerializeField] public Colours colourList;
    [Tooltip("Add a button text")]
    [SerializeField] private ButtonTexts buttonTextList;

    [HideInInspector] public Text colourWord;
    private Text roundText;

    // Start is called before the first frame update
    void Start()
    {
        FourColourCheck();

        colourWord = gameObject.transform.Find("Colour Word").GetComponent<Text>();
        roundText = gameObject.transform.Find("Rounds_Text").GetComponent<Text>();
        ScoreManager.ResetScores();
        GenerateTestRound();
    }

    /// <summary>
    /// SetColourWord Sets the Colour of the colour word but also 
    /// Checks to see if the Colour was used previously
    /// </summary>
    private void SetColourWord()
    {
        int randColourWordNum = Random.RandomRange(0, colourList.colourText.ToArray().Length);
        int randColourNum = Random.RandomRange(0, colourList.colours.ToArray().Length);

        // Checks to see if the colour word is the same as the colour and 
        // Checks to see if the next colour is the same as the previous colour
        if (randColourNum != randColourWordNum && randColourNum != previousColourNum)
        {
            for (int i = 0; i < colourList.colourText.ToArray().Length; i++)
            {
                if (i == randColourWordNum)
                {
                    colourWord.text = colourList.colourText[i].ToString();
                    currentColourWordNum = i;
                }
            }
            for (int t = 0; t < colourList.colours.ToArray().Length; t++)
            {
                if (t == randColourNum)
                {
                    colourWord.color = colourList.colours[t];
                    currentColourNum = t;
                    previousColourNum = randColourNum;
                }
            }
        }
        else
        {
            SetColourWord();
        }
    }

    /// <summary>
    /// When there are more than 4 colours the button will have to be randomized 
    /// The Colour word text gets added to a button and the Colour word colour is 
    /// Also added and the other 2 buttons are randomized
    /// </summary>
    private void SetButtonsRandom()
    {
        List<int> usedColourWordNums = new List<int>();
        
        for (int i = 0; i < buttonTextList.buttonTexts.Length; i++)
        {
            buttonTextList.buttonTexts[i].text = null;
        }

        usedColourWordNums.Add(currentColourNum);
        usedColourWordNums.Add(currentColourWordNum);

        for (int i = 0; i < buttonTextList.buttonTexts.Length; i++)
        {
            int randColour = Random.RandomRange(0, colourList.colourText.ToArray().Length);

            // Checks to see if the number that was randomly generated has been used already
            if (!usedColourWordNums.Contains(randColour))
            {
                buttonTextList.buttonTexts[i].text = colourList.colourText[randColour].ToString();
                usedColourWordNums.Add(randColour);
            }
            else
            {
                i--;
            }   
        }
        //Get a random button 
        int randomButtonNumOne = Random.RandomRange(0, buttonTextList.buttonTexts.Length);
        // Set that button to be the correct word for the colour shown
        buttonTextList.buttonTexts[randomButtonNumOne].text = colourList.colourText[currentColourNum].ToString();

        // Get another random button
        int randomButtonNumTwo = Random.RandomRange(0, buttonTextList.buttonTexts.Length);
        // Checks if both button are not the same
        if (randomButtonNumOne != randomButtonNumTwo)
        {
            // Set this button to be the word shown
            buttonTextList.buttonTexts[randomButtonNumTwo].text = colourList.colourText[currentColourWordNum].ToString();
        }
        else
        {
            randomButtonNumTwo++;
            // Checks to see if the button is within the array length
            if (randomButtonNumTwo >= buttonTextList.buttonTexts.Length)
            {
                randomButtonNumTwo = randomButtonNumOne - 1;
            }
            buttonTextList.buttonTexts[randomButtonNumTwo].text = colourList.colourText[currentColourWordNum].ToString();
        }
    }

    /// <summary>
    /// SetButtons is called when there are only 4 colours
    /// </summary>
    private void SetButtons()
    {
        for (int i = 0; i < buttonTextList.buttonTexts.Length; i++)
        {
            buttonTextList.buttonTexts[i].text = colourList.colourText[i];
        }
    }

    /// <summary>
    /// FourColourCheck checks if the users has chosen 4 colours then delete from 
    /// the respective arrays
    /// </summary>
    private void FourColourCheck()
    {
        if (Settings.m_fourColours)
        {
            for (int i = 0; i < colourList.colours.Count; i++)
            {
                if (i > 3)
                {
                    colourList.colours.RemoveAt(i);
                    colourList.colourText.RemoveAt(i);
                    i--;
                }
            }
            SetButtons();
        }
    }

    /// <summary>
    /// GenerateTestRound Generates a round of the test
    /// </summary>
    public void GenerateTestRound()
    {
        ScoreManager.m_currentRound.roundNumber++;
        roundText.text = "Round: " + ScoreManager.m_currentRound.roundNumber.ToString() + " / " + ScoreManager.m_maxRounds;
        SetColourWord();
        if (!Settings.m_fourColours)
        {
            SetButtonsRandom();
        }
        
        ScoreManager.m_startAnswerTimer = true;
    }

}
