using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMain : MonoBehaviour
{
  async void Start()
    {
        await Task.Delay(5000);
        SceneManager.LoadScene("UI");
    }
}
