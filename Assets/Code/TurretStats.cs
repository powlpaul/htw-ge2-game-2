using UnityEngine;

[System.Serializable]
public class TurretStats
{

	public float range;
	public int damage;
	public float attackSpeed;
	public float slowAmount;
	public float slowTime;
	public int bounceAmount;
	public int upgradeCost;
	public TurretStats(int range, int damage, int attackSpeed, int upgradeCost)
    {
		this.range = range;
		this.damage = damage;
		this.attackSpeed = attackSpeed; 
		this.upgradeCost = upgradeCost;
    }

}
