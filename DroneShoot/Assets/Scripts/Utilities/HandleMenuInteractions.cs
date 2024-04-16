using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleMenuInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreContent;
    public GameObject scoreTextPrefab;
    void Start()
    {
        string scoreFilePath  = Application.persistentDataPath + "/score.csv";
        if (File.Exists(scoreFilePath))
        {            
            string[] scoreData = File.ReadAllLines(scoreFilePath);
            foreach (string data in scoreData)
            {
                string[] iteration = data.Split(';');
                GameObject scoreText = Instantiate(scoreTextPrefab, scoreContent.transform);
                scoreText.gameObject.GetComponent<TMP_Text>().SetText($"{iteration[0]}: {iteration[1]}");
            }
        }
    }

    public void StartGame()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
