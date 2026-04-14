// ItemExplain.cs
using UnityEngine;
using UnityEngine.UI;

public class ItemExplain : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;

    private void OnEnable()
    {
        ItemButton.OnItemSelected += Setup;
    }

    private void OnDisable()
    {
        ItemButton.OnItemSelected -= Setup;
    }

    public void Setup(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if (data == null) return;

        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
        textDesc.id = data.Desc;
        textDesc.OnChangedId();
    }
}