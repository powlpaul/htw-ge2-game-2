using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	
	[SerializeField] private Wave[] waves;
	[SerializeField] private Wave[] waves2;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;
	[Header("Wave Values")]
	[SerializeField] int numOfWaves;
	[SerializeField] Vector2 waveSizeMinMax;
	[SerializeField] Vector2 waveRateMinMax;
	[SerializeField] GameObject[] enemies;
	//public Text waveCountdownText;
	private static bool isWave = false;
	public GameManager gameManager;

	private int waveIndex = 0;

    private void Start()
    {
		InitializeWave();
		MenuManager menu = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();
		menu.Setup(waves2.Length);
	}

	private void InitializeWave()
    {
		waves2 = new Wave[numOfWaves];

		for(int i = 0;i < numOfWaves; i++)
        {
			waves2[i] = new Wave(
				enemies[0],
				(int) Mathf.Lerp(waveSizeMinMax.x, waveSizeMinMax.y,(float) i / numOfWaves), 
				Mathf.Lerp(waveRateMinMax.x, waveRateMinMax.y,(float) i / numOfWaves)
			);
        }
    }
    void Update ()
	{
	

		if (waveIndex == waves2.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}
		if (isWave && EnemiesAlive == 0)
		{
			isWave = false;
			GameObject[] banks = GameObject.FindGameObjectsWithTag("Bank");
			foreach(GameObject bank in banks)
            {
				bank.GetComponent<Bank>().TickUp();
            }
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

		Wave wave = waves2[waveIndex];

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
