using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Sigleton;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;

    protected override void Awake()
    {
        base.Awake();
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins +=amount;
    }
}
