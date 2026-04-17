using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI Header;
    public Button CancelButton;
    public Button DeleteButton;
    public Button AcceptButton;
    public Button[] keyButtons;

    private Coroutine CoBlink;
    private string input = "";
    private string cursor;

    private void Awake()
    {
        CancelButton.onClick.AddListener(OnClickCancel);
        DeleteButton.onClick.AddListener(OnClickDelete);
        AcceptButton.onClick.AddListener(OnClickAccept);

        foreach (var btn in keyButtons)
        {
            string key = btn.GetComponentInChildren<TextMeshProUGUI>().text;
            btn.onClick.AddListener(() => OnClickKey(key));
        }
    }

    private void Update()
    {
        if (input.Length < 7)
        {
            Header.text = input + cursor;
        }
        else
        {
            Header.text = input;
        }
    }

    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            cursor = cursor == "" && input.Length < 7 ? "_" : "";
            yield return new WaitForSeconds(0.3f);
        }
    }

    public override void Open()
    {
        base.Open();
        CoBlink = StartCoroutine(BlinkCursor());
    }

    public override void Close()
    {
        if (CoBlink != null)
        {
            StopCoroutine(CoBlink);
        }
        input = "";
        base.Close();
    }

    public void OnClickKey(string key)
    {
        if (input.Length < 7)
        {
            input += key;
        }
    }

    public void OnClickCancel()
    {
        input = "";
    }

    public void OnClickDelete()
    {
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
        }
    }

    public void OnClickAccept()
    {
        windowManager.Open(0);
    }
}
