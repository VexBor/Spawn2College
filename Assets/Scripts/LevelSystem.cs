using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int xp;
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelTxt;

    private const string LevelKey = "Level";
    private const string XpKey = "Xp";

    private void Start() {
        level = PlayerPrefs.GetInt(LevelKey);
        xp = PlayerPrefs.GetInt(XpKey);
        UpdateUi();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            AddXp(20);
        }
    }

    public int NeedXP() {
        return Mathf.RoundToInt(100 * level * 1.3f);
    }

    public void AddXp(int count) {
        xp += count;

        if(xp >= NeedXP()) {
            xp -= NeedXP();
            level++;
        }
        UpdateUi();
        Save();
    }

    private void UpdateUi() {
        xpBar.value = (float)xp / NeedXP();
        levelTxt.text = $"Рівень {level}";
    }

    private void Save() {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.SetInt(XpKey, xp);
        PlayerPrefs.Save();        
    }
}