using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;

	public int worth = 50;

	//public GameObject deathEffect;
	private float slowTimer = 0;

	[Header("Unity Stuff")]
	public Image healthBar;

	private bool isDead = false;

	void Start ()
	{
		speed = startSpeed;
		health = startHealth;
	}
    private void Update()
    {
		slowTimer += Time.deltaTime;
		if (slowTimer > 1) speed = startSpeed;
    }
    public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}
	public void OnTriggerEnter(Collider other)
	{
		if (other.tag != "SlowZone") return;
		speed = startSpeed / 2;
	}
    public void OnTriggerStay(Collider other)
    {
		slowTimer = 0;
    }
    public void OnTriggerExit(Collider other)
    {
		slowTimer = 0;
    }
    public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}

	void Die ()
	{
		isDead = true;

		PlayerStats.Money += worth;

		/*GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);*/

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}

}
