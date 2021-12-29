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
    public struct EightColours
    {
        public List<string> colourText;
        public List<Color> colours;
    }
    [System.Serializable]
    public struct FourColours
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

    [SerializeField] public EightColours eightColourList;
    [SerializeField] private FourColours fourColourList;
    [SerializeField] private ButtonTexts buttonTextList;

    public Text colourWord;
    private Text roundText;

    // Start is called before the first frame update
    void Start()
    {
        if (Settings.fourColours)
        {
            for (int i = 0; i < eightColourList.colours.Count; i++)
            {
                if (i > 3)
                {
                    eightColourList.colours.RemoveAt(i);
                    eightColourList.colourText.RemoveAt(i);
                    i--;
                }
            }

            SetButtons(); 
        } 
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
        int randColourWordNum = Random.RandomRange(0, eightColourList.colourText.ToArray().Length);
        int randColourNum = Random.RandomRange(0, eightColourList.colours.ToArray().Length);

        // Checks to see if the colour word is the same as the colour and 
        // Checks to see if the next colour is the same as the previous colour
        if (randColourNum != randColourWordNum && randColourNum != previousColourNum)
        {
            for (int i = 0; i < eightColourList.colourText.ToArray().Length; i++)
            {
                if (i == randColourWordNum)
                {
                    colourWord.text = eightColourList.colourText[i].ToString();
                    currentColourWordNum = i;
                }
            }
            for (int t = 0; t < eightColourList.colours.ToArray().Length; t++)
            {
                if (t == randColourNum)
                {
                    colourWord.color = eightColourList.colours[t];
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
            int randColour = Random.RandomRange(0, eightColourList.colourText.ToArray().Length);

            // Checks to see if the number that was randomly generated has been used already
            if (!usedColourWordNums.Contains(randColour))
            {
                buttonTextList.buttonTexts[i].text = eightColourList.colourText[randColour].ToString();
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
        buttonTextList.buttonTexts[randomButtonNumOne].text = eightColourList.colourText[currentColourNum].ToString();

        // Get another random button
        int randomButtonNumTwo = Random.RandomRange(0, buttonTextList.buttonTexts.Length);
        // Checks if both button are not the same
        if (randomButtonNumOne != randomButtonNumTwo)
        {
            // Set this button to be the word shown
            buttonTextList.buttonTexts[randomButtonNumTwo].text = eightColourList.colourText[currentColourWordNum].ToString();
        }
        else
        {
            randomButtonNumTwo++;
            // Checks to see if the button is within the array length
            if (randomButtonNumTwo >= buttonTextList.buttonTexts.Length)
            {
                randomButtonNumTwo = randomButtonNumOne - 1;
            }
            buttonTextList.buttonTexts[randomButtonNumTwo].text = eightColourList.colourText[currentColourWordNum].ToString();
        }
    }

    private void SetButtons()
    {
        for (int i = 0; i < buttonTextList.buttonTexts.Length; i++)
        {
            buttonTextList.buttonTexts[i].text = eightColourList.colourText[i];
        }

    }

    public void GenerateTestLevel()
    {
        ScoreManager.thisRound.roundNumber++;
        roundText.text = "Round: " + ScoreManager.thisRound.roundNumber.ToString();
        SetColourWord();
        if (!Settings.fourColours)
        {
            SetButtonsRandom();
        }
        
        ScoreManager.m_startAnswerTimer = true;
    }

    private void ColourWordTest()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetColourWord();
            SetButtonsRandom();
        }
    }
}
