using UnityEngine;

public class KickScene : MonoBehaviour
{
    
 public void Closing()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
