public enum Languages
{
    Korean,
    English,
    Japanses,
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public enum JobTypes
{
    Warrior,
    Mage,
    Healer,
    Rogue,
    Archer,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;  // 추가

    private static Languages language = Languages.Korean;  // 추가
    public static Languages Language  // 추가
    {
        get => language;
        set
        {
            if (language == value) return;
            language = value;
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };

    public static string String => StringTableIds[(int)Variables.Language];

    public static readonly string Item = "ItemTable";

    public static readonly string Character = "CharacterTable";
}