using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    [SerializeField]
    private float startingTime = 500f;
    public TMP_Text timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timeText.text = string.Format("{0:00}:{1:00}",
            Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60));

        if (currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("Ending");
        }
    }
}
