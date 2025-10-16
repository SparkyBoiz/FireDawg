using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f;
    public string sceneToLoad = "GameOver";

    [Header("UI")]
    public TMP_Text timerText;

    private float currentTime;
    private bool isRunning = false;

    private void Start()
    {
        if (timerText == null)
        {
            return;
        }

        currentTime = startTime;
        isRunning = true;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateTimerUI();
            TimerEnded();
        }
        else
        {
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {

        }
    }
}