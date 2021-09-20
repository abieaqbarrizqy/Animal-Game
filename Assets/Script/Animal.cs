using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private AnimalManager m_AnimalManager; //>LoopRotation
    private bool LoadNewGame = false; //>LoopRotation
    private bool ButtonPressed = false; //>LoopRotation

    [HideInInspector]
    public int AnimalIndex = 0;

    private CheckBox m_CheckBox;
    private ArusGameData m_GameData;
    private Scores m_Scores;

    private SurvivalLives m_SurvivalLives;

    private ShortGameMode m_ShortGame;
    // Start is called before the first frame update
    void Start()
    {
        //>LoopRotation
        m_AnimalManager = GameObject.Find("Main Camera").GetComponent<AnimalManager>() as AnimalManager;
        m_CheckBox = GameObject.Find("CheckBox").GetComponent<CheckBox>() as CheckBox;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<ArusGameData>() as ArusGameData;
        m_SurvivalLives = GameObject.Find("Main Camera").GetComponent<SurvivalLives>() as SurvivalLives;
        m_ShortGame = GameObject.Find("Main Camera").GetComponent<ShortGameMode>() as ShortGameMode;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
    }

    // Update is called once per frame
    void Update()
    {
        //>LoopRotation
        if(LoadNewGame == true && ButtonPressed == false)
        {
            if(m_CheckBox.AnimationCompleted())
            {
                //jika kita memilih hewan object kita ingin memuat game baru jadi  object akan berputar dan mendapat object baru
                m_AnimalManager.LoadNextGame();
                LoadNewGame = false;
                m_CheckBox.Clear();
            }
        }
    }

    //>LoopRotation
    private void OnMouseDown()
    {
        //>LoopRotation
        if(ButtonPressed == false && m_GameData.HasGameFinished() == false) //jika menekan tombol kita ingin memuat game baru tapi variabel ini, dan fungsi ini mencegah kita untuk menkan double klik dibutton yang sama
        {
            if(AnimalIndex == m_GameData.GetFinalAnimalIndex()) //jika menebak dengan benar
            {
                m_CheckBox.Betul();
                m_Scores.AddScores();

                if(GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
                {
                    m_ShortGame.Rotate(true);
                }
            }
            else //Jika Jawab Salah
            {
                m_Scores.AddWrongScore();
                if(GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
                {
                    m_SurvivalLives.RemoveLife();
                }
                else if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
                {
                    m_ShortGame.Rotate(false);
                }
                m_CheckBox.Wrong();
            }

            LoadNewGame = true;
        }
        StartCoroutine(Sleep());
    }

    public void SetAnimalIndex(int index)
    {
        AnimalIndex = index;
    }

    IEnumerator Sleep()
    {
        ButtonPressed = true;
        yield return new WaitForSeconds(1.0f);
        ButtonPressed = false;
    }
}
