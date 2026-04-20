using System.Collections.Generic;
using SaveDataVC = SaveDataV3;
using System.Linq;
using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // SaveLoadManager.Data = new SaveDataVC();
            // SaveLoadManager.Data.Name = "test1234";
            // SaveLoadManager.Data.Gold = 4321;

            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                //Debug.Log(SaveLoadManager.Data.Name);
                //Debug.Log(SaveLoadManager.Data.Gold);
                //Debug.Log(SaveLoadManager.Data.Name);
                // for (int i = 0; i < SaveLoadManager.Data.Item.Count; i++)
                // {
                //     Debug.Log(SaveLoadManager.Data.itemList[i]);
                // }
                foreach (var saveItemData in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(saveItemData.InstanceId);
                    Debug.Log(saveItemData.ItemData.Name);
                    Debug.Log(saveItemData.CreationTime);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }

        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     int index = Random.Range(0, DataTableManager.ItemTable.table.Count);
        //     string random = DataTableManager.ItemTable.table.Keys.ToList()[index];
        //     Debug.Log(random);
        //     SaveLoadManager.Data.itemList.Add(random);
        // }
    }
}
