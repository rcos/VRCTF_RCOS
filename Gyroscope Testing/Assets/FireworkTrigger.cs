using UnityEngine;
using UnityEngine.VFX;

public class FireworkTrigger : MonoBehaviour
{
    public VisualEffect fireworkEffect; // Assign this in the Inspector

    void OnEnable()
    {
        if (fireworkEffect != null)
        {
            fireworkEffect.Play();  // This triggers the firework!
        }
    }
}
