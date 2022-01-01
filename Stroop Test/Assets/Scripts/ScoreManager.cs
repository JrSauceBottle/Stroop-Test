using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Purpose: To Keep a track of all the scores when changing scenes
/// Created Date: 19th of December 2021
/// Author/s: Callum McDermott
/// Major Revision History: 
///     - 19th of December 2021: Core functionialty added
///     - 29th of December 2021: Changed the way I was storing round infomation and made it into a stuct
/// </summary>

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private int maxRounds;
    //Rounds
    public struct Round
    {
        public int roundNumber;
        public float roundTime;
    }
    public static Round m_currentRound;
    public static List<Round> m_listOfRounds = new List<Round>();

    public static int m_maxRounds;
    //How many the player got right
    public static int m_score;
    //From when the test started
    public static float m_timeTaken;
    public static bool m_startTimer;
    //Each round of the game 
    public static float m_lowestTime;
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
        UpdateTimers();
    }

    public static void UpdateTimers()
    {
        if (m_startTimer)
        {
            m_timeTaken += Time.deltaTime;
        }
        if (m_startAnswerTimer)
        {
            m_currentRound.roundTime += Time.deltaTime;
        }
    }

    public static void ResetScores()
    {
        m_score = 0;
        m_currentRound.roundNumber = 0;
        m_answerTimeAverage = 0;
        m_listOfRounds.Clear();
        m_timeTaken = 0;
        m_startTimer = true;
    }

    public static void CheckEndTest()
    {
        if (m_currentRound.roundNumber >= m_maxRounds )
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
        m_listOfRounds.Add(m_currentRound);
        m_currentRound.roundTime = 0;
    }

    public static float AverageTimeToAnswer()
    {
        m_lowestTime = m_listOfRounds.ToArray()[0].roundTime;
        foreach(var round in m_listOfRounds)
        {
            m_answerTimeAverage += round.roundTime;
            if (m_lowestTime > round.roundTime)
            {
                m_lowestTime = round.roundTime;
            }
        }

        return m_answerTimeAverage = m_answerTimeAverage / m_listOfRounds.Count;
    }
}
