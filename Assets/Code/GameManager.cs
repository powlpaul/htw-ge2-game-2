using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool GameIsOver;
	[SerializeField] private MenuManager menuManager;
	[SerializeField] private AudioMaster audioMaster;

	private AudioSource[] allAudioSources;

	void Awake()
	{
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	}

	void Start ()
	{
		GameIsOver = false;
		StopAllAudio();
		audioMaster.PlayBackgroundTrack();
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

	void StopAllAudio()
	{
		foreach (AudioSource audioS in allAudioSources)
		{
			audioS.Stop();
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
		PlayerStats.FinalizeScore();
		menuManager.WinGame();
		GameIsOver = true;
		//completeLevelUI.SetActive(true);
	}

}
