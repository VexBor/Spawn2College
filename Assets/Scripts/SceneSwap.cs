using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public void LoadScene(int id) {
        SceneManager.LoadScene(id);
    }   
}
