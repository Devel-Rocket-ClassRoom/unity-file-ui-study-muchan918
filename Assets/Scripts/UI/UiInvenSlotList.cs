using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public UiInventorySlot prefab;
    public ScrollRect scrollRect;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 10; i++)
            {
                var saveItemData = SaveItemData.GetRandomItem();
                var newIven = Instantiate(prefab, scrollRect.content);
                newIven.SetItem(saveItemData);
            }
        }
    }
}
