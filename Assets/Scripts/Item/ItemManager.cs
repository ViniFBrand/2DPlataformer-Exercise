using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Sigleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;

    protected override void Awake()
    {
        base.Awake();
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value +=amount;
    }

    /*private void UpdateUI()
    {
       UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }*/
}
