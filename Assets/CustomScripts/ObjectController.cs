using UnityEngine;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float _minObjectDistance = 2.5f;
    private const float _maxObjectDistance = 3.5f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;

    private Renderer _myRenderer;
    private Vector3 _startingPosition;
    
    private KeyboardManager _keyboardManager;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        // Commented out the first line below because this is specific to the Google Cardboard SDK provided scene
        // _startingPosition = transform.parent.localPosition;
        _myRenderer = GetComponent<Renderer>();
        _keyboardManager = GetComponent<KeyboardManager>();
        SetMaterial(false);
    }
    
    // Adding a function to spin the object on pointer click 
    public void RotateObject()
    {
        transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        // On screen touch, you can decide what to do with the Interactable object
        // TeleportRandomly();
        // RotateObject();
        if (_keyboardManager != null)
        {
            _keyboardManager.AddLetter(gameObject.name);
        }
    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
        
        // Added a debug message at the current game object being stared at
        if (gazedAt)
        {
            Debug.Log("Gazed at: " + gameObject.tag);
        }
    }
}
