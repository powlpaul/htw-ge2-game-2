using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int maxMoney = 0;
    [SerializeField] private int moneyPerTick = 0;
    [SerializeField] private int interestPercent = 0;
    [SerializeField] private float tickTime = 0;

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
    public void Sell()
    {
        PlayerStats.Money += 400 + (int)count;
    }
    public void CashOut()
    {
        PlayerStats.Money += Mathf.RoundToInt(count);
        count = 0;

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
