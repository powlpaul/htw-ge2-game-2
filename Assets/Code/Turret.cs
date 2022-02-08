using UnityEngine;
using System.Collections;
using Microsoft.VisualBasic;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;

	[SerializeField] private Animator animator;

	[Header("General")]
	[SerializeField] string title;
	public float range = 15f;
	[SerializeField] TurretStats[] upgradePath;
	private int currentLevel = 0;
	[Header("Use Bullets (default)")]
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float fireRate = 1f;
	[SerializeField] private int damage = 50;
	private float fireCountdown = 0f;
	
	private int killCount = 0;

	public LineRenderer lineRenderer;
	//public ParticleSystem impactEffect;
	//public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;
	public Texture2D previewImage;
    // Use this for initialization
    void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
		this.range = upgradePath[currentLevel].range;
		this.fireRate = upgradePath[currentLevel].attackSpeed;
		this.damage = upgradePath[currentLevel].damage;
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			target = null;
			animator.SetBool("isAttacking", false);
		}

	}
	void DamageAllEnemies()
    {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		int counter = 0;
		foreach (GameObject enemy in enemies)
        {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy <= range)
			{
				enemy.GetComponent<Enemy>().TakeDamage(50);
				counter++;
			}
		}
		Debug.Log(counter);
	}
	// Update is called once per frame
	void Update () {
	
		if (target == null)
		{
			return;
		}

		LockOnTarget();

	 
		if (fireCountdown <= 0f)
		{
			animator.SetBool("isAttacking", true);
			if (title == "Storm Bird")
			{
				DamageAllEnemies();
            }
            else
            {
				Shoot();
			}
			
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
		

	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		bullet.SetParent(this);
		if (bullet != null)
        {
			bullet.damage = this.damage;
			if (title == "Berserk Bird") 
			{ 
				bullet.damage += killCount;
				Debug.Log(bullet.damage);
			}
		}
		
		
		if (title == "Boomerang Bird" || title == "Archwizard" || title == "Berserk Bird" || title == "Ninja Bird") bullet.Seek(target, upgradePath[currentLevel].bounceAmount);
		else
		{
			bullet.Seek(target);
			AudioMaster.AM.PlayShotSoundEffect();
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	void OnMouseDown()
    {
		GameObject.Find("MenuCanvas").GetComponent<MenuManager>().ShowTurretScreen(this);
    }
	public void Upgrade()
    {
		if (PlayerStats.Money - this.upgradePath[currentLevel].upgradeCost < 0 || this.upgradePath[currentLevel].upgradeCost == 0) return;
		PlayerStats.Money -= this.upgradePath[currentLevel].upgradeCost;
		currentLevel++;
		this.range = upgradePath[currentLevel].range;
		this.fireRate = upgradePath[currentLevel].attackSpeed;
		this.damage = upgradePath[currentLevel].damage;
		if (!string.IsNullOrEmpty(upgradePath[currentLevel].name)) this.title = upgradePath[currentLevel].name;

	}
	public void UpgradeToSecondPath()
    {
		if (PlayerStats.Money - this.upgradePath[currentLevel].upgradeCost < 0 || this.upgradePath[currentLevel].upgradeCost == 0) return;
		PlayerStats.Money -= this.upgradePath[currentLevel].upgradeCost;
		currentLevel +=2;
		this.range = upgradePath[currentLevel].range;
		this.fireRate = upgradePath[currentLevel].attackSpeed;
		this.damage = upgradePath[currentLevel].damage;
		if (!string.IsNullOrEmpty(upgradePath[currentLevel].name)) this.title = upgradePath[currentLevel].name;

	}
	public void Sell()
    {
		PlayerStats.Money += GetSellAmount();
		Destroy();
    }
	public string GetTitle()
    {
		return title;
    }
	public int GetSellAmount()
    {
		int sellAmount = 600 / 2;
		for(int i = 0; i < currentLevel; i++)
        {
			sellAmount += upgradePath[i].upgradeCost / 2;
        }
		return sellAmount;
    }
	public int GetUpgradeAmount()
    {
		return this.upgradePath[currentLevel].upgradeCost;
	}
	public int GetCurrentLevel()
    {
		return currentLevel;
    }


    public void Destroy()
    {
		Destroy(gameObject);
    }
	public void IncrementKillCount()
    {
		killCount++;
    }

	public int GetKillCount()
    {
		return killCount;
    }
	public TurretStats GetCurrentLevelStats()
    {
		return upgradePath[currentLevel];
    }
	public TurretStats GetLevelStats(int index)
	{
		return upgradePath[index];
	}
}
