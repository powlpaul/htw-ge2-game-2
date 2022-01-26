using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;

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

	[Header("Use Laser")]
	[SerializeField] private bool useLaser = false;

	[SerializeField] private int damageOverTime = 30;
	[SerializeField] private float slowAmount = .5f;

	public LineRenderer lineRenderer;
	//public ParticleSystem impactEffect;
	//public Light impactLight;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

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
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					//impactEffect.Stop();
					//impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();
		} else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}

	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser ()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			//impactEffect.Play();
			//impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		//impactEffect.transform.position = target.position + dir.normalized;

		//impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.damage = this.damage;
			bullet.Seek(target);
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
		currentLevel++;
		PlayerStats.Money -= this.upgradePath[currentLevel].upgradeCost;
		this.range = upgradePath[currentLevel].range;
		this.fireRate = upgradePath[currentLevel].attackSpeed;
		this.damage = upgradePath[currentLevel].damage;

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
}
