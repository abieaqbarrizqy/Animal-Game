using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timeLeft;
    private ArusGameData m_GameData;
    public GUIStyle ClockStyle;
    private bool StartedGameOverTimer = false;

    //Game Over
    public GUIStyle GameOverStyle;
    public GameObject GameOverPanel;
    public GameObject TextJawabanBenar;
    public GameObject TextJawabanSalah;

    public List<GameObject> Jam;

    private Scores m_Scores;

    private bool EndGUIActivated;
    // Start is called before the first frame update
    void Start()
    {
        StartedGameOverTimer = false;
        EndGUIActivated = false;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<ArusGameData>() as ArusGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;

        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.TIME_TRAIL_MODE)
        {
            this.enabled = true; //jika berada di mode ini, maka waktu akan ditampilkan
            foreach (GameObject J in Jam)
            {
                J.SetActive(true);
            }
        }
        else
            this.enabled = false;// yang ini sebaliknya, jika tidak maka tidak ditampilkan
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(EndGUIActivated && StartedGameOverTimer == false)//Fungsi untuk gameover
        {
            StartedGameOverTimer = true;
            StartCoroutine(Sleep(2.0f));
        }
    }

    private void OnGUI()
    {
        if(timeLeft > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 20, 10, 200, 100), "" + (int)timeLeft, ClockStyle);
        }
        else
        {
            ActivateGameOverGUI();
        }
    }

    IEnumerator Sleep(float sleepTime)
    {
        yield return new WaitForSeconds(sleepTime);

        if(m_GameData.HasGameFinished() == false && EndGUIActivated == true)
        {
            m_GameData.SetGameOver();
        }
        ActivateGameOverGUI();
    }

    private void ActivateGameOverGUI()
    {
        TextJawabanBenar.GetComponent<Text>().text = m_Scores.GetArusScore().ToString();
        TextJawabanSalah.GetComponent<Text>().text = m_Scores.GetArusWrongScore().ToString();
        GameOverPanel.SetActive(true);
        EndGUIActivated = true;
    }
}
