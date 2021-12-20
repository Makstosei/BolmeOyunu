using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{
    public void OyunaYenidenBasla()
    {
        SceneManager.LoadScene("level1");
    }


    public void Anamenü()
    {
        SceneManager.LoadScene("Menu");


    }


}



