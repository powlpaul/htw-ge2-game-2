using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	[SerializeField] private int startMoney = 400;

	public static int Lives;
	[SerializeField] int startLives = 20;

	public static int Rounds;

	void Start ()
	{
		MenuManager menu = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();
		menu.UpdateHealth(startLives);
		menu.UpdateMoney(startMoney);
		Money = startMoney;
		Lives = startLives;
		Rounds = 0;
	}

}
