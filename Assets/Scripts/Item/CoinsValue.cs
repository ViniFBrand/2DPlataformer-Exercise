using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsValue : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    private void Update()
    {
        ShowText();
    }

    public void ShowText()
    {
        textElement.text = "x" + ItemManager.Instance.coins;
    }
}
