using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton, exitButton;


    
    void Start()
    {
        FadeOut();
    }


    //in for this one we using dg.tweening
  void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1, 1.5f).SetDelay(0.5f);
    }

    public void ExitGame()
    {

        Application.Quit();
    }


    // for this one we need unityengine.scenemanagement
    public void StartGame()
    {

        SceneManager.LoadScene("Level1"); 

    }


}
