// LocalizationText.cs
using System;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    public string id;
    public Languages language;
    public TextMeshProUGUI text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            language = Languages.Korean;
            OnChangedId();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            language = Languages.English;
            OnChangedId();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            language = Languages.Japanses;
            OnChangedId();
        }
    }

    private void OnValidate()
    {
        OnChangedId();
    }

    public void OnChangedId()
    {
        if (text == null) return;

        Variables.Language = language;
        text.text = DataTableManager.StringTable.Get(id);
    }

    [ContextMenu("Change Language")]
    private void ChangeLanguage()
    {
        LocalizationText[] allTexts = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (LocalizationText locText in allTexts)
        {
            locText.language = language;
            locText.OnChangedId();
        }
    }
}