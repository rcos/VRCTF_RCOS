using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HomePageManager : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public GameObject scenario1Button;
    public GameObject scenario2Button;
    public GameObject scenario3Button;
    public GameObject proceedButton;
    public GameObject backButton;

    private string defaultText = "Welcome to VR RCC Project!\n\nChoose a scenario:";
    private string scenario1Instructions = "Find a way to log in to the computer.\n\nSee what you can do to objects in the room.";
    private string scenario2Instructions = "Scenario 2 instructions.";
    private string scenario3Instructions = "Scenario 3 instructions. ";

    private string selectedScenario = ""; // Track which scenario was clicked

    void Start()
    {
        ShowHome();
    }

    public void OnScenario1Clicked()
    {
        selectedScenario = "Scenario1";
        mainText.text = scenario1Instructions;
        ToggleScenarioButtons(false);
        ShowProceedAndBack(true);
    }

    public void OnScenario2Clicked()
    {
        selectedScenario = "Scenario2";
        mainText.text = scenario2Instructions;
        ToggleScenarioButtons(false);
        ShowProceedAndBack(true);
    }

    public void OnScenario3Clicked()
    {
        selectedScenario = "Scenario3";
        mainText.text = scenario3Instructions;
        ToggleScenarioButtons(false);
        ShowProceedAndBack(true);
    }

    public void OnBackClicked()
    {
        ShowHome();
    }

    public void OnProceedClicked()
    {
        if (!string.IsNullOrEmpty(selectedScenario))
        {
            SceneManager.LoadScene(selectedScenario);
        }
        else
        {
            Debug.LogWarning("No scenario selected!");
        }
    }

    private void ShowHome()
    {
        mainText.text = defaultText;
        ToggleScenarioButtons(true);
        ShowProceedAndBack(false);
        selectedScenario = "";
    }

    private void ToggleScenarioButtons(bool state)
    {
        scenario1Button.SetActive(state);
        scenario2Button.SetActive(state);
        scenario3Button.SetActive(state);
    }

    private void ShowProceedAndBack(bool state)
    {
        proceedButton.SetActive(state);
        backButton.SetActive(state);
    }
}
