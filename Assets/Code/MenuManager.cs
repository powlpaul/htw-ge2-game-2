using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private TextMeshProUGUI moneyDisplay;
    [SerializeField] private TextMeshProUGUI RoundDisplay;
    [SerializeField] private Button nextRoundButton;
    [Header("ShopMenu")]
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private TextMeshProUGUI[] birdPrices;
    [Header("TurretMenu")]
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private TextMeshProUGUI turretName;
    [SerializeField] private TextMeshProUGUI turretLvL;
    [SerializeField] private TextMeshProUGUI upgradeCostDisplay;
    [SerializeField] private TextMeshProUGUI SellAmountDisplay;
    [SerializeField] private TextMeshProUGUI killCountDisplay;
    [SerializeField] private GameObject upgradeButton1;
    [SerializeField] private GameObject upgradeButton2;
    [Header("BankMenu")]
    [SerializeField] private GameObject bankMenu;
    [SerializeField] private Text bankName;
    [SerializeField] private Text bankUpgradeCostDisplay;
    [SerializeField] private Text bankSellAmountDisplay;
    [SerializeField] private Text bankMoneyDisplay;
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
       //AudioMaster.AM.PlayTurretClickSound();
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
        AudioMaster.AM.PlayTurretClickSound();
    }
    public void ShowBankScreen(Bank bank)
    {
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        bankMenu.SetActive(true);

        displayedBank = bank;
        displayedTurret = null;
        UpdateBankStatsInDisplay();
        AudioMaster.AM.PlayTurretClickSound();


    }
    public void UpdateTurretStatsInDisplay()
    {
        turretName.text = displayedTurret.GetTitle();
        turretLvL.text = (displayedTurret.GetCurrentLevel() + 1) + " LVL ";
        SellAmountDisplay.text = "" + displayedTurret.GetSellAmount() + "$";
        upgradeCostDisplay.text = "" + displayedTurret.GetUpgradeAmount() + "$";
        killCountDisplay.text =  displayedTurret.GetKillCount() + "Kills";
        if(displayedTurret.GetUpgradeAmount() > 0)
        {
            upgradeButton1.SetActive(true);
            upgradeButton2.SetActive(displayedTurret.GetCurrentLevelStats().isSplittingPath);
        }
        else
        {
            upgradeButton1.SetActive(false);
            upgradeButton2.SetActive(false);
        }
       
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
        AudioMaster.AM.PlayTurretClickSound();

    }
    public void OnUpgradePressed()
    {
        displayedTurret.Upgrade();
        UpdateTurretStatsInDisplay();
        AudioMaster.AM.PlayTurretClickSound();
    }
    public void OnCashOutPressed()
    {
        displayedBank.CashOut();
        UpdateBankStatsInDisplay();
        AudioMaster.AM.PlayTurretClickSound();
    }
    public void EndGame()
    {
        //TODO show another screen which reads "you lost'
    }

    public void WinGame()
    {
        //TODO DISPLAY set the winGame hud as enabled;
    }
}
