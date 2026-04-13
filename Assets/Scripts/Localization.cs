using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Localization : MonoBehaviour
{
    public StringIds stringIds;
    public Languages language;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            gameObject.GetComponent<Localization>().enabled = false;
        }
    }

    private void OnValidate()
    {
        OnChangedId();
    }

    // 인스펙터에서 변경됐을때 자동으로 실행되는 함수
    private void OnChangedId()
    {
        Variables.Language = language;
        buttonText.text = StringTableText.StringIds[(int)stringIds];
        text.text = DataTableManager.StringTable.Get(StringTableText.StringIds[(int)stringIds]);
    }
}