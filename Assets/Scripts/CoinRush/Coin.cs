using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private int multiply = 1;
    
    private CoinRushController controller;
    private Wallet wallet;

    private void Start() {
        wallet = Object.FindFirstObjectByType<Wallet>();
        controller = Object.FindFirstObjectByType<CoinRushController>();
        multiply = PlayerPrefs.GetInt("Multiply");
        if(multiply == 0) multiply++;
    }

    private void OnCollisionEnter(Collision c) {
        if(c.gameObject.tag == "Player"){
            wallet.Add(count * multiply);
            controller.AddCoin(count);
            Destroy(gameObject);
        }
    }
}