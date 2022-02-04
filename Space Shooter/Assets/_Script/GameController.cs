using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject enemy;
    public GameObject battery;

    public Vector3 spawnValue;
    public int hazardCount;
    public int batteryCount;
    public int enemyCount;
    
    public float spawnWait_asteroid;
    public float spawnWait_battery;
    public float spawnWait_enemy;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text waveText;
    public Text restartText;
    public Text gameOverText;

    public Font myFont;

    private bool gameOver;
    private bool restart;
    private int score;

    private int waves = 1;

    int randomSeed;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.font = myFont;
        waveText.font = myFont;
        randomSeed = (int)System.DateTime.Now.Ticks;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        waves = 1;
        score = 0;
        UpdateScore();
        UpdateWave();
        StartCoroutine (SpawnWaves());
        StartCoroutine(SpawnBattery());
        StartCoroutine(SpawnEnemy());
    }
     
    void Update()
    {
        Debug.Log("spawnWait_enemy: " + spawnWait_enemy + " waves: " + waves + " enemyCount: " + enemyCount);
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (gameOver)
        {
            restart = true;
            restartText.text = "Press \"R\" to restart\nPress \"ESC\" to exit";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }

    // Control the asteroid waves
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                randomSeed = Random.Range(0, 100);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (randomSeed >= 0 && randomSeed <= 66)
                {
                    Instantiate(asteroid1, spawnPosition, spawnRotation);
                }
                else
                {
                    Instantiate(asteroid2, spawnPosition, spawnRotation);
                }
                yield return new WaitForSeconds(spawnWait_asteroid);
            }
            NextWave();
            UpdateWave();
            Difficulty();
            yield return new WaitForSeconds(waveWait);
        }
    }
    IEnumerator SpawnBattery()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < batteryCount; i++)
            {
                yield return new WaitForSeconds(spawnWait_battery);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(battery, spawnPosition, spawnRotation);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait_enemy);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    void NextWave()
    {
        waves += 1;
    }
    void UpdateWave()
    {
        waveText.text = "Wave: " + waves;
    }
    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
    }

    private void Difficulty()
    {
        if (enemyCount < 4)
        {
            if (waves % 2 == 0)
            {
                enemyCount++;
                spawnWait_enemy /= 2;
            }
        }
    }
}
