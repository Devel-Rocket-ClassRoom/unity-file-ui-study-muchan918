using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        AttackAscending,
        AttackDescending,
        DefenceAscending,
        DefenceDescending,
    }

    public enum FilteringOptions
    {
        None,
        Warrior,
        Mage,
        Healer,
        Rogue,
        Archer,
    }

    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),
        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.Attack),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.Attack),
        (lhs, rhs) => lhs.CharacterData.Defence.CompareTo(rhs.CharacterData.Defence),
        (lhs, rhs) => rhs.CharacterData.Defence.CompareTo(lhs.CharacterData.Defence),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Job == JobTypes.Warrior,
        (x) => x.CharacterData.Job == JobTypes.Mage,
        (x) => x.CharacterData.Job == JobTypes.Healer,
        (x) => x.CharacterData.Job == JobTypes.Rogue,
        (x) => x.CharacterData.Job == JobTypes.Archer,
    };

    public UiCharacterSlot prefab;
    public ScrollRect scrollRect;

    private List<UiCharacterSlot> uiSlotList = new List<UiCharacterSlot>();

    private List<SaveCharacterData> saveCharacterDataList = new List<SaveCharacterData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    private void OnSelectSlot(SaveCharacterData saveCharacterData)
    {

    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    private void UpdateSlots()
    {

    }
}
