using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
    public Vector3 positionOffset;


	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
    }

	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}
	
    private void OnMouseEnter()
    {
		if (buildManager.GetTurretToBuild() == null || turret != null) return;
		GameObject _turret = (GameObject)Instantiate(buildManager.GetTurretToBuild().prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		if (turret.tag == "Bank")
		{
			turret.GetComponent<Bank>().enabled = false;
			turret.GetComponent<BoxCollider>().enabled = false;

		} else {
			turret.GetComponent<Turret>().enabled = false;
			turret.GetComponent<CapsuleCollider>().enabled = false;
		}
	}
    private void OnMouseExit()
    {

		if ((turret == null) || ((turret != null && turret.tag == "Bank" && turret.GetComponent<Bank>().enabled == true)) || (turret != null && turret.tag == "Tower" && turret.GetComponent<Turret>().enabled == true)) return;
		if(turret.tag == "Bank") turret.GetComponent<Bank>().Destroy();
		else turret.GetComponent<Turret>().Destroy();
		turret = null;
    }
	
    void OnMouseDown ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		// && turret.GetComponent<Turret>().isActiveAndEnabled == true
		if (!buildManager.CanBuild)
			return;
		if (turret != null && ((turret.tag == "Turret" && turret.GetComponent<Turret>().enabled) || (turret.tag == "Bank" && turret.GetComponent<Bank>().enabled )))
		{
			buildManager.SelectNode(this);
			return;
		}
		BuildTurret(buildManager.GetTurretToBuild());
	}

	void BuildTurret (TurretBlueprint blueprint)
	{
	
		
		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}
		if(turret.tag == "Tower") turret.GetComponent<Turret>().Destroy();
		if (turret.tag == "Bank") turret.GetComponent<Bank>().Destroy();
		PlayerStats.Money -= blueprint.cost;
		/*
		if (turret.GetComponent<Turret>().enabled == false)
        {
			turret.GetComponent<Turret>().enabled = true;
			turret.GetComponent<CapsuleCollider>().enabled = true;
			//Debug.LogError("turret");
			return;

		}
		*/
		GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;

		//GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		//Destroy(effect, 5f);

		Debug.Log("Turret build!");
	}



	/*void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		} else
		{
			rend.material.color = notEnoughMoneyColor;
		}

	}*/

	/*void OnMouseExit ()
	{
		rend.material.color = startColor;
    }*/

}
