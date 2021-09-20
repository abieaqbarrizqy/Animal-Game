 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMM : MonoBehaviour
{
    [SerializeField]
    public GameObject Panel;

public void OpenPanel()
   {
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
  }

    public void ClosePane()
    {
        if(Panel != null)
        {
            Panel.SetActive(false);
        }

    }    
}
