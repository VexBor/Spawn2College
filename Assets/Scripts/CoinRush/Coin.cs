using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int count;
    private Wallet wallet;

    private void Start() {
        wallet = Object.FindFirstObjectByType<Wallet>();
    }

    private void OnCollisionEnter(Collision c) {
        if(c.gameObject.tag == "Player"){
            wallet.Add(count);
            Destroy(gameObject);
        }
    }
}
