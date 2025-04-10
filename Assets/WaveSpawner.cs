using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemy;
    public Transform spawnPoint;
    public Transform endPoint;

    [SerializeField]
    private float WaveTime = 30f;

    private float timer = 10f;
    private int waveNumber = 0;
    private bool playButtonPressed = false;

    public TextMeshProUGUI waveCountdown;
    public TextMeshProUGUI pausedText;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI killDisplay;
    public TextMeshProUGUI finalScore;

    private int score = 0;
    private int kills = 0;

    private bool defenseAlive;

    void Start()
    {
    }

    void Update()
    {
        defenseAlive = endPoint.GetComponent<Defender>().isAlive;
        if (timer <= 0 && defenseAlive && playButtonPressed)
        {
            StartCoroutine(SpawnWave());
            timer = WaveTime;
        }
        else if (defenseAlive && playButtonPressed)
        {
            timer -= Time.deltaTime;
            waveCountdown.text = Mathf.Round(timer).ToString();
        }
        else
        {
            if(!defenseAlive)
            {
                Destroy(waveCountdown);
                finalScore.text = "Game Over!\nFinal Score:  " + scoreDisplay.text + "!";
            }
            waveCountdown.text = Mathf.Round(timer).ToString();
        }

        scoreDisplay.text = score.ToString();
        killDisplay.text = kills.ToString();
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        int rand = Random.Range(3, 0);

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy(rand);
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy(int rand)
    {
        Transform newEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        newEnemy.GetComponent<Enemy>().waveSpawner = this;
        newEnemy.GetComponent<Enemy>().tier = waveNumber % rand;
    }

    public void togglePlayButton()
    {
        if (playButtonPressed)
        {
            playButtonPressed = false;
            pausedText.text = "- Paused -";
        }
        else
        {
            playButtonPressed = true;
            pausedText.text = "";
        }
    }

    public void incrementScore(int newScore)
    {
        score += newScore;
    }

    public void incrementKillCount()
    {
        kills++;
    }
}
