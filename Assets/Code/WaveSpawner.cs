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
	[SerializeField] SpawningScheme[] spawningSchemes;
	//public Text waveCountdownText;
	[SerializeField] private static bool isWave = false;
	public GameManager gameManager;

	[SerializeField] private int waveIndex = 0;

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
			Wave newWave = new Wave();
			foreach(SpawningScheme spawningScheme in spawningSchemes)
            {
				if(i >= spawningScheme.startWave && i <= spawningScheme.finalWave)newWave.enemiesInWave.Add(new EnemyInWave(
					spawningScheme.enemy,
					(int) Mathf.Lerp(spawningScheme.minMaxAmount.x, spawningScheme.minMaxAmount.y,(float) (i - spawningScheme.startWave) / (spawningScheme.finalWave - spawningScheme.startWave)),
					0.5f
					));
            }
			waves2[i] = newWave;
        }
    }
    void Update ()
	{
	

	
		if (isWave && EnemiesAlive == 0)
		{
			if (waveIndex == waves2.Length && !GameManager.GameIsOver)
			{
				//Debug.Log("you won the game");
				gameManager.WinLevel();
				this.enabled = false;
				return;

			}
			isWave = false;
			GameObject[] banks = GameObject.FindGameObjectsWithTag("Bank");
			foreach(GameObject bank in banks)
            {
				bank.GetComponent<Bank>().TickUp();
            }
			PlayerStats.Money += 100 ;
			return;
		}
	}
	public void StartNextWave()
    {
		if (waveIndex == waves2.Length )
		{
			gameManager.WinLevel();
			this.enabled = false;
			return;
		}
		AudioMaster.AM.PlayTurretClickSound();
		isWave = true;
		Debug.Log("test");
		StartCoroutine(SpawnWave2());
		//SpawnWave2();
		
	}

	public static bool GetWaveState()
    {
		return isWave;
    }

	IEnumerator SpawnWave2()
    {
		PlayerStats.Rounds++;

		Wave wave = waves2[waveIndex];

		EnemiesAlive = wave.GetCount();

		foreach (EnemyInWave enemyInWave in wave.enemiesInWave)
        {
			StartCoroutine(SpawnEnemy2(enemyInWave.enemy, enemyInWave.count));
			yield return new WaitForSeconds(0.5f);
		}

			//while(isWave)
			//SpawnEnemy(wave.enemy);
		
		//}

		waveIndex++;
	}

	IEnumerator SpawnEnemy2(GameObject enemy, int amount)
    {
		for(int i = 0; i < amount; i++)
        {
			Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
		}
		
	}
	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
