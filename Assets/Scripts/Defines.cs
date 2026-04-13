public enum Languages
{
    Korean,
    English,
    Japanses,
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
}