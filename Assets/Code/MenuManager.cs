using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private RawImage birdImage;
    [SerializeField] private TooltipTrigger upgradeToolTip1;
    [SerializeField] private TooltipTrigger upgradeToolTip2;
    [Header("BankMenu")]
    [SerializeField] private GameObject bankMenu;
    [SerializeField] private TextMeshProUGUI bankName;
    [SerializeField] private TextMeshProUGUI bankUpgradeCostDisplay;
    [SerializeField] private TextMeshProUGUI bankSellAmountDisplay;
    [SerializeField] private TextMeshProUGUI bankMoneyDisplay;
    [SerializeField] private TextMeshProUGUI bankCashOutDisplay;
    [SerializeField] private TextMeshProUGUI bankLevelDisplay;
    [SerializeField] private GameObject bankUpgradeButton1;
    [SerializeField] private GameObject bankUpgradeButton2;
    [Header("references")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winGameScreen;
    [SerializeField] private  Texture2D birdImages;
    [SerializeField] private string nextScene;
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
        turretLvL.text = "LVL " + (displayedTurret.GetCurrentLevel() + 1);
        SellAmountDisplay.text = "" + displayedTurret.GetSellAmount() + "$";
        upgradeCostDisplay.text = "" + displayedTurret.GetUpgradeAmount() + "$";
        killCountDisplay.text =  displayedTurret.GetKillCount() + " KILLS";
        birdImage.texture = displayedTurret.previewImage;
        if(displayedTurret.GetUpgradeAmount() > 0)
        {
            upgradeButton1.SetActive(true);
            upgradeToolTip1.header = displayedTurret.GetLevelStats(displayedTurret.GetCurrentLevel() + 1).name;
            upgradeToolTip1.content = displayedTurret.GetLevelStats(displayedTurret.GetCurrentLevel() + 1).description;
            if (displayedTurret.GetCurrentLevelStats().isSplittingPath)
            {
                upgradeButton2.SetActive(true);
                upgradeToolTip2.header = displayedTurret.GetLevelStats(displayedTurret.GetCurrentLevel() + 2).name;
                upgradeToolTip2.content = displayedTurret.GetLevelStats(displayedTurret.GetCurrentLevel() + 2).description;
            }
            else upgradeButton2.SetActive(false);

        }
        else
        {
            upgradeButton1.SetActive(false);
            upgradeButton2.SetActive(false);
        }
       
    }
    public void UpdateBankStatsInDisplay()
    {
        bankName.text = "Bird Bank";
        bankLevelDisplay.text = "LVL " + (displayedBank.GetCurrentLevel() + 1);
        bankSellAmountDisplay.text = displayedBank.GetSellAmount() + "$";
        bankUpgradeCostDisplay.text = displayedBank.GetCurrentStats().upgradeCost + "$";
        bankMoneyDisplay.text = displayedBank.GetGainedMoney() + "$";
        bankCashOutDisplay.text = displayedBank.GetCount() + "$";
        if (displayedBank.GetCurrentStats().upgradeCost > 0)
        {
            bankUpgradeButton1.SetActive(true);
            bankUpgradeButton2.SetActive(displayedBank.GetCurrentStats().isSplittingPath);
        }
        else
        {
            bankUpgradeButton1.SetActive(false);
            bankUpgradeButton2.SetActive(false);
        }

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
        if (displayedTurret != null)
        {
            displayedTurret.Upgrade();
            UpdateTurretStatsInDisplay();
        }
        else if (displayedBank != null)
        {
            displayedBank.Upgrade();
            UpdateBankStatsInDisplay();
        }
            
       
        AudioMaster.AM.PlayTurretClickSound();
    }
    public void OnSecondaryUpgradePressed()
    {
        if (displayedTurret != null)
        {
            displayedTurret.Upgrade();
            UpdateTurretStatsInDisplay();
        }
    }
    public void OnCashOutPressed()
    {
        displayedBank.CashOut();
        UpdateBankStatsInDisplay();
        AudioMaster.AM.PlayTurretClickSound();
    }
    public void OnSellPressed()
    {
        if(displayedTurret != null)
        {
            displayedTurret.Sell();
            HideTurretScreen();
        }else if(displayedBank != null)
        {
            displayedBank.Sell();
            HideTurretScreen();
        }
      
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
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        bankMenu.SetActive(false);
        Time.timeScale = 0;

        winGameScreen.SetActive(true);
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

    public void OnNext()
    {
        SceneManager.LoadScene(nextScene);

        Time.timeScale = 1;
    }

    public void OnFinish()
    {
        SceneManager.LoadScene("MainMenu");

        Time.timeScale = 0;
    }
}
