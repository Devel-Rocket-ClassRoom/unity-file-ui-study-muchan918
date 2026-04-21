using TMPro;
using UnityEngine;

public class UiCharacterSaveLoad : MonoBehaviour
{
    public TMP_Dropdown itemSorting;
    public TMP_Dropdown itemFiltering;
    public TMP_Dropdown characterSorting;
    public TMP_Dropdown characterFiltering;

    public UiInvenSlotList uiInvenSlotList;
    public UiCharacterSlotList uiCharacterSlotList;

    private void OnEnable()
    {
        OnLoad();
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.CharacterList = uiCharacterSlotList.GetSaveCharacterDataList();
        SaveLoadManager.Data.ItemSortingIndex = (int)uiInvenSlotList.Sorting;
        SaveLoadManager.Data.ItemFilteringIndex = (int)uiInvenSlotList.Filtering;
        SaveLoadManager.Data.CharacterSortingIndex = (int)uiCharacterSlotList.Sorting;
        SaveLoadManager.Data.CharacterFilteringIndex = (int)uiCharacterSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)SaveLoadManager.Data.ItemSortingIndex;
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)SaveLoadManager.Data.ItemFilteringIndex;
        uiCharacterSlotList.Sorting = (UiCharacterSlotList.SortingOptions)SaveLoadManager.Data.CharacterSortingIndex;
        uiCharacterSlotList.Filtering = (UiCharacterSlotList.FilteringOptions)SaveLoadManager.Data.CharacterFilteringIndex;

        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
        uiCharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);

        itemSorting.value = SaveLoadManager.Data.ItemSortingIndex;
        itemFiltering.value = SaveLoadManager.Data.ItemFilteringIndex;
        characterSorting.value = SaveLoadManager.Data.CharacterSortingIndex;
        characterFiltering.value = SaveLoadManager.Data.CharacterFilteringIndex;
    }
}
