using UnityEngine;
using TMPro;

public class WalletUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceTxt;

    public void UpdateBalanceText(int newBalance) {
        balanceTxt.text = $"{newBalance}";
    }
}
