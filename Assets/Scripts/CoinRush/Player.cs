using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CoinRushController controller;

    void OnCollisionEnter(Collision c) {
        if(c.gameObject.tag == "Obstacle") {
            controller.EndGame();
        }
    }
}
