using UnityEngine;
using UnityEngine.UI;

public class CharacterExplain : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText textStat;

    private void OnEnable()
    {
        CharacterButton.OnCharacterSelected += Setup;
    }

    private void OnDisable()
    {
        CharacterButton.OnCharacterSelected -= Setup;
    }

    public void Setup(string characterId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        if (data == null) return;

        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
        textDesc.id = data.Desc;
        textDesc.OnChangedId();
    }
}