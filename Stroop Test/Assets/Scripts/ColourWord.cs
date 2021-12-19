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
        public string[] colourText;
        public Color[] colours;
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

    [SerializeField] public Colours colourList;
    [SerializeField] private ButtonTexts buttonTextList;

    public Text colourWord;
    private Text roundText;

    // Start is called before the first frame update
    void Start()
    {
        colourWord = gameObject.transform.Find("Colour Word").GetComponent<Text>();
        roundText = gameObject.transform.Find("Rounds").GetComponent<Text>();
        ScoreManager.ResetScores();
        GenerateTestLevel();
    }

    // Update is called once per frame
    void Update()
    {
        // ColourWordTest();
        
    }

    private void SetColourWord()
    {
        int randColourWordNum = Random.RandomRange(0, colourList.colourText.Length);
        int randColourNum = Random.RandomRange(0, colourList.colours.Length);

        // Checks to see if the colour word is the same as the colour and 
        // Checks to see if the next colour is the same as the previous colour
        if (randColourNum != randColourWordNum && randColourNum != previousColourNum)
        {
            for (int i = 0; i < colourList.colourText.Length; i++)
            {
                if (i == randColourWordNum)
                {
                    colourWord.text = colourList.colourText[i].ToString();
                    currentColourWordNum = i;
                }
            }
            for (int t = 0; t < colourList.colours.Length; t++)
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

    private void SetButtons()
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
            int randColour = Random.RandomRange(0, colourList.colourText.Length);

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

    public void GenerateTestLevel()
    {
        ScoreManager.m_currentRound++;
        roundText.text = "Round: " + ScoreManager.m_currentRound.ToString();
        SetColourWord();
        SetButtons();
        ScoreManager.m_startAnswerTimer = true;
    }

    private void ColourWordTest()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetColourWord();
            SetButtons();
        }
    }
}
