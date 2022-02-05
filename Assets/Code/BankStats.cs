[System.Serializable]
/*
 * This class is mainly a value holder. hence it does not inherint from MonoBehaviour .
 * all stats are serializalbe in the editor and are used in an array to represent the upgrade path of a given turret
 * 
 */

public class BankStats 
{
    int maxMoney = 0;
    int moneyPerTick = 0;
    int interestPercent = 0;
    int upgradeCost =0;
}
