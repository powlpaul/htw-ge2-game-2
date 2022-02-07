using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		if (target != null) {
			transform.LookAt(target.position);
			return;
		}
		target = Waypoints.points[0];
		transform.LookAt(target.position);
	}

	void Update()
	{
		if (enemy.GetIsFrozen()) return;
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
			
			transform.LookAt(target.position);
		}

	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

    void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
	public void SetTarget(Transform newTarget)
    {
		target = newTarget;
		for(int i = 0; i < Waypoints.points.Length; i++)
        {
			if (Waypoints.points[i] == newTarget) wavepointIndex = i;
        }
    }

	public Transform GetTarget()
    {
		return target;
    }
}
