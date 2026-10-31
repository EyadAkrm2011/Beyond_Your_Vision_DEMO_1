using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    
    public void OnClickEnterButton()
    {
        SceneManager.LoadScene("TransitionScene");
    }

  
}
