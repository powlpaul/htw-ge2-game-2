using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int maxMoney = 0;
    [SerializeField] private int moneyPerTick = 0;
    [SerializeField] private int interestPercent = 0;
    private int gainedMoney =0;
    [SerializeField] private BankStats[] upgradePath;
    [SerializeField] private GameObject moneyGraphic;
    private int currentLevel = 0;
    private float count;
    void Start()
    {
        Initialize();
        Debug.Log(CalcMoney(2));
    }
    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.GetWaveState()) return;
        

        
    }
    private float CalcMoney(int depth)
    {
        if (depth == 1) return moneyPerTick;
        else return moneyPerTick + CalcMoney(depth - 1) * 1.1f;
    }
    public void OnMouseDown()
    {
        GameObject.Find("MenuCanvas").GetComponent<MenuManager>().ShowBankScreen(this);
    }
    public void Initialize()
    {
        count = 0;
    }
    public void TickUp()
    {
        moneyGraphic.SetActive(true);
        Debug.Log(count);
        count = Mathf.Min(count + moneyPerTick + count * interestPercent / 100, maxMoney);
        Debug.Log(count);
    }
    public int GetSellAmount()
    {
        return 400 + (int) count;
    }
    public int GetCount()
    {
        return (int)count;
    }
    public  BankStats GetCurrentStats()
    {
        return upgradePath[currentLevel];
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public int GetGainedMoney()
    {
        return gainedMoney;
    }
    public void Sell()
    {
        PlayerStats.Money += 400 + (int)count;
        Destroy(gameObject);
    }
    public void CashOut()
    {
        moneyGraphic.SetActive(false);
        PlayerStats.Money += Mathf.RoundToInt(count);
        gainedMoney += Mathf.RoundToInt(count);
        count = 0;

    }
    public void Upgrade()
    {
        if (PlayerStats.Money - this.upgradePath[currentLevel].upgradeCost < 0 || this.upgradePath[currentLevel].upgradeCost == 0) return;
        PlayerStats.Money -= this.upgradePath[currentLevel].upgradeCost;
        currentLevel++;
        this.maxMoney = upgradePath[currentLevel].maxMoney;
        this.moneyPerTick = upgradePath[currentLevel].moneyPerTick;
        this.interestPercent = upgradePath[currentLevel].interestPercent;

    }
    public void Upgrade2()
    {
        if (PlayerStats.Money - this.upgradePath[currentLevel].upgradeCost < 0 || this.upgradePath[currentLevel].upgradeCost == 0) return;
        PlayerStats.Money -= this.upgradePath[currentLevel].upgradeCost;
        currentLevel += 2;
        this.maxMoney = upgradePath[currentLevel].maxMoney;
        this.moneyPerTick = upgradePath[currentLevel].moneyPerTick;
        this.interestPercent = upgradePath[currentLevel].interestPercent;

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
