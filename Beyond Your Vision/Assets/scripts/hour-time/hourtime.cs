using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hourtime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject black;
    [SerializeField] private float time1;

    private int time2 = 12;
    private float timer = 0f;
    private bool isGameOver = false;

    void Start()
    {
        if (black != null)
        {
            black.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= time1)
        {
            timer = 0f;
            time2 = (time2 == 12) ? 1 : time2 + 1;

            if (time2 == 6)
            {
                TriggerSixAMAsync();
            }
        }

        if (timeText != null && !isGameOver)
        {
            timeText.text = time2 + "am";
        }
    }

    void TriggerSixAMAsync()
    {
        isGameOver = true;

        if (timeText != null)
        {
            timeText.text = "6 AM";
        }

        if (black != null)
        {
            black.SetActive(true);
        }

        Debug.Log("6 AM");
        Invoke("LoadUI", 10f);
    }

    void LoadUI()
    {
        SceneManager.LoadScene("UI");
    }
}