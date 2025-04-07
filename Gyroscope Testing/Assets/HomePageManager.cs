using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HomePageManager : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public GameObject scenario1Button;
    public GameObject proceedButton;
    public GameObject backButton;

    private string defaultText = "Welcome to VR RCC Project!\n\nChoose a scenario:";
    private string scenario1Instructions = "Find a way to log in to the computer.\n\nSee what you can do to objects in the room.";

    void Start()
    {
        ShowHome();
    }

    public void OnScenario1Clicked()
    {
        mainText.text = scenario1Instructions;
        scenario1Button.SetActive(false);
        proceedButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void OnBackClicked()
    {
        ShowHome();
    }

    public void OnProceedClicked()
    {
        SceneManager.LoadScene("Scenario1"); // replace with your actual scene name
    }

    private void ShowHome()
    {
        mainText.text = defaultText;
        scenario1Button.SetActive(true);
        proceedButton.SetActive(false);
        backButton.SetActive(false);
    }
}
