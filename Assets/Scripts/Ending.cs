using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EndGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void RetryGame()
    {
        SceneManager.LoadScene("Main");
    }
}
