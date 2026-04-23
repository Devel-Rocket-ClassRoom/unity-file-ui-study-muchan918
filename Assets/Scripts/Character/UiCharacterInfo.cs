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

    private SaveCharacterData selectedCharacter;
    private SaveItemData selectedItem;

    public void SetSelectedCharacter(SaveCharacterData character)
    {
        selectedCharacter = character;
    }

    public void SetSelectedItem(SaveItemData item)
    {
        selectedItem = item;
    }

    public void OnEquip()
    {
        if (selectedCharacter == null || selectedItem == null) return;

        switch (selectedItem.ItemData.Type)
        {
            case ItemTypes.Weapon:
                if (selectedCharacter.WeaponItem != selectedItem.ItemData)
                {
                    if (selectedCharacter.WeaponItem != null)
                    {
                        selectedCharacter.CharacterData.Attack -= selectedCharacter.WeaponItem.Value;
                    }
                    selectedCharacter.WeaponItem = selectedItem.ItemData;
                    selectedCharacter.CharacterData.Attack += selectedItem.ItemData.Value;
                }
                break;
            case ItemTypes.Equip:
                if (selectedCharacter.EquipItem != selectedItem.ItemData)
                {
                    if (selectedCharacter.EquipItem != null)
                    {
                        selectedCharacter.CharacterData.Defence -= selectedCharacter.EquipItem.Value;
                    }
                    selectedCharacter.EquipItem = selectedItem.ItemData;
                    selectedCharacter.CharacterData.Defence += selectedItem.ItemData.Value;
                }
                break;
            case ItemTypes.Consumable:
                if (selectedCharacter.ConsumableItem != selectedItem.ItemData)
                {
                    selectedCharacter.ConsumableItem = selectedItem.ItemData;
                }
                break;
        }

        SetSaveCharacterData(selectedCharacter);
    }

    public void OnUnequipWeapon()
    {
        if (selectedCharacter == null || selectedCharacter.WeaponItem == null) return;

        selectedCharacter.CharacterData.Attack -= selectedCharacter.WeaponItem.Value;
        selectedCharacter.WeaponItem = null;

        SetSaveCharacterData(selectedCharacter);
    }

    public void OnUnequipEquip()
    {
        if (selectedCharacter == null || selectedCharacter.EquipItem == null) return;

        selectedCharacter.CharacterData.Defence -= selectedCharacter.EquipItem.Value;
        selectedCharacter.EquipItem = null;

        SetSaveCharacterData(selectedCharacter);
    }

    public void OnUnequipConsumable()
    {
        if (selectedCharacter == null || selectedCharacter.ConsumableItem == null) return;

        selectedCharacter.ConsumableItem = null;

        SetSaveCharacterData(selectedCharacter);
    }

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
        weaponIcon.sprite = saveCharacterData.WeaponItem?.SpriteIcon;
        equipIcon.sprite = saveCharacterData.EquipItem?.SpriteIcon;
        consumableIcon.sprite = saveCharacterData.ConsumableItem?.SpriteIcon;

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
}
