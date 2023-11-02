using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] List<ShipUpgradeSO> list;
    [SerializeField] TextMeshProUGUI buffName;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI cost_Success;
    [SerializeField] TextMeshProUGUI cost_NotSucc;
    [SerializeField] GameObject Player;
    Health health;
    Shooter shooter;
    int count = 0;
    private void Awake()
    {
        health = Player.GetComponent<Health>();
        shooter = Player.GetComponent<Shooter>();
    }
    public void Buy()
    {
        if (PlayerPrefs.GetFloat("Money") < list[count].GetBuffCost())
        {
            cost_NotSucc.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_NotSucc.gameObject));
        }
        else if (PlayerPrefs.GetFloat("Money") > list[count].GetBuffCost())
        {
            if (count == 0)
            {
                health.Upgrade();
            }
            else if (count == 1)
            {
                shooter.Upgrade();
            }
            float t = PlayerPrefs.GetFloat("Money");
            t -= list[count].GetBuffCost();
            PlayerPrefs.SetFloat("Money", t);
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

        //if (!PlayerPrefs.HasKey("Upgrade"))
        //{
        //    count = 0;
        //}
        //else
        //{
        //    Load();
        //}
        ChangeBuff(count);
    }
    public void Next()
    {
        count++;
        if (count >= list.Count)
        {
            count = 0;
        }
        ChangeBuff(count);
        //Save();
    }
    public void Back()
    {
        count--;
        if (count < 0)
        {
            count = list.Count - 1;
        }
        ChangeBuff(count);
        //Save();
    }
    private void ChangeBuff(int count)
    {
        buffName.text = list[count].GetBuffName();
        cost.text = list[count].GetBuffCost().ToString();
    }
    //void Load()
    //{
    //    count = PlayerPrefs.GetInt("Upgrade");
    //}
    //void Save()
    //{
    //    PlayerPrefs.SetInt("Upgrade", count);
    //}
}
