using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text moneyDisplay;
    [SerializeField] private Text RoundDisplay;
    [SerializeField] private Button nextRoundButton;
    [Header("ShopMenu")]
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private Text[] birdPrices;
    [Header("TurretMenu")]
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private Text turretName;
    [SerializeField] private Text upgradeCostDisplay;
    [SerializeField] private Text SellAmountDisplay;
    [SerializeField] private Text killCountDisplay;
    [Header("BankMenu")]
    [SerializeField] private GameObject bankMenu;
    [SerializeField] private Text bankName;
    [SerializeField] private Text bankUpgradeCostDisplay;
    [SerializeField] private Text bankSellAmountDisplay;
    [SerializeField] private Text bankMoneyDisplay;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenu;


    private bool isPauseMenuActive = false;
    private int maxRounds;
    private Turret displayedTurret;
    private Bank displayedBank;
    // Start is called before the first frame update
    void Start()
    {
        BuildManager buildManager = GameObject.Find("GameMaster").GetComponent<BuildManager>();
        for (int i = 0; i < birdPrices.Length; i++)
        {
            birdPrices[i].text = buildManager.GetTurretCost(i) + "$";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.GetWaveState()) nextRoundButton.interactable = false;
        else nextRoundButton.interactable = true;

        if (Input.GetKeyUp(KeyCode.Escape) && !isPauseMenuActive)
        {
            buyMenu.SetActive(false);
            pauseMenu.SetActive(true);
            isPauseMenuActive = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && isPauseMenuActive)
        {
            pauseMenu.SetActive(false);
            isPauseMenuActive = false;
            Time.timeScale = 1;
            buyMenu.SetActive(true);
        }
    }

    public void Setup(int maxRounds)
    {
        this.maxRounds = maxRounds;
        RoundDisplay.text = $"1/{maxRounds}";
    }
    public void UpdateHealth(int newHealth)
    {
        healthDisplay.text = "" + newHealth;
    }
    public void UpdateMoney(int newMoney)
    {
        moneyDisplay.text = "" + newMoney;
    }
    public void NextRound(int roundNumber)
    {
        RoundDisplay.text = $"{roundNumber}/{maxRounds}";
    }

    public void ToggleGameSpeed()
    {
        if (Time.timeScale > 1.0) Time.timeScale = 1.0f;
        else Time.timeScale = 3.0f;
    }
    public void ShowTurretScreen(Turret tower)
    {
        buyMenu.SetActive(false);
        bankMenu.SetActive(false);
        upgradeMenu.SetActive(true);
     
        displayedTurret = tower;
        displayedBank = null;
        UpdateTurretStatsInDisplay();
    }
    public void ShowBankScreen(Bank bank)
    {
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        bankMenu.SetActive(true);

        displayedBank = bank;
        displayedTurret = null;
        UpdateBankStatsInDisplay();


    }
    public void UpdateTurretStatsInDisplay()
    {
        turretName.text = displayedTurret.GetTitle() + " LVL " + (displayedTurret.GetCurrentLevel() + 1);
        SellAmountDisplay.text = "" + displayedTurret.GetSellAmount() + "$";
        upgradeCostDisplay.text = "" + displayedTurret.GetUpgradeAmount() + "$";
        killCountDisplay.text =  displayedTurret.GetKillCount() + "Kills";
    }
    public void UpdateBankStatsInDisplay()
    {
        bankName.text = "Bank" + "LVL 1";
        bankSellAmountDisplay.text = displayedBank.GetSellAmount() + "$";
        bankUpgradeCostDisplay.text = "1000$";
        bankMoneyDisplay.text = displayedBank.GetCount() + "$";


    }
    public void HideTurretScreen()
    {
        buyMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        bankMenu.SetActive(false);
    }
    public void OnUpgradePressed()
    {
        displayedTurret.Upgrade();
        UpdateTurretStatsInDisplay();
    }
    public void OnCashOutPressed()
    {
        displayedBank.CashOut();
    }
    public void EndGame()
    {
        //TODO show another screen which reads "you lost'
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        bankMenu.SetActive(false);
        Time.timeScale = 0;

        gameOverScreen.SetActive(true);
    }

    public void OnGameOverReset()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeScene));

        Time.timeScale = 1;
    }

    public void WinGame()
    {
        //TODO DISPLAY set the winGame hud as enabled;
    }

    public void OnResume()
    {
        pauseMenu.SetActive(false);
        isPauseMenuActive = false;
        Time.timeScale = 1;
        buyMenu.SetActive(true);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("MainMenu");

        Time.timeScale = 0;
    }
}
