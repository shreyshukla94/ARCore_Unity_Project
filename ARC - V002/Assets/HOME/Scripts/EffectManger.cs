using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class EffectManger : MonoBehaviour
{
    [SerializeField]
    private Light defaultlight;

    [SerializeField]
    private Button togglelightbutton;

    [SerializeField]
    private Button toggleshadowbutton;

    // Start is called before the first frame update
    private void Start()
    {
        if (togglelightbutton == null || toggleshadowbutton == null)
        {
            enabled = false;
            return;
        }

        if (defaultlight == null)
        {
            enabled = false;
            return;
        }
        togglelightbutton.onClick.AddListener(ToggleLights);
        toggleshadowbutton.onClick.AddListener(ToggleShadows);
    }

    private void ToggleLights()
    {
        defaultlight.enabled = !defaultlight.enabled;
    }

    private void ToggleShadows()
    {
        if (defaultlight.enabled)
        {
            float shadowvalue = defaultlight.shadowStrength > 0 ? 0 : 1;
            defaultlight.shadowStrength = shadowvalue;
        }
    }
}