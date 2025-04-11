using UnityEngine;
using UnityEngine.EventSystems; // Required for UI interactions

public class KeyInput : MonoBehaviour
{
    private KeyboardManager KeyboardManager;
    private Renderer Rend; 
    
    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    private void Start()
    {
        KeyboardManager = FindObjectOfType<KeyboardManager>();
        Rend = GetComponent<Renderer>();

        if (Rend == null)
        {
            Debug.LogError($"Renderer missing on {gameObject.name}. KeyInput script requires a Renderer component.");
        }
    }
    
    // These functions dont work when Main camera is the one that works on Windows 
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetKeyMaterial(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        SetKeyMaterial(false);
    }
    
    private void SetKeyMaterial(bool gazedAt)
    {
        if (Rend == null)
        {
            Debug.LogError($"Renderer is missing on {gameObject.name}. Cannot change material.");
            return; 
        }

        if (InactiveMaterial == null || GazedAtMaterial == null)
        {
            Debug.LogError($"Materials are missing on {gameObject.name}. Please assign them in the Inspector.");
            return; 
        }
        
        Rend.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (KeyboardManager != null)
        {
            KeyboardManager.AddLetter(gameObject.name);
        }
        else
        {
            Debug.LogError("KeyboardManager not found. Make sure it is in the scene.");
        }
    }
}