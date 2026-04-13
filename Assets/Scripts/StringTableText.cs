using UnityEngine;
using TMPro;

public enum StringIds
{
    Hello,
    Bye,
    YouDie,
    Hi,
}

public class StringTableText : MonoBehaviour
{
    public string id;
    public StringIds stringIds;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI text;

    public static readonly string[] StringIds =
    {
        "Hello",
        "Bye",
        "You die",
        "Hi",
    };

    private void Start()
    {
        OnChangedId();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Variables.Language = Languages.Korean;
            OnChangedId();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Variables.Language = Languages.English;
            OnChangedId();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Variables.Language = Languages.Japanses;
            OnChangedId();
        }
    }

    public void OnChangedId()
    {
        Debug.Log($"현재 언어: {Variables.Language}");
        buttonText.text = StringIds[(int)stringIds];
        text.text = DataTableManager.StringTable.Get(StringIds[(int)stringIds]);
    }
}
