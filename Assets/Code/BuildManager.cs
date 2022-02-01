using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}
    private void Update()
    {
		if (Input.GetMouseButtonDown(1))
        {
			turretToBuild = null;
        }

	}
    /*public GameObject buildEffect;
	public GameObject sellEffect;*/

    private TurretBlueprint turretToBuild = null;
	[SerializeField] private TurretBlueprint[] allTurrets;
	private Node selectedNode;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public void SelectNode(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		turretToBuild = null;
	}

	public void DeselectNode()
	{
		selectedNode = null;
	}

	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		turretToBuild = turret;
		DeselectNode();
	}
	public void ChangeTurretOnClick(int index)
    {
		turretToBuild = allTurrets[index];
    }
	public int GetTurretCost(int index)
    {
		return allTurrets[index].cost;
    }

	public void Test()
    {
		Debug.Log("Test");
    }
	public TurretBlueprint GetTurretToBuild ()
	{
		return turretToBuild;
	}

}
