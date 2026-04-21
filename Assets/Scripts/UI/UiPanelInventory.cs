using UnityEngine;
using TMPro;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
        OnChangeFiltering(filtering.value);
        OnChangeSorting(sorting.value);
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.sortingIndex = (int)uiInvenSlotList.Sorting;
        SaveLoadManager.Data.filteringIndex = (int)uiInvenSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)SaveLoadManager.Data.sortingIndex;
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)SaveLoadManager.Data.filteringIndex;
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);

        sorting.value = SaveLoadManager.Data.sortingIndex;
        filtering.value = SaveLoadManager.Data.filteringIndex;
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}
