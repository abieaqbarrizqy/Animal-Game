using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    public GameObject[] AnimalObjects;
    //Untuk Menampilakan text nama hewan
    public Text DisplayedText;


    private int NumberOfAnimalsObjects = 0;

    //Animal Data
    private ArusGameData m_GameData;
    // Start is called before the first frame update

    private bool IsFirstRun = false;
    void Start()
    {
        NumberOfAnimalsObjects = AnimalObjects.Length;
        //tetap pegang contoh di game data dari ArusGameData, jadi di editor scene ini, ingin menggunakan animal managar ini dan harus membuat objet "GameDataObject"
        //objek ini akan memiliki ArusGameData Script ini
        m_GameData = GameObject.Find("GameDataObject").GetComponent<ArusGameData>() as ArusGameData;
        IsFirstRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFirstRun) FirstRun();
    }

    void FirstRun()//pada dasarnya menetapkan hewan 
    {
        AssignAnimals();
        StartCoroutine(LoopRotation(140.0f));
        UpdateDisplayedText();
        IsFirstRun = false;
    }

    public void LoadNextGame() //Loop Rotation
    {
        StartCoroutine(LoopRotation(140.0f));
        m_GameData.GetNewAnimals();
        UpdateDisplayedText();
    }

    //fungsi ini benar" ditugaskan sprite tertentu untuk animal object
    public void AssignAnimals()
    {
        //jadi pada dasarnya ingin memilih hewan secara random dimana  memiliki final tampilan hewan dan ini akan menjadi berbeda and mempunyai 3-2 situasi
        int FinalAnimalPosition = (int)Random.Range(0, NumberOfAnimalsObjects);

        switch(FinalAnimalPosition)
        {
            case 0 : AnimalObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFinalAnimalIndex());
                     AnimalObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFirstAnimalIndex());
                     AnimalObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetSecondAnimalIndex());

                //CheckBox
                AnimalObjects[0].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFinalAnimalIndex());
                AnimalObjects[1].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFirstAnimalIndex());
                AnimalObjects[2].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetSecondAnimalIndex());
                break;
            case 1 :
                     AnimalObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFirstAnimalIndex());
                     AnimalObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFinalAnimalIndex());
                     AnimalObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetSecondAnimalIndex());

                AnimalObjects[0].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFirstAnimalIndex());
                AnimalObjects[1].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFinalAnimalIndex());
                AnimalObjects[2].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetSecondAnimalIndex());
                break;
            case 2:
                     AnimalObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFirstAnimalIndex());
                     AnimalObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetSecondAnimalIndex());
                     AnimalObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnimalSpriteIndex(m_GameData.GetFinalAnimalIndex());

                AnimalObjects[0].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFirstAnimalIndex());
                AnimalObjects[1].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetSecondAnimalIndex());
                AnimalObjects[2].GetComponent<Animal>().SetAnimalIndex(m_GameData.GetFinalAnimalIndex());
                break;
        }

    }

    //Loop Rotation
    IEnumerator LoopRotation(float angle)
    {
        float dir = 1f;
        float rotSpeed = 140.0f;
        float startAngle = angle;
        bool assigned = false;

        while(angle > 0)
        {
            float step = Time.deltaTime * rotSpeed;
            AnimalObjects[0].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            AnimalObjects[1].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            AnimalObjects[2].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);

            //Object saat ini berubah
            if(angle <= (startAngle / 3) && assigned == false)
            {
                AssignAnimals();
                assigned = true;
            }

            angle -= 2; //rotate 2 derajat

            yield return null;
        }
        AnimalObjects[0].GetComponent<Transform>().rotation = Quaternion.identity;
        AnimalObjects[1].GetComponent<Transform>().rotation = Quaternion.identity;
        AnimalObjects[2].GetComponent<Transform>().rotation = Quaternion.identity;
    }

    //Menampilkan nama text hewan
    void UpdateDisplayedText()
    {
        DisplayedText.text = m_GameData.GetAnimalsName(m_GameData.GetFinalAnimalIndex());
    }
}
