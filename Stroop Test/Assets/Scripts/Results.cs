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
    //Variables Set in the Inspector
    public GameObject roundTimeText;
    [Tooltip("Set the offset for the Round time Text")]
    public Vector2 roundTimeTextOffset;
    [Tooltip("Set the offset for the Best score text")]
    public float bestScoreTextYOffset;
    //Text Gameobjects
    public static GameObject m_roundTimeText;
    public static Text bestScore;
    public static Text roundTimeResults;
    [HideInInspector] public Text correctScoreText;
    [HideInInspector] public Text timeTakenText;
    [HideInInspector] public Text averageTimeTakenText;

    //Offsets for each roundText 
    private static Vector2 m_roundTimeTextOffset;
    private static float currentYOffset;
    private static float m_bestScoreTextYOffset;

    // Start is called before the first frame update
    void Start()
    {
        correctScoreText = gameObject.transform.Find("Scores Text").Find("CorrectScoreText").GetComponent<Text>();
        timeTakenText = gameObject.transform.Find("Scores Text").Find("TimeTakenScoreText").GetComponent<Text>();
        averageTimeTakenText = gameObject.transform.Find("Scores Text").Find("RoundTimeScorText").GetComponent<Text>();
        roundTimeResults = gameObject.transform.Find("RoundTimeResults").GetComponent<Text>();
        bestScore = gameObject.transform.Find("BestScore_Text").GetComponent<Text>();
        m_roundTimeText = roundTimeText;
        m_bestScoreTextYOffset = bestScoreTextYOffset;
        m_roundTimeTextOffset = roundTimeTextOffset;
        currentYOffset = m_roundTimeTextOffset.y;
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
        AddRoundTimesToText();
    }

    private static void AddRoundTimesToText()
    {
        foreach (var item in ScoreManager.m_listOfRounds)
        {
            GameObject currentText;

            currentText = Instantiate(m_roundTimeText, roundTimeResults.transform);
            currentText.transform.position = new Vector2(currentText.transform.position.x + m_roundTimeTextOffset.x, currentText.transform.position.y - currentYOffset);
            currentYOffset += m_roundTimeTextOffset.y;

            currentText.GetComponent<Text>().text = "Round " + item.roundNumber + ": " + item.roundTime.ToString("F2");
            if (item.roundTime == ScoreManager.m_lowestTime)
            {
                currentText.GetComponent<Text>().color = Color.yellow;
                bestScore.transform.position = new Vector2(bestScore.transform.position.x, currentText.GetComponent<Text>().transform.position.y + m_bestScoreTextYOffset);

            }
        }
        
    }
}
