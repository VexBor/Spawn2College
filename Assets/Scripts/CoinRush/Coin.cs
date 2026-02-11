using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int count;
    
    private CoinRushController controller;
    private Wallet wallet;

    private void Start() {
        wallet = Object.FindFirstObjectByType<Wallet>();
        controller = Object.FindFirstObjectByType<CoinRushController>();
    }

    private void OnCollisionEnter(Collision c) {
        if(c.gameObject.tag == "Player"){
            wallet.Add(count);
            controller.AddCoin(count);
            Destroy(gameObject);
        }
    }
}
