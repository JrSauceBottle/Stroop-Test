using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Purpose: To hold the functionality of changing the score text when the result scene is loaded
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
/// </summary>
public class Results : MonoBehaviour
{
    public Text correctScoreText;
    public Text timeTakenText;
    public Text averageTimeTakenText;

    // Start is called before the first frame update
    void Start()
    {
        correctScoreText = gameObject.transform.Find("Scores Text").Find("CorrectScoreText").GetComponent<Text>();
        timeTakenText = gameObject.transform.Find("Scores Text").Find("TimeTakenScoreText").GetComponent<Text>();
        averageTimeTakenText = gameObject.transform.Find("Scores Text").Find("RoundTimeScorText").GetComponent<Text>();
        FinalResults();
    }

    /// <summary>
    /// Updates all the text to show the scores
    /// </summary>
    private void FinalResults()
    {
        correctScoreText.text = ScoreManager.ScoreTracker() + "/" + ScoreManager.m_maxRounds;
        timeTakenText.text = ScoreManager.TimeTakenToComplete().ToString("F2");
        averageTimeTakenText.text = ScoreManager.AverageTimeToAnswer().ToString("F2");
    }
}
