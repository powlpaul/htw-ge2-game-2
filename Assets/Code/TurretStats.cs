using UnityEngine;

[System.Serializable]
/*
 * This class is mainly a value holder. hence it does not inherint from MonoBehaviour .
 * all stats are serializalbe in the editor and are used in an array to represent the upgrade path of a given turret
 * 
 */
public class TurretStats
{

	public float range;
	public int damage;
	public float attackSpeed;
	public float slowAmount;
	public float slowTime;
	public float slowZoneDuration;
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
