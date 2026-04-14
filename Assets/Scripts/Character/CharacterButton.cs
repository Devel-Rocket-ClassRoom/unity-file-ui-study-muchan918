using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public string characterId;
    public Image icon;
    public LocalizationText textName;

    public static event Action<string> OnCharacterSelected;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        if (data == null) return;

        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
    }

    public void OnClickButton()
    {
        OnCharacterSelected?.Invoke(characterId);
    }
}
