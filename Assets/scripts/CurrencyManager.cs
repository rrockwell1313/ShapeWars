using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currency1 = 100;
    public int currency2 = 100;
    public TextMeshProUGUI currency1Text;
    public TextMeshProUGUI currency2Text;


    private void Start()
    {
        UpdateCurrencyText();
    }

    public void ChangeCurrency1(int amount)
    {
        currency1 += amount;
        UpdateCurrencyText();
    }

    public void ChangeCurrency2(int amount)
    {
        currency2 += amount;
        UpdateCurrencyText();
    }

    public void UpdateCurrencyText()
    {
        currency1Text.text = "Coins: " + currency1;
        currency2Text.text = "Coins: " + currency2;

    }
}

