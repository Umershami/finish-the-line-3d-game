using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int currentLevel = 1;
    public float timeLimit = 20f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void LevelCompleted()
    {
        currentLevel++;
        StartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void ResetGame()
    {
        currentLevel = 1;
        StartLevel();
    }
}
