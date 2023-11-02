using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject Shopcanvas;
    [SerializeField] List<BulletSO> list;
    [SerializeField] SpriteRenderer spriter;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI cost_Success;
    [SerializeField] TextMeshProUGUI cost_NotSucc;
    [SerializeField] TextMeshProUGUI TotalPoints;
    [SerializeField] GameObject BuyButton;
    int count = 0;
    public void OpenShop()
    {
        Canvas.SetActive(false);
        Shopcanvas.SetActive(true);
    }
    public void Backhome()
    {
        Canvas.SetActive(true);
        Shopcanvas.SetActive(false);
    }
    private void DisplayTotalPoints()
    {
        TotalPoints.text = "Total Points\n" + PlayerPrefs.GetFloat("Money"); 
    }
    public void Buy()
    {
        if(PlayerPrefs.GetFloat("Money") < list[count].GetPointToBuy() && !list[count].GetEquipedState())
        {
            cost_NotSucc.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_NotSucc.gameObject));
        }
        else if(PlayerPrefs.GetFloat("Money") > list[count].GetPointToBuy() && !list[count].GetEquipedState())
        {
            float t = PlayerPrefs.GetFloat("Money");
            t -= list[count].GetPointToBuy();
            PlayerPrefs.SetFloat("Money", t);
            list[count].SetIsEquiped();
            cost_Success.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_Success.gameObject));
        }
    }
    IEnumerator Disable(GameObject x)
    {
        yield return new WaitForSecondsRealtime(1f);
        x.SetActive(false);
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", 0);
        }

        if (!PlayerPrefs.HasKey("BulletCount"))
        {
            count = 0;
        }
        else
        {
            Load();
            Debug.Log(PlayerPrefs.GetInt("BulletCount"));
        }
        ChangeBullet(count);
        DisplayTotalPoints();
    }
    private void Update()
    {
        if (list[count].GetEquipedState())
        {
            GetPlayBullet();
            BuyButton.SetActive(false);
        }
        else if (!list[count].GetEquipedState())
        {
            BuyButton.SetActive(true);
        }
        DisplayTotalPoints();
    }
    private void GetPlayBullet()
    {
        PlayerPrefs.SetInt("bulletIndex", count);
    }
    public void Next()
    {
        count++;
        if (count >= list.Count)
        {
            count = 0;
        }
        ChangeBullet(count);
        Save();
    }
    public void Back()
    {
        count--;
        if (count < 0)
        {
            count = list.Count - 1;
        }
        ChangeBullet(count);
        Save();
    }
    private void ChangeBullet(int count)
    {
        spriter.sprite = list[count].GetShopIcon();
        cost.text = list[count].GetPointToBuy().ToString();
    }
    void Load()
    {
        if(PlayerPrefs.GetInt("BulletCount") >= list.Count)
        {
            count = list.Count - 1;
        }
        else
        {
            count = PlayerPrefs.GetInt("BulletCount");
        }
    }
    void Save()
    {
        PlayerPrefs.SetInt("BulletCount", count);
        Debug.Log(PlayerPrefs.GetInt("BulletCount"));
    }
}
