using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Purpose: To Keep a track of all the scores when changing scenes
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
/// </summary>

public class ScoreManager : MonoBehaviour
{
    //Variables to check to make sure variables are updating accordingly 
    [SerializeField] private int maxRounds;
    [SerializeField] private int currentRound;
    [SerializeField] private int currentScore;
    [SerializeField] private float currentTimeTaken;
    [SerializeField] private List<float> currentAnswerTimeList = new List<float>();
    //Rounds
    public static int m_maxRounds = 10;
    public static int m_currentRound;
    //How many the player got right
    public static int m_score;
    //From when the test started
    public static float m_timeTaken;
    public static bool m_startTimer;
    //Each round of the game 
    public static List<float> m_answerTimeList = new List<float>();
    public static float m_currentAnswerTime;
    public static float m_answerTimeAverage;
    public static bool m_startAnswerTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_maxRounds = maxRounds;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = m_score;
        currentRound = m_currentRound;
        UpdateTimers();
        currentTimeTaken = m_timeTaken;
        currentAnswerTimeList = m_answerTimeList;
    }

    public static void UpdateTimers()
    {
        if (m_startTimer)
        {
            m_timeTaken += Time.deltaTime;
        }
        if (m_startAnswerTimer)
        {
            m_currentAnswerTime += Time.deltaTime;
        }
    }

    public static void ResetScores()
    {
        m_score = 0;
        m_currentRound = 0;
        m_timeTaken = 0;
        m_startTimer = true;
        m_answerTimeList.Clear();
    }

    public static void EndTest()
    {
        if (m_currentRound >= m_maxRounds )
        {
            m_startTimer = false;
            
            LoadScene.LoadResultScene();
        }
    }

    public static float ScoreTracker()
    {
        return m_score++;
    }

    public static float TimeTakenToComplete()
    {
        return m_timeTaken; 
    }

    public static void AddAnswerTime()
    {
        m_answerTimeList.Add(m_currentAnswerTime);
        m_currentAnswerTime = 0;
    }

    public static float AverageTimeToAnswer()
    {
        foreach (var item in m_answerTimeList)
        {
            m_answerTimeAverage += item;
        }

        return m_answerTimeAverage = m_answerTimeAverage / m_answerTimeList.Count;
    }
}
