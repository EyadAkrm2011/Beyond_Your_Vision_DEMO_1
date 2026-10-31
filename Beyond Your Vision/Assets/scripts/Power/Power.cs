using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Power : MonoBehaviour
{
    public float power;
    public float consume;
    public TextMeshProUGUI powerText;

    void Start()
    {
        power = 100f;
        consume = 1f;
        StartCoroutine(powerconsume());
    }

    IEnumerator powerconsume()
    {
        while (power > 0)
        {
            if (powerText != null)
            {
                powerText.text = power.ToString();
            }

            yield return new WaitForSeconds(9.6f);
            power = power - consume;
        }
        power = 0;
        if (powerText != null)
        {
            powerText.text = power.ToString();
        }

        GameOver();
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}