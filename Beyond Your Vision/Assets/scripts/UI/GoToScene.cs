using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public bool Go;
    public void OnClickEnterButton()
    {
        Go = true;
        Debug.Log("Go is true");
    }

    private void Update()
    {
        if (Go == true)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
