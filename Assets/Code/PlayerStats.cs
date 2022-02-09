using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	[SerializeField] private int startMoney = 400;
	public static int score = 0;
	public static int Lives;
	[SerializeField] int startLives = 20;
	public static MenuManager menu;
	public static int Rounds;

	void Start ()
	{
		menu = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();
		menu.UpdateHealth(startLives);
		menu.UpdateMoney(startMoney);
		Money = startMoney;
		Lives = startLives;
		Rounds = 0;
	}
    private void Update()
    {
		menu.UpdateMoney(Money);
		menu.UpdateHealth(Lives);
		menu.NextRound(Rounds);
    }

	public static void FinalizeScore()
    {
		Debug.Log(score);
		score += Lives * 10;
		score += Money * 5;
		menu.ShowFinalScore(score);
		Debug.Log(score);
    }

}
