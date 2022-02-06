using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool GameIsOver;
	[SerializeField] private MenuManager menuManager;
	void Start ()
	{
		GameIsOver = false;
	}

	// Update is called once per frame
	void Update () {
		if (GameIsOver)
			return;

		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame ()
	{
		menuManager.EndGame();
		GameIsOver = true;
		
		//gameOverUI.SetActive(true);
	}

	public void WinLevel ()
	{
		menuManager.WinGame();
		GameIsOver = true;
		//completeLevelUI.SetActive(true);
	}

}
