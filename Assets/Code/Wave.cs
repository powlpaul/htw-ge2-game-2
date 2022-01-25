using UnityEngine;

[System.Serializable]
public class Wave {

	public GameObject enemy;
	public int count;
	public float rate;

	public Wave(GameObject enemy, int count, float rate)
    {
		this.enemy = enemy;
		this.count = count;
		this.rate = rate;
    }
 
}
