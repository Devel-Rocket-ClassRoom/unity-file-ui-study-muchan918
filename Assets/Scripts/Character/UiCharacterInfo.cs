using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image characterIcon;
    public Image weaponIcon;
    public Image equipIcon;
    public Image consumableIcon;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI textJob;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textStat;

    public void SetEmpty()
    {
        characterIcon.sprite = null;
        weaponIcon.sprite = null;
        equipIcon.sprite = null;
        consumableIcon.sprite = null;
        textName.text = string.Empty;
        textJob.text = string.Empty;
        textDesc.text = string.Empty;
        textStat.text = string.Empty;
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        CharacterData characterData = saveCharacterData.CharacterData;

        characterIcon.sprite = characterData.SpriteIcon;
        weaponIcon.sprite = saveCharacterData.WeaponItem.SpriteIcon;
        equipIcon.sprite = saveCharacterData.EquipItem.SpriteIcon;
        consumableIcon.sprite = saveCharacterData.ConsumableItem.SpriteIcon;

        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), characterData.StringName);

        string id = characterData.Job.ToString().ToUpper();
        textJob.text = string.Format(FormatCommon,
            DataTableManager.StringTable.Get("JOB"),
            DataTableManager.StringTable.Get(id));
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), characterData.StringDesc);
        textStat.text = string.Format(DataTableManager.StringTable.Get("StatFormat"),
            characterData.Attack,
            characterData.Defence
        );

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetEmpty();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSaveCharacterData(SaveCharacterData.GetRandomCharacter());
        }
    }
}
