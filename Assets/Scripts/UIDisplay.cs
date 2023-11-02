using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Health PlayerHealth;
    [SerializeField] Slider slider;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextMeshProUGUI textHp;
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        PlayerHealth = Player.GetComponent<Health>();;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerHP"))
        {
            slider.maxValue = PlayerHealth.GetHP();
        }
        else
        {
            slider.maxValue = PlayerPrefs.GetFloat("PlayerHP");
        }
    }
    void Update()
    {
        slider.value = PlayerHealth.GetHP();
        textHp.text = slider.value.ToString();
        textMeshProUGUI.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
