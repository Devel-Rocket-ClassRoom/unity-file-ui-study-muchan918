using TMPro;
using UnityEngine;

public class UiPanelCharacter : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiCharacterSlotList uiCharacterSlotList;

    private void OnEnable()
    {

    }

    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.Sorting = (UiCharacterSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (UiCharacterSlotList.FilteringOptions)index;
    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();
    }
}
