using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableCoin2 : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins2();
    }
}
