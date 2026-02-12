using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container; 
    [SerializeField] private PathFinder PathFinder;

    void Start()
    {
        GenerateMenu();
    }

    void GenerateMenu()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject targetObj in targets)
        {
            GameObject newButton = Instantiate(buttonPrefab, container);
            
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = targetObj.name;

            Button btn = newButton.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                PathFinder.SetTarget(targetObj.transform);
                transform.gameObject.SetActive(false);
            });
        }
    }
}