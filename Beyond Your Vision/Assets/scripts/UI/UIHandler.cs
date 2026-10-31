using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimationsOFButtons : MonoBehaviour
{
    // defining variables
    public GameObject MainUI;
    public GameObject LogoUI;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    private bool Awake;
    private bool isStarted = false;
    public AudioSource computeronsound;
    public GameObject ComputerCanva;
    async void Startcomputer()
    {
        computeronsound.Play();
        await Task.Delay(2513);
        one.SetActive(true);
        await Task.Delay(2210);
        two.SetActive(true);
        await Task.Delay(2005);
        three.SetActive(true);
        await Task.Delay(2040);
        four.SetActive(true);
        await Task.Delay(1523);
        one.SetActive(false); two.SetActive(false); three.SetActive(false); four.SetActive(false);
        
        await Task.Delay(1000);
        LogoUI.SetActive(true);
        await Task.Delay(2000);
        LogoUI.SetActive(false);
        await Task.Delay(1000);
        MainUI.SetActive(true);
    }
    public void AwakeComputer(InputAction.CallbackContext cntx)
    {
        if (cntx.started || cntx.performed)
        {
            Awake = true;
            ComputerCanva.SetActive(false);
            

        }
    }
    private void Update()
    {
        if (Awake == true && isStarted == false) {
            Startcomputer();
            isStarted= true;
        }
    }
}
