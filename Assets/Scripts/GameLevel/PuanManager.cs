using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuanManager : MonoBehaviour
{
    private int toplampuan, puanartisi;
    [SerializeField]
    private Text puanText;

    void Start()
    {
        puanText.text = toplampuan.ToString();
    }

    public void PuanArttir(string zorlukseviyesi)
    {
        switch (zorlukseviyesi)
        {
            case "kolay":
                puanartisi = 5;
                break;
            case "orta":
                puanartisi = 10;
                break;
            case "zor":
                puanartisi = 15;
                break;
        }

        toplampuan += puanartisi;
        puanText.text = toplampuan.ToString();
    }

}
