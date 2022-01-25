using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	[SerializeField] private Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	//public Text waveCountdownText;
	private static bool isWave = false;
	public GameManager gameManager;

	private int waveIndex = 0;
    private void Start()
    {
		MenuManager menu = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();
		menu.Setup(waves.Length);
	}
    void Update ()
	{
	

		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}
		if (EnemiesAlive == 0)
		{
			isWave = false;
			return;
		}
	}
	public void StartNextWave()
    {
		isWave = true;
		Debug.Log("test");
		StartCoroutine(SpawnWave());
		
	}

	public static bool GetWaveState()
    {
		return isWave;
    }
	IEnumerator SpawnWave ()
	{
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
