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

public static class Variables
{
    public static Languages Language = Languages.Korean;
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