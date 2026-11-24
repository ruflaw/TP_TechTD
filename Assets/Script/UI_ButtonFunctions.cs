using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ButtonFunctions : MonoBehaviour
{
    [SerializeField] private string MainSceneName = "Final_Scene";

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void StartMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
        Time.timeScale = 1.0f;
    }
}
