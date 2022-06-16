using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI nameInputTextField;
    public Button startButton;
    public string playerName;
    public string bestName;
    public int bestScore;

    public static MenuUIHandler menuInstance;

    private void Awake()
    {
        if (menuInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        menuInstance = this;
        DontDestroyOnLoad(gameObject);
        LoadHiScore();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
    
    public void CheckName(string input)
    {
        if (input == "")
        {
            nameInputTextField.text = "anon";
        }
        playerName = input;
        Debug.Log("The player's name is '" + playerName + "'.");

    }

    public void StartGame()
    {
                
            canvas.enabled = false;
            SceneManager.LoadScene(1);
        
        
    }

    public void ExitGame()
    {
        SaveHiScore();
#if UNITY_EDITOR //instructions for compiler!
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string bestName;
        public int bestScore;
    }
    public void SaveHiScore()
    {
        SaveData data = new SaveData();
        data.bestName = bestName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHiScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestName = data.bestName;
            bestScore = data.bestScore;
        }
    }



}
