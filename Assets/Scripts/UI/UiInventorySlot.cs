using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventorySlot : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData saveItemData { get; private set; }

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        saveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        saveItemData = data;
        imageIcon.sprite = saveItemData.ItemData.SpriteIcon;
        textName.text = saveItemData.ItemData.StringName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetEmpty();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var saveItemData = new SaveItemData();
            saveItemData.ItemData = DataTableManager.ItemTable.Get("Item1");

            SetItem(saveItemData);
        }
    }
}
