using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplyText;
    [SerializeField] private Wallet wallet;
    [SerializeField] private int startPrice = 200;
    private int multiply;
    private int price;

    private const string multiplyKey = "Multiply";

    private void Start(){
        multiply = PlayerPrefs.GetInt(multiplyKey);
        if(multiply == 0) multiply++;
        price = Mathf.RoundToInt(startPrice * multiply * 1.3f);
        UpdateUi();
    }

    public void MultiplyBuy() {
        if(wallet.Withdraw(price)) {
            multiply++;
            price = Mathf.RoundToInt(startPrice * multiply * 1.3f);
            Save();
            UpdateUi();
        }        
    }

    private void Save(){
        PlayerPrefs.SetInt(multiplyKey, multiply);
        PlayerPrefs.Save();
    }

    private void UpdateUi(){
        multiplyText.text = $"x{multiply}  Ціна: {price}";
    }
}
