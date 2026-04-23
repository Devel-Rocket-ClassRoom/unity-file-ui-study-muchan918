using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SaveItemData
{
    public Guid InstanceId { get; set; } // C#의 유니크 아이디

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }

    public DateTime CreationTime { get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        newItem.ItemData.Value = UnityEngine.Random.Range(0, 50);
        return newItem;
    }

    public SaveItemData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{ItemData.Id}";
    }
}
