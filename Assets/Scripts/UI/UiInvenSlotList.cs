using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        CostAscending,
        CostDescending,
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NonConsumable,
    }

    public readonly System.Comparison<SaveItemData>[] comparison =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };

    public UiInventorySlot prefab;
    public ScrollRect scrollRect;

    private List<UiInventorySlot> uiSlotList = new List<UiInventorySlot>(); // 데이터 뷰어(보여주는 용도)

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>(); // 실제 데이터

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
    public UiItemInfo uiItemInfo;

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        uiItemInfo.SetSaveItemData(saveItemData);
        Debug.Log(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    void OnEnable()
    {

    }

    private void OnDisable()
    {
        saveItemDataList = null;
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparison[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {

            // 생성하는 부분
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.saveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        // 갱신
        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count) // setItem 해줘야함
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else // 비활성화
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].saveItemData);
        UpdateSlots();
    }
}
