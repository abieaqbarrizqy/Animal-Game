using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kita ingin tahu, button mana yang dipilih 
public class GameSettings : MonoBehaviour
{
    private Dictionary<EJenisType, string> _SceneName = new Dictionary<EJenisType, string>();

    public enum EGameMode //GameModePanel
    {
        NOTE_SET,
        TIME_TRAIL_MODE, //Mode Waktu
        SURVIVAL_MODE, //3 nyawa
        SHORT_MODE, //15 Pertanyaan
    }
    public enum EJenisType
    {
        E_NOT_SET = 0,
        E_KARNIVORA,
        E_HERBIVORA,
        E_OMNIVORA,
    };

    private EGameMode _GameMode;

    private EJenisType _Jenis;
    private string _JenisName;

    public static GameSettings Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetSceneNameAndType();
        _GameMode = EGameMode.NOTE_SET;
        _Jenis = EJenisType.E_NOT_SET;
    }

    private void SetSceneNameAndType()
    {
        _SceneName.Add(EJenisType.E_KARNIVORA, "GameScene");
        _SceneName.Add(EJenisType.E_HERBIVORA, "GameScene");
        _SceneName.Add(EJenisType.E_OMNIVORA, "GameScene");
    }

    public string GetJenisSceneName()
    {
        string name;
        if(_SceneName.TryGetValue(_Jenis, out name)) //mendapat hasil dari dictionary "SetSceneNameandType()"
        {
            return name; //Jika sukses dia akan memanggil nama dari "GameScene"
        }
        else
        {
            Debug.Log("ERROR: SCENE JENIS TIDAK DITEMUKAN");
            return ("ERROR: SCENE JENIS TIDAK DITEMUKAN");
        }
    }
   public void SetJenisType(EJenisType type)
    {
        _Jenis = type;
    }

    public void SetGameMode(EGameMode mode) //GameModePanel
    {
        _GameMode = mode;
    }

    public EGameMode GetGameMode() //GameModePanel
    {
        return _GameMode;
    }

    public EJenisType GetJenisType()
    {
        return _Jenis;
    }

    public void SetJenisName(string Name)
    {
        SetJenisType(GetJenisTypeFromString(Name));
        _JenisName = Name;
    }

    private EJenisType GetJenisTypeFromString(string type)
    {
        switch(type)
        {
            case "KARNIVORA": return EJenisType.E_KARNIVORA;
            case "HERBIVORA": return EJenisType.E_HERBIVORA;
            case "OMNIVORA": return EJenisType.E_OMNIVORA;

            default: return EJenisType.E_NOT_SET;
        }
    }
}
