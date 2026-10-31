using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
  async void Start()
    {
        await Task.Delay(5400);
        SceneManager.LoadScene("MainScene");
    }
}
