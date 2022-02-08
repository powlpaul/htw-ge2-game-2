using UnityEngine;
using System.Collections.Generic;
[System.Serializable]

public class Wave {

	
	public List<EnemyInWave> enemiesInWave;
	
	public Wave()
    {
		this.enemiesInWave = new List<EnemyInWave>();
    }
	public int GetCount()
    {
		int reti = 0;
		foreach(EnemyInWave enemy in enemiesInWave) reti += enemy.count;
		return reti;
    }
}
public class EnemyInWave
{
	public GameObject enemy;
	public int count;
	public float rate;
	public EnemyInWave(GameObject enemy, int count, float rate)
	{
		this.enemy = enemy;
		this.count = count;
		this.rate = rate;
	}
}
