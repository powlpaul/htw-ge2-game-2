using UnityEngine;

[System.Serializable]
/*
 * This class is mainly a value holder. hence it does not inherint from MonoBehaviour .
 * all stats are serializalbe in the editor and are used in an array to represent the upgrade path of a given turret
 * 
 */

public class BankStats 
{
    public int maxMoney = 0;
    public int moneyPerTick = 0;
    public int interestPercent = 0;
    public int upgradeCost =0;
    public bool isSplittingPath;
    public string title;
    [TextArea]
    public string description;
}
