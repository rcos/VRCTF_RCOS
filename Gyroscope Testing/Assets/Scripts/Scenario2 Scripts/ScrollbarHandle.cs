using UnityEngine;
using UnityEngine.UI;

public class ScrollbarHandle : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    private Transform cam; 
    private bool isDragging = false;

    private float dragStartCamRotY;
    private float dragStartScrollbarValue;

    [SerializeField] private float minRotY = 0f;    // top bound (degrees)
    [SerializeField] private float maxRotY = 90f;   // bottom bound (degrees)

    public Image handleImage;
    private Color normalColor = new Color(0.5f, 0, 0.5f); // purple
    private Color draggingColor = new Color(1f, 0.65f, 0f); // orange

    void Start()
    {
        cam = Camera.main.transform;
    }

    public void OnPointerClick()
    {
        isDragging = !isDragging;

        handleImage.color = isDragging ? draggingColor : normalColor;

        if (isDragging)
        {
            dragStartCamRotY = cam.eulerAngles.y;      // starting rotation
            dragStartScrollbarValue = scrollbar.value; // starting scrollbar
        }
    }

    void Update()
    {
        if (isDragging && scrollbar != null)
        {
            float currentRotY = cam.eulerAngles.y;

            // Handle wrap-around (360 → 0 issue)
            float deltaY = Mathf.DeltaAngle(dragStartCamRotY, currentRotY);

            float totalRange = maxRotY - minRotY;
            float normalizedDelta = deltaY / totalRange;

            scrollbar.value = Mathf.Clamp01(dragStartScrollbarValue + normalizedDelta);
        }
    }
}

