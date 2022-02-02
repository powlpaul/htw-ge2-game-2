using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[SerializeField] GameObject EnemyAfterDeath;
	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;

	public int worth = 50;

	//public GameObject deathEffect;
	private float slowTimer = 0;
	private SlowZone currentSlowZone;
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
		if (currentSlowZone ==null ||  slowTimer > currentSlowZone.slowDuration) speed = startSpeed;
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
		SlowZone newSlowZone = other.GetComponent<SlowZone>();
		if (this.currentSlowZone == null ||newSlowZone.slowAmount < currentSlowZone.slowAmount) currentSlowZone = newSlowZone;
		speed = startSpeed  * currentSlowZone.slowAmount;
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

		if (EnemyAfterDeath == null) WaveSpawner.EnemiesAlive--;
		else
		{
			GameObject newEnemy = GameObject.Instantiate(EnemyAfterDeath, transform.position, Quaternion.identity);
			EnemyMovement thisMovement = this.gameObject.GetComponent<EnemyMovement>();
			EnemyMovement newEnemyMovement = newEnemy.GetComponent<EnemyMovement>();
			newEnemyMovement.SetTarget(thisMovement.GetTarget());
		}

		Destroy(gameObject);
	}
	public bool getIsDead()
    {
		return this.isDead;
    }
}
