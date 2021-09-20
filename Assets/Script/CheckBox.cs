using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    public Sprite Benar;
    public Sprite Salah;

    private Image m_Image;
    private bool m_animationCompleted; //Animasi
    private float m_FillAmount; //Fill Amount = Isi Jumlah
    void Start()
    {
        m_FillAmount = 0;
        m_animationCompleted = false;
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_Image.fillAmount = m_FillAmount; // menghilangkan 
        CustomizeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AnimationCompleted()
    {
        return m_animationCompleted;
    }
    public void Betul()
    {
        CustomizeAnimation();
        m_Image.sprite = Benar;

        StartCoroutine(FillingEffect());
    }

    public void Wrong()
    {
        CustomizeAnimation();
        m_Image.sprite = Salah;
        StartCoroutine(FillingEffect());
    }

    public void Clear()
    {
        m_FillAmount = 0;
        m_Image.fillAmount = m_FillAmount;
        m_animationCompleted = false;
    }


    IEnumerator FillingEffect() //Efek pengisian 
    {
        while(m_FillAmount < 1)
        {
            m_FillAmount += 0.05f;
            m_Image.fillAmount = m_FillAmount;

            yield return null;
        }

        m_animationCompleted = true;
    }

    private void CustomizeAnimation()
    {
        int fillMethod = 1;
        int origin = (int)Random.Range(0, 3);

        m_Image.fillMethod = (Image.FillMethod)fillMethod;
        m_Image.fillOrigin = origin;
    }
}
