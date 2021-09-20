using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalLives : MonoBehaviour
{
    public List<GameObject> Nyawa;
    public GameObject GameOverPanel;
    public GameObject TebakanBenar;
    public GameObject TebakanSalah;
    private int JumlahNyawa = 3;
    private Scores m_Scores;


    private ArusGameData m_GameData;
    // Start is called before the first frame update
    void Start()
    {
        JumlahNyawa = 3;

        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
        {
            this.enabled = true;
            foreach (GameObject n in Nyawa)
            {
                n.SetActive(true);
            }
        }
        else
            this.enabled = false;

        m_GameData = GameObject.Find("GameDataObject").GetComponent<ArusGameData>() as ArusGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void RemoveLife()
    {
        if(JumlahNyawa > 0)
        {
            JumlahNyawa--;
            Nyawa[JumlahNyawa].SetActive(false);
        }
        if(JumlahNyawa == 0)
        {
            TebakanBenar.GetComponent<Text>().text = m_Scores.GetArusScore().ToString();
            TebakanSalah.GetComponent<Text>().text = m_Scores.GetArusWrongScore().ToString();
            GameOverPanel.SetActive(true);
            m_GameData.SetGameOver();
        }
    }
}
 