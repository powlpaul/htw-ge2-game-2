using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	public int cost;

	public GameObject upgradedPrefab;
	public int upgradeCost;

	public void UpgradeTurret()
    {
		prefab.GetComponent<Turret>().Upgrade();
    }
	public int GetSellAmount ()
	{
		return cost / 2;
	}

}
