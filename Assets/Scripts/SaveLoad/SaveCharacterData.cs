using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class SaveCharacterData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData WeaponItem { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData EquipItem { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ConsumableItem { get; set; }

    public DateTime CreationTime { get; set; }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new SaveCharacterData();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        // newCharacter.WeaponItem = DataTableManager.ItemTable.GetRandom();
        // newCharacter.EquipItem = DataTableManager.ItemTable.GetRandom();
        // newCharacter.ConsumableItem = DataTableManager.ItemTable.GetRandom();

        return newCharacter;
    }

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{CharacterData.Id}";
    }
}
