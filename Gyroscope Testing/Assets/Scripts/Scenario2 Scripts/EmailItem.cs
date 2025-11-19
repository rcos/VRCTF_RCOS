using UnityEngine;
using TMPro;

public class EmailItem : MonoBehaviour
{
    public TMP_Text LabelText;
    public TMP_Text SubjectText;
    public string FullContent;

    public string LabelName; 
    public string SubjectLine;
    
    public void OnPointerClick()
    {        
        EmailManager.Instance.ShowEmailDetail(this);

        if (LabelText.text == "RPI" && SubjectText.text == "CTF Code"){
            GameObject scenarioManagerObj = GameObject.Find("ScenarioManagerTemp");
            if (scenarioManagerObj != null)
            {
                // Get ScenarioManager component
                ScenarioManager scenarioManager = scenarioManagerObj.GetComponent<ScenarioManager>();
                if (scenarioManager != null)
                {
                    // Call FlagTriggered
                    scenarioManager.FlagTriggered();
                    Debug.Log("FlagTriggered called successfully!");
                }
            }
        }
    }
    
    public void OnPointerEnter()
    {
        
    }
}

