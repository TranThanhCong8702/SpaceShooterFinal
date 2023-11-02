using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfigSO> list;
    WaveConfigSO WaveConfigSO;
    [SerializeField] float timebtwwaves = 2f;
    [SerializeField] bool isLooping;
    int HigherDiffCount = 0;
    BackgroundScroll back;
    private void Awake()
    {
        back = FindObjectOfType<BackgroundScroll>();
    }
    void Start()
    {
        StartCoroutine(TheEnemySpawner());
    }

     public WaveConfigSO getCurrWave()
    {
        return WaveConfigSO;
    }
    IEnumerator TheEnemySpawner()
    {
        do
        {
            for (int j = HigherDiffCount; j<list.Count;j++)
            {
                WaveConfigSO = list[j];
                for (int i = 0; i < WaveConfigSO.GetenemyCount(); i++)
                {
                    Instantiate(WaveConfigSO.GetEnemyPrefabs(i), WaveConfigSO.GetStartWayPoints().position, Quaternion.Euler(0,0,180), transform);
                    yield return new WaitForSeconds(WaveConfigSO.GetTimeVariance());
                }
                yield return new WaitForSeconds(timebtwwaves);
            }
            back.FasterBackground();
            HigherDiffCount++;
            if (HigherDiffCount >= list.Count - 1)
            {
                HigherDiffCount = list.Count - 2;
            }
            timebtwwaves = timebtwwaves / 1.2f;
        }
        while (isLooping);
        
    }
    void Update()
    {

    }
}
