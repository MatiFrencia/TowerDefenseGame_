using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wavephase
{
    PHASE_1 = 0,
    PHASE_2 = 1,
    PHASE_3 = 2,
    PHASE_4 = 3,
    PHASE_5 = 4
}

public class Spawner : MonoBehaviour
{
    public static Transform[] SpawnPoints;
    public GameObject[] Enemies;
    public Transform target;
    public ProgressBar progressBar;
    private int WaveIndex = 0;
    public int WaveLimit = 25;
    public float timer = 2.5f;
    public float timeOfWaves = 6.5f;
    public int enemiesPerWave = 7;
    public Wavephase wavephase;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            SpawnPoints[i] = transform.GetChild(i);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        wavephase = Wavephase.PHASE_1;

        WaveLimit = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveIndex != WaveLimit)
        {
            if (timer <= 0.0f)
            {
                StartCoroutine(WavePhaseState());
                timer = timeOfWaves;
            }
        }

        timeOfWaves -= Time.deltaTime;
        progressBar.current = timer;
    }

    IEnumerator WavePhaseState()
    {
        switch (wavephase)
        {
            case Wavephase.PHASE_1:
                WaveIndex++;
                wavephase = Wavephase.PHASE_2;
                break;
            case Wavephase.PHASE_2:
                enemiesPerWave += 2;
                WaveIndex++;
                wavephase = Wavephase.PHASE_3;
                break;
            case Wavephase.PHASE_3:
                enemiesPerWave += 2;
                WaveIndex++;
                wavephase = Wavephase.PHASE_4;
                break;
            case Wavephase.PHASE_4:
                enemiesPerWave += 2;
                WaveIndex++;
                wavephase = Wavephase.PHASE_5;
                break;
            case Wavephase.PHASE_5:
                enemiesPerWave += 2;
                WaveIndex++;
                break;
            default:
                break;
        }

        for (int i = 0; i< enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, Enemies.Length);
        int randomPoints = Random.Range(0, SpawnPoints.Length);
        Enemies[randomEnemy].GetComponent<Unit>().Target = target;
        var enemy = Instantiate(Enemies[randomEnemy], SpawnPoints[randomPoints].position, transform.rotation);
    }

}
