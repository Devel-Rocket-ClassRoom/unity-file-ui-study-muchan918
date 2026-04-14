// ItemButton.cs
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemButton : MonoBehaviour
{
    public string itemId;
    public Image icon;
    public LocalizationText textName;

    public static event Action<string> OnItemSelected;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if (data == null) return;

        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textName.OnChangedId();
    }

    public void OnClickButton()
    {
        OnItemSelected?.Invoke(itemId);
    }
}