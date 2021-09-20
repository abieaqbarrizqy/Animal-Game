using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] s_audioList;

    List<AudioSource> s_source = new List<AudioSource>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < s_audioList.Length; i++)
        {
            s_source.Add(new AudioSource());
            s_source[i] = gameObject.AddComponent<AudioSource>();
            s_source[i].clip = s_audioList[i];
        }
    }

    public void s_playsound(int s)
    {
        s_source[s].Play();
    }
}
