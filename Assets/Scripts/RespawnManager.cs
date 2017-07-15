using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RespawnManager : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 20.5f;
    private float countDown = 5.0f;

    public Text countDownText;

    private int waveIndex = 0;
    private int respawnNumberOfEnemy = 5;

    private WaitForSeconds spawnDelay = new WaitForSeconds(0.5f);

    private void Update()
    {
        if (countDown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;

        countDownText.text = Mathf.Round(countDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < respawnNumberOfEnemy; i++)
        {
            SpawnEnemy();
            yield return spawnDelay;
        }
        waveIndex++;
    }

    void SpawnEnemy()
    {
        //あとモデルを追加して変更予定。
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}