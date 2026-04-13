using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    public StringIds stringIds;
    public Languages language;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            gameObject.GetComponent<LocalizationText>().enabled = false;
        }
    }

    private void OnValidate()
    {
        OnChangedId();
    }

    // 인스펙터에서 변경됐을때 자동으로 실행되는 함수
    public void OnChangedId()
    {
        Variables.Language = language;
        text.text = DataTableManager.StringTable.Get(StringTableText.StringIds[(int)stringIds]);
    }

    [ContextMenu("Apply All Localization Texts")]
    private void ApplyAllLocalizationTexts()
    {
        // LocalizationText[] allTexts = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        LocalizationText[] allTexts = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (LocalizationText locText in allTexts)
        {
            locText.stringIds = stringIds;
            locText.language = language;
            locText.OnChangedId();
        }
    }
}
