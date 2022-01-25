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

    private float timePassed;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.GetWaveState()) return;
        

        
    }
}
