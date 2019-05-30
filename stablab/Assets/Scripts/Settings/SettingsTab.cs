using UnityEngine;
using UnityEngine.UI;

public class SettingsTab : MonoBehaviour
{
    [SerializeField]
    private CheckboxController hightTrackerDisabled;
    [SerializeField]
    private CheckboxController invertedControls;
    [SerializeField]
    private CheckboxController tooltipDisabled;
    [SerializeField]
    private InputField screenShotFilePath;

    private void OnEnable()
    {
        hightTrackerDisabled.SetChecked(!Settings.data.hightTrackerActivated);
        invertedControls.SetChecked(Settings.data.invertedControls);
        tooltipDisabled.SetChecked(!Settings.data.tooltipEnabled);
        screenShotFilePath.text = Settings.data.screenShotFilePath;
    }
}
