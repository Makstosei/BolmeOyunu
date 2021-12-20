using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject kareprefab;

    [SerializeField]
    private Transform karelerPanel;

    [SerializeField]
    private Text SoruText;

    private GameObject[] Karelerdizisi = new GameObject[25];

    [SerializeField]
    private Transform SoruPaneli;

    List<int> bolumdegerleriListesi = new List<int>();

    int bolensayi, bolunensayi, kacincisoru, butondegeri, dogrusonuc, kalanhaklar;
    bool oyunbasladimi = false;
    string sorununZorlukDerecesi;

    hakmanager hakmanager;
    PuanManager puanManager;

    GameObject gecerlikare;

    [SerializeField]
    private Sprite[] karesprites;

    [SerializeField]
    private GameObject Sonucpaneli;

    [SerializeField]
    AudioSource audioSource;

    public AudioClip butonsesi;


    private void Awake()
    {
        kalanhaklar = 3;
        hakmanager = Object.FindObjectOfType<hakmanager>();
        hakmanager.kalanhaklarikontrolet(kalanhaklar);
        puanManager = Object.FindObjectOfType<PuanManager>();
        Sonucpaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        SoruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareOlustur();

    }

    public void KareOlustur()
    {

        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(kareprefab, karelerPanel);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = karesprites[Random.Range(0, karesprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            Karelerdizisi[i] = kare;
        }
        BölümDegerleriniTextlereYaz();
        StartCoroutine(DoFadeRoutine());
        Invoke("SoruPaneliniAc", 1f);

    }

    private void ButonaBasildi()
    {
        if (oyunbasladimi)
        {
            audioSource.PlayOneShot(butonsesi);
            butondegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            gecerlikare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            SonucKontrol();
        }

    }

    private void SonucKontrol()
    {
        if (butondegeri == dogrusonuc)
        {
            gecerlikare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerlikare.transform.GetChild(0).GetComponent<Text>().text = "";
            gecerlikare.transform.GetComponent<Button>().interactable = false;
            bolumdegerleriListesi.RemoveAt(kacincisoru);
            puanManager.PuanArttir(sorununZorlukDerecesi);
            Debug.Log(bolumdegerleriListesi.Count);

            if (bolumdegerleriListesi.Count > 0)
            {
                SoruPaneliniAc();
            }
            else
            {
                Oyunbitti();

            }

        }

        else
        {
            kalanhaklar--;
            hakmanager.kalanhaklarikontrolet(kalanhaklar);
        }
        if (kalanhaklar <= 0)
        {
            Oyunbitti();


        }
    }


    void Oyunbitti()
    {
        oyunbasladimi = false;
        Sonucpaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in Karelerdizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 1);
            yield return new WaitForSeconds(0.05f);
        }


    }

    void BölümDegerleriniTextlereYaz()
    {

        foreach (var kare in Karelerdizisi)
        {

            int rastgaleSayi = Random.Range(1, 13);
            bolumdegerleriListesi.Add(rastgaleSayi);
            kare.transform.GetChild(0).GetComponent<Text>().text = rastgaleSayi.ToString();

        }

    }


    void SoruPaneliniAc()
    {
        SoruyuSor();
        SoruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        oyunbasladimi = true;
    }

    void SoruyuSor()
    {
        bolensayi = Random.Range(2, 11);
        kacincisoru = Random.Range(0, bolumdegerleriListesi.Count);
        dogrusonuc = bolumdegerleriListesi[kacincisoru];
       
        bolunensayi = bolensayi * dogrusonuc;
        if (bolunensayi <= 40)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if (bolunensayi > 40 && bolunensayi <= 80)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }



        SoruText.text = bolunensayi.ToString() + "  : " + bolensayi.ToString();
    }
}
