using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;

	[SerializeField] GameObject EnemyAfterDeath;
	[HideInInspector]
	public float speed;

	public float startHealth = 100;
	private float health;
	private bool isFrozen = false;
	private bool isConfused = false;
	public int worth = 50;

	//public GameObject deathEffect;
	private float slowTimer = 0;
	private float confuseTimer;
	private SlowZone currentSlowZone;
	private float freezeTimer;
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
		freezeTimer += Time.deltaTime;
		confuseTimer += Time.deltaTime;
		if (confuseTimer > 2.5) Unconfuse();
		if (freezeTimer > 2.5) isFrozen = false;

		if (currentSlowZone ==null ||  slowTimer > currentSlowZone.slowDuration) speed = startSpeed;

    }
	public void Freeze()
    {
		//Debug.Log("I got frozen");
		isFrozen = true;
		freezeTimer = 0;

	}
	public void Confuse()
    {
		Debug.Log("I got confused");
		isConfused = true;
		confuseTimer = 0;
		gameObject.GetComponent<EnemyMovement>().GetPreviousWayPoint();
    }
	public void Unconfuse()
    {
		if (!isConfused) return;
		isConfused = false;
		gameObject.GetComponent<EnemyMovement>().GetNextWaypoint();
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
		AudioMaster.AM.PlayEnemyDeathSound();
		/*GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);*/
		PlayerStats.score += Mathf.RoundToInt(Waypoints.GetDistanceToEnd(transform, gameObject.GetComponent<EnemyMovement>().GetWayPointIndex()) * worth);
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
	public bool GetIsFrozen()
    {
		return isFrozen;
    }
	public bool GetIsConfused()
    {
		return isConfused;
    }
}
