using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewspaperTransition : MonoBehaviour
{
    [SerializeField] private GameObject news;
    [SerializeField] private GameObject text;
    async void Start()
    {
        text.SetActive(false);
        news.SetActive(true);
        await Task.Delay(5000);
        text.SetActive(true);
        news.SetActive(false);
        await Task.Delay(2000);
        SceneManager.LoadScene("MainScene");
    }
}
