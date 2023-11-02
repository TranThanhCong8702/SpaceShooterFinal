using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float Hp = 50f;
    [SerializeField] float Points = 50f;
    [SerializeField] bool IsPlayer;
    [SerializeField] ParticleSystem Paticle;
    [SerializeField] bool applyCamShake;
    BoostController boost;
    ScoreKeeper score;
    CamShake cam;
    AudioPlayer player;
    LevManager levManager;
    public float GetHP()
    {
        return Hp;
    }
    public void Upgrade()
    {
        if (!IsPlayer)
        {
            return;
        }
        Hp += 10f;
        PlayerPrefs.SetFloat("PlayerHP", Hp);
    }
    private void Awake()
    {
        cam = Camera.main.GetComponent<CamShake>();
        player = FindObjectOfType<AudioPlayer>();
        score = FindObjectOfType<ScoreKeeper>();
        levManager = FindObjectOfType<LevManager>();
        boost = GetComponent<BoostController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DameDealer dameDealer = collision.GetComponent<DameDealer>();
        if(dameDealer != null)
        {
            PlayParticleSys();
            TakeDame(dameDealer.GetDmae());
            player.PlayDamagingClip();
            PlayCamShake();
            dameDealer.Hit();
        }
        if(collision.tag == "boostHP")
        {
            Debug.Log("BHP");
            Boost b = collision.GetComponent<Boost>();
            if (b == null) { Debug.Log("Hp Failed"); return; }
            Hp += b.FullHP(Hp);
            if(Hp > PlayerPrefs.GetFloat("PlayerHP"))
            {
                Debug.Log(PlayerPrefs.GetFloat("PlayerHP"));
                Hp = PlayerPrefs.GetFloat("PlayerHP");
            }
            b.Hit();
        }
    }
    void TakeDame(float dame)
    {
        Hp -= dame;
        if(Hp <= 0)
        {
            if (!IsPlayer && score != null)
            {
                score.AddScore(Points);
                if(boost != null)
                {
                    boost.PopOut();
                }
            }
            else if (IsPlayer && levManager != null)
            {
                levManager.LoadScene("Ending");
            }
            Destroy(gameObject);
        }
    }
    void PlayParticleSys()
    {
        if(Paticle != null)
        {
            ParticleSystem pat = Instantiate(Paticle, transform.position, Quaternion.identity);
            Destroy(pat.gameObject, pat.main.duration + pat.main.startLifetime.constantMax);
        }
    }
    void PlayCamShake()
    {
        if(cam != null && applyCamShake) 
        {
            cam.Play();
        }
    }

    void Start()
    {
        if (IsPlayer)
        {
            if (!PlayerPrefs.HasKey("PlayerHP"))
            {
                PlayerPrefs.SetFloat("PlayerHP", Hp);
                Debug.Log("Not have new Hp");
            }
            else
            {
                Hp = PlayerPrefs.GetFloat("PlayerHP");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
