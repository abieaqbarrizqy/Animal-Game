using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text ScoreText;
    private ArusGameData m_GameData;
    private int m_AnimalNumber;

    private int m_Scores;
    private int m_WrongScores;
    private bool Initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        m_GameData = GameObject.Find("GameDataObject").GetComponent<ArusGameData>() as ArusGameData;
        m_Scores = 0;
        m_WrongScores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Initialized)
        {
            if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
                m_AnimalNumber = 15;
            else if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
                m_AnimalNumber = 100;
            else
                m_AnimalNumber = m_GameData.GetAnimalNumber();
            DisplayScores();
            Initialized = true;
        }

    }

    public int GetArusScore() { return m_Scores;}
    public int GetArusWrongScore() { return m_WrongScores;}
    public int GetQuestionsNumber() {return m_AnimalNumber;}

    public void AddScores()
    {
        if(m_Scores < m_AnimalNumber)
            m_Scores += 1;
        DisplayScores();
    }

    public void RemoveScores()
    {
        if(m_Scores > 0)
            m_Scores -= 1;
        DisplayScores();
    }

    public void AddWrongScore()
    {
        if(m_WrongScores < m_AnimalNumber)
            m_WrongScores += 1;
    }

    //void DisplayScores()
    //{
    //    string DisplayString = "Scores: " + m_Scores + "/" + m_AnimalNumber;
    //    ScoreText.text = DisplayString;
    //}
    
    void DisplayScores()
    {
        string DisplayString = "Scores: " + m_Scores;
        ScoreText.text = DisplayString;
    }
}
