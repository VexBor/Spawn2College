using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinRushController : MonoBehaviour
{
    [Header("Object/Script")]
    [SerializeField] private LevelSystem level;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private TextMeshProUGUI xpTxt;
    [SerializeField] private TextMeshProUGUI coinTxt;

    [Header("Settings")]
    [SerializeField] private int xpOnSecond;

    private float time;
    private int coin;

    private void Update() {
        time += Time.deltaTime;
    } 

    public void  AddCoin(int count){
        coin += count;
    } 

    public void EndGame() {
        Time.timeScale = 0f;

        endGameMenu.SetActive(true);

        int xp = Mathf.RoundToInt(time * xpOnSecond);
        level.AddXp(xp);

        xpTxt.text = $"Досвіду зароблено: {xp}";
        coinTxt.text = $"Монет зароблено: {coin}";
    }

    public void OpenMenu(){
        endGameMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart() {
        endGameMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
