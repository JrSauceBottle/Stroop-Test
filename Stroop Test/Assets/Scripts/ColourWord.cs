using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Purpose: To hold the functionality of the COME BACK TO
/// Created Date: 18th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 18th of December 2021: Core functionialty added
/// </summary>

public class ColourWord : MonoBehaviour
{

    [System.Serializable]
    private struct Colours
    {
        public string[] colourText;
        public Color[] colours;
        public int currentColourNum;
        public int currentColourWordNum;
    }
    [System.Serializable]
    private struct ButtonTexts
    {
        public Text[] buttonTexts;
        public Text correctButtonText;
    }

    [SerializeField] private Colours colourList;
    [SerializeField] private ButtonTexts buttonList;


    private List<int> usedColourWordNums = new List<int>();

    private int randColourWordNum;
    private int randColourNum;
    private int previousColourNum;

    private Text colourWord;


    // Start is called before the first frame update
    void Start()
    {
        colourWord = gameObject.transform.Find("Colour Word").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //SetColourWord();
        ColourWordTest();
    }

    private void SetColourWord()
    {

        randColourWordNum = Random.RandomRange(0, colourList.colourText.Length);
        randColourNum = Random.RandomRange(0, colourList.colours.Length);

        // Checks to see if the colour word is the same as the colour and 
        // Checks to see if the next colour is the same as the previous colour
        if (randColourNum != randColourWordNum && randColourNum != previousColourNum)
        {
            for (int i = 0; i < colourList.colourText.Length; i++)
            {
                if (i == randColourWordNum)
                {
                    colourWord.text = colourList.colourText[i].ToString();
                    colourList.currentColourWordNum = i;
                }
            }
            for (int t = 0; t < colourList.colours.Length; t++)
            {
                if (t == randColourNum)
                {
                    colourWord.color = colourList.colours[t];
                    colourList.currentColourNum = t;
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
        
        usedColourWordNums.Clear();
        for (int i = 0; i < buttonList.buttonTexts.Length; i++)
        {
            buttonList.buttonTexts[i].text = null;
        }

        usedColourWordNums.Add(colourList.currentColourNum);
        usedColourWordNums.Add(colourList.currentColourWordNum);

        for (int i = 0; i < buttonList.buttonTexts.Length; i++)
        {
            randColourWordNum = Random.RandomRange(0, colourList.colourText.Length);

            // Checks to see if the number that was randomly generated has been used already
            if (!usedColourWordNums.Contains(randColourWordNum))
            {
                buttonList.buttonTexts[i].text = colourList.colourText[randColourWordNum].ToString();
                usedColourWordNums.Add(randColourWordNum);
                previousColourNum = randColourNum;
            }
            else
            {
                i--;
            }   
        }
        //Get a random button 
        int randomButtonNumOne = Random.RandomRange(0, buttonList.buttonTexts.Length);
        // Set that button to be the correct word for the colour shown
        buttonList.buttonTexts[randomButtonNumOne].text = colourList.colourText[colourList.currentColourNum].ToString();

        // Get another random button
        int randomButtonNumTwo = Random.RandomRange(0, buttonList.buttonTexts.Length);
        // Checks if both button are not the same
        if (randomButtonNumOne != randomButtonNumTwo)
        {
            // Set this button to be the word shown
            buttonList.buttonTexts[randomButtonNumTwo].text = colourList.colourText[colourList.currentColourWordNum].ToString();
        }
        else
        {
            randomButtonNumTwo++;
            // Checks to see if the button is within the array length
            if (randomButtonNumTwo >= buttonList.buttonTexts.Length)
            {
                randomButtonNumTwo = randomButtonNumOne - 1;
            }
           
            buttonList.buttonTexts[randomButtonNumTwo].text = colourList.colourText[colourList.currentColourWordNum].ToString();
        }
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
