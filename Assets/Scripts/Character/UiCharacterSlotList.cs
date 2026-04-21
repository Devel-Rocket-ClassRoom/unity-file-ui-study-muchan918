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
    public UnityEvent<SaveCharacterData> onSelectSlot;

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

    public SaveCharacterData GetSaveCharacterData()
    {
        if (selectedSlotIndex == -1) return null;
        return uiSlotList[selectedSlotIndex].saveCharacterData;
    }

    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.saveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetCharacter(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].saveCharacterData);
        UpdateSlots();
    }

    public void OnItemRemoved(ItemData removedItemData)
    {
        foreach (var character in saveCharacterDataList)
        {
            if (character.WeaponItem == removedItemData)
            {
                character.CharacterData.Attack -= removedItemData.Value;
                character.WeaponItem = null;
            }
            if (character.EquipItem == removedItemData)
            {
                character.CharacterData.Defence -= removedItemData.Value;
                character.EquipItem = null;
            }
            if (character.ConsumableItem == removedItemData)
            {
                character.ConsumableItem = null;
            }
        }

        UpdateSlots();
    }
}
