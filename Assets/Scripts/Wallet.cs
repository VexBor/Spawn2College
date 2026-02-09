using UnityEngine;
using UnityEngine.Events;
 public class Wallet : MonoBehaviour
{
    [SerializeField]private int balance = 0;
    private const string BalanceKey = "Balance";

    public UnityEvent<int> OnBalanceChanged;

    void Start() {
        balance = PlayerPrefs.GetInt(BalanceKey);
        OnBalanceChanged?.Invoke(balance);
    }

    public bool Withdraw(int count) {
        if(balance - count < 0){
            return false;
        }
        else {
            balance -= count;
          OnBalanceChanged?.Invoke(balance);
            Save();
            return true;
        }
    }

    public void Add(int count){
        balance += count;
        OnBalanceChanged?.Invoke(balance);
        Save();
    }

    private void Save() {
        PlayerPrefs.SetInt(BalanceKey, balance);
        PlayerPrefs.Save();
    }

}
