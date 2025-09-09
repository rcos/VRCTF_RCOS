using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void OnPointerClick()
    {  
        EmailManager.Instance.BackToList();
    }
    
    public void OnPointerEnter()
    {
    }
}
