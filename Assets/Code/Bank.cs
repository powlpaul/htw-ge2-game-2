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
    }
    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.GetWaveState()) return;
        

        
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
    void OnMouseDown()
    {
        CashOut();
    }
    public void CashOut()
    {
        PlayerStats.Money += Mathf.RoundToInt(count);
        count = 0;

    }
}