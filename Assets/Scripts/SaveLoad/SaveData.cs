using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp(); // 다음 버전의 이것을 return해준다
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;


        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    // 항상 초기값을 줘야한다
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveDataV2
{
    public List<string> itemList = new List<string>();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach (string id in itemList)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }

        return data;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new List<SaveItemData>();

    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveData VersionUp()
    {
        SaveDataV5 data = new SaveDataV5();
        data.Name = Name;
        data.Gold = Gold;
        data.ItemList = ItemList.ToList();

        return data;
    }
}

[System.Serializable]
public class SaveDataV5 : SaveDataV4
{
    public int sortingIndex = 0;
    public int filteringIndex = 0;

    public SaveDataV5()
    {
        Version = 5;
    }

    public override SaveData VersionUp()
    {
        SaveDataV6 data = new SaveDataV6();
        data.Name = Name;
        data.Gold = Gold;
        data.ItemList = ItemList.ToList();

        return data;
    }
}

[System.Serializable]
public class SaveDataV6 : SaveDataV5
{
    public int ItemSortingIndex = 0;
    public int ItemFilteringIndex = 0;
    public int CharacterSortingIndex = 0;
    public int CharacterFilteringIndex = 0;

    public List<SaveCharacterData> CharacterList = new List<SaveCharacterData>();

    public SaveDataV6()
    {
        Version = 6;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}