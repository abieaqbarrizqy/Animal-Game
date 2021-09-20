using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [System.Serializable]

    //Membuat struktur

    public struct AnimalData
    {
        public string Name;
        public Sprite Animal;
        public bool Guessed; //ketika object hewan telah ketebak
        public bool BeenQuestioned; // diset benar, kapanpun di game tertentu
    }

    //Array Game
    public AnimalData[] KarnivoraDataSet;
    public AnimalData[] HerbivoraDataSet;
    public AnimalData[] OmnivoraDataSet;

    [HideInInspector]
    public AnimalData[] AnimalSetPerGame; //keinginan akan menahan hewan yang telah dipilih secara pasti
    [HideInInspector]
    public AnimalData[] AnimalDataSet;

    public static GameData Instance; //request data

    private void Awake()
    {
        if (Instance == null) //membuat instance tersebut dikatakan null
        {
            DontDestroyOnLoad(this); //jadi instance pertama dalam zona obyek di main menu tidak ingin menjadi hilang
            Instance = this; //fungsi mencegah hilangnya instance

        }
        else
            Destroy(this); //jika class dibuat baru lagi jadi ingin dihilangkan dikarenakan instance ini sudah menetapkan dari main menu
            //Kesimpulan, jadi diinginkan untuk memulai game dari menu utama jika tidak game tidak dapat data set 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignArrayOfAnimal()//Fungsi ini menetapkan himpunan dari hewan menjadi setting arusdataset
    {
        switch (GameSettings.Instance.GetJenisType())
        {
            case GameSettings.EJenisType.E_KARNIVORA:
                AnimalDataSet = new AnimalData[KarnivoraDataSet.Length];
                KarnivoraDataSet.CopyTo(AnimalDataSet, 0);
                break;

            case GameSettings.EJenisType.E_HERBIVORA:
                AnimalDataSet = new AnimalData[HerbivoraDataSet.Length];
                HerbivoraDataSet.CopyTo(AnimalDataSet, 0);
                break;

            case GameSettings.EJenisType.E_OMNIVORA:
                AnimalDataSet = new AnimalData[OmnivoraDataSet.Length];
                OmnivoraDataSet.CopyTo(AnimalDataSet, 0);
                break;
        }


    }
}
