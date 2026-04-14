using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public void OnClickStringTableKr()
    {
        // var table = new StringTable();
        // table.Load(NameStringTableKr);
        //Debug.Log(table.Get("You die"));

        Debug.Log(DataTableManager.StringTable.Get("You die"));
    }

    public void OnClickStringTableEn()
    {
        var table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("Bye"));
        Debug.Log(table.Get("You die"));
    }

    public void OnClickStringTableJp()
    {
        var table = new StringTable();
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("AAA"));
        Debug.Log(table.Get("Hello"));
    }
}
