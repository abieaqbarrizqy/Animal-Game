using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject GameModePanel; //untuk GameModePanel
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fungsi Tombol Button
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAndClearData(string sceneName)
    {
        ArusGameData.AllowDestroyOnLoad = true;
        SceneManager.LoadScene(sceneName);
    }

    public void ShowGameModePanel() //GameMOdePanel
    {
        GameModePanel.SetActive(true);
    }

    public void HideGameModePanel() //GameMOdePanel
    {
        GameModePanel.SetActive(false);
    }

    public void StartTimeTrail()//Fungsi untuk jika memilih mode ini
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.TIME_TRAIL_MODE);
        LoadScene(GameSettings.Instance.GetJenisSceneName());
    }

    public void StartSurvivalGame()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.SURVIVAL_MODE);
        LoadScene(GameSettings.Instance.GetJenisSceneName());
    }

    public void StartShortGame()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.SHORT_MODE);
        LoadScene(GameSettings.Instance.GetJenisSceneName());
    }

    public void Exit()
    {
        Application.Quit();
    }
}
