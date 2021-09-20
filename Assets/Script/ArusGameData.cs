using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArusGameData : MonoBehaviour
{
    [HideInInspector]
    public int FirstAnimalIndex = 0;
    [HideInInspector]
    public int SecondAnimalIndex;
    [HideInInspector]
    public int FinalAnimalIndex;

    private int PrevFinalAnimalIndex; //Untuk membuat gambar animal ini tidak sama disaat ulang nanti
    private int AnimalsPerGame = 35; //Berapa banyak hewan harus di satu game

    private bool GameFinished = false;

    public void ResetGameOver() { GameFinished = false; }
    public bool HasGameFinished() { return GameFinished; }
    public void SetGameOver() { GameFinished = true; }

    public static bool AllowDestroyOnLoad = false;
    public void Awake()
    {
        if (AllowDestroyOnLoad == false)
            DontDestroyOnLoad(this);//jika ganti scene, script tidak akan hilang
        else
            AllowDestroyOnLoad = false;
    }

    void Start()
    {
        FinalAnimalIndex = 0;
        PrevFinalAnimalIndex = 0;
        FirstAnimalIndex = 0;
        SecondAnimalIndex = 0;
        GameData.Instance.AssignArrayOfAnimal();

        if (AnimalsPerGame >= GameData.Instance.AnimalDataSet.Length)
            AnimalsPerGame = GameData.Instance.AnimalDataSet.Length;

        GameData.Instance.AnimalSetPerGame = new GameData.AnimalData[AnimalsPerGame];
        GameFinished = false;
        PickAnimalsForGame();
        GetNewAnimals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickAnimalsForGame() //Fungsi Prediksi
    {
        int PickedAnimalNumber = 0;

        for(int Index = 0; Index < GameData.Instance.AnimalDataSet.Length; Index++)
        {
            if (PickedAnimalNumber >= AnimalsPerGame)
                break;
            else
            {
                if(GameData.Instance.AnimalDataSet[Index].Guessed == false) //Menghindari fungsi duplikat(menampilkan ulang yang sama lagi)
                {
                    GameData.Instance.AnimalSetPerGame[PickedAnimalNumber] = GameData.Instance.AnimalDataSet[Index];
                    PickedAnimalNumber++;
                }
            }
        }

        if (PickedAnimalNumber < AnimalsPerGame - 1)
        {
            //Jika tidak mencukup hewan, pilih random
            for (int Index = 0; Index < GameData.Instance.AnimalDataSet.Length; Index++)
            {
                if (PickedAnimalNumber >= AnimalsPerGame)
                    break;
                else
                {
                    if (GameData.Instance.AnimalDataSet[Index].Guessed == true)
                    {
                        GameData.Instance.AnimalSetPerGame[PickedAnimalNumber] = GameData.Instance.AnimalDataSet[Index];
                        PickedAnimalNumber++;
                    }
                }
            }
        }
    }

    public void GetNewAnimals()
    {
        PrevFinalAnimalIndex = FinalAnimalIndex;
        if (GetNumberOfAnimalsLeft() > 0)
        {
            do
            {
                //Mendapat nomor Random dari 0 ke Max data game
                FinalAnimalIndex = (int)Random.Range(0, GameData.Instance.AnimalSetPerGame.Length);
            }
            /// jadi tidak diinginkan untuk mendapatkan yang sama disaat telah memilih
            /// Di Game Sbelumnya dan tidak diinginkan untuk juga mendapatkan hewan yang telah dipilih 
            /// yang telah tidak dipilih jadi ingin menjaga looping sampai memuaskan apapun di kondisi
            while (PrevFinalAnimalIndex == FinalAnimalIndex || GameData.Instance.AnimalSetPerGame[FinalAnimalIndex].Guessed == true);

            do
            {
                FirstAnimalIndex = (int)Random.Range(0, GameData.Instance.AnimalSetPerGame.Length);
            }
            while (FirstAnimalIndex == FinalAnimalIndex);

            do
            {
                SecondAnimalIndex = (int)Random.Range(0, GameData.Instance.AnimalSetPerGame.Length);
            }
            while (SecondAnimalIndex == FirstAnimalIndex || SecondAnimalIndex == FinalAnimalIndex);

            GameData.Instance.AnimalSetPerGame[FinalAnimalIndex].BeenQuestioned = true; //akan tau hewan mana yang telah ditanyai
        }

        else
        {
            //Level telah selesai
            GameFinished = true;
        }
    }

    private int GetNumberOfAnimalsLeft()
    {
        //Memulai dari 0 lalu akan dilanjutkan 
        int left = 0;
        
        for(int Index = 0; Index < GameData.Instance.AnimalSetPerGame.Length; Index++)
        {
            if (GameData.Instance.AnimalSetPerGame[Index].Guessed == false)
                left++;
        }
        return left;
    }

    //Menampilkan nama text hewan
    public string GetAnimalsName(int Index)
    {
        return GameData.Instance.AnimalSetPerGame[Index].Name;
    }

    public int GetAnimalNameLength(int AnimalIndex)
    {
        return GameData.Instance.AnimalSetPerGame[AnimalIndex].Name.Length;
    }

    public int GetFirstAnimalIndex()
    {
        return FirstAnimalIndex;
    }

    public int GetSecondAnimalIndex()
    {
        return SecondAnimalIndex;
    }

    public int GetFinalAnimalIndex()
    {
        return FinalAnimalIndex;
    }

    public void SetGuessed()
    {
        GameData.Instance.AnimalSetPerGame[FinalAnimalIndex].Guessed = true;
    }

    public Sprite GetAnimalSpriteIndex(int AnimalIndex)
    {
        return GameData.Instance.AnimalSetPerGame[AnimalIndex].Animal;
    }

    public int GetAnimalNumber() { return GameData.Instance.AnimalDataSet.Length; }
}
