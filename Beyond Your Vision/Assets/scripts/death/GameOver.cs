using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject screen;

    void Update()
    {
        if (screen.activeSelf && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Show()
    {
        screen.SetActive(true);
        Time.timeScale = 0f;
    }
}
