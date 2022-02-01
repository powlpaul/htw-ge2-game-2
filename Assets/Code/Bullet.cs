using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private GameObject slowZone;
	private Turret parent;
	public float speed = 70f;

	public int damage = 50;

	[SerializeField] private int bounceAmount = 0;
	public float explosionRadius = 0f;
	//public GameObject impactEffect;
	
	public void Seek (Transform _target)
	{
		target = _target;
		bounceAmount = 0;
	}
	public void Seek (Transform _target, int bounceAmount)
    {
		target = _target;
		this.bounceAmount = bounceAmount;
    }
	// Update is called once per frame
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			UpdateTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}
	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		float distanceToCurrentTarget = Vector3.Distance(transform.position, target.position);
		
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance && distanceToEnemy > distanceToCurrentTarget + 0.25f)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= 10)
		{

			target = nearestEnemy.transform;
			//Debug.LogError(target.position.x + " " + target.position.y);
			bounceAmount--;
			//targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}

	}
	void HitTarget ()
	{
		/*GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);*/

		if (explosionRadius > 0f)
		{
			Explode();
		} else
		{
			Damage(target);
		}
		if (slowZone != null) { 
			GameObject slowZoneGO = Instantiate(slowZone, transform.position, Quaternion.identity);
			SlowZone slowZoneScript = slowZoneGO.GetComponent<SlowZone>();
			//slowZoneScript.slowDuration = parent.
		}
		if(bounceAmount == 0) Destroy(gameObject);
	}

	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null)
		{
			e.TakeDamage(damage);
			if (e.getIsDead()) parent.IncrementKillCount();
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

	public void SetParent(Turret parent)
    {
		this.parent = parent;
    }
}
