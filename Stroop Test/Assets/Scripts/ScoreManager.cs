using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Purpose: To Keep a track of all the scores when changing scenes
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
///     -29th of December 2021: Changed the way I was storing round infomation and made it into a stuct
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
    public struct Round
    {
        public int roundNumber;
        public float roundTime;
    }
    public static Round thisRound;
    public static List<Round> m_listOfRounds = new List<Round>();

    public static int m_maxRounds = 10;
    //How many the player got right
    public static int m_score;
    //From when the test started
    public static float m_timeTaken;
    public static bool m_startTimer;
    //Each round of the game 
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
        currentRound = thisRound.roundNumber;
        UpdateTimers();
        currentTimeTaken = m_timeTaken;
    }

    public static void UpdateTimers()
    {
        if (m_startTimer)
        {
            m_timeTaken += Time.deltaTime;
        }
        if (m_startAnswerTimer)
        {
            thisRound.roundTime += Time.deltaTime;
        }
    }

    public static void ResetScores()
    {
        m_score = 0;
        thisRound.roundNumber = 0;
        m_answerTimeAverage = 0;
        m_listOfRounds.Clear();
        m_timeTaken = 0;
        m_startTimer = true;
    }

    public static void EndTest()
    {
        if (thisRound.roundNumber >= m_maxRounds )
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
        m_listOfRounds.Add(thisRound);
        thisRound.roundTime = 0;
    }

    public static float AverageTimeToAnswer()
    {
        foreach(var item in m_listOfRounds)
        {
            m_answerTimeAverage += item.roundTime;
        }

        return m_answerTimeAverage = m_answerTimeAverage / m_listOfRounds.Count;
    }
}
