using TMPro;
using UnityEngine;

public class placementprefab : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    [SerializeField]
    private TextMeshPro OverlayText;

    [SerializeField]
    private Canvas canvasComponent;

    [SerializeField]
    private string OverlayDisplayText;

    public void SetOverlayText(string text)
    {
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(true);
            OverlayText.text = text;
        }
    }

    private void Awake()
    {
        OverlayText = GetComponentInChildren<TextMeshPro>();
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(false);
        }
    }

    public void ToggleOverlay()
    {
        OverlayText.gameObject.SetActive(IsSelected);
        OverlayText.text = OverlayDisplayText;
    }

    public void ToggleCanvas()
    {
        canvasComponent?.gameObject.SetActive(IsSelected);
    }
}