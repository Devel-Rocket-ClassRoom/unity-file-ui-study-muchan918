using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindowAnswer : GenericWindow
{
    private readonly StringBuilder sb = new StringBuilder();

    public TextMeshProUGUI inputField;
    public Button cancelButton;
    public Button deleteButton;
    public Button acceptButton;

    public GameObject rootKeyboard;
    public int maxCharacters = 7;

    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>().text;
            key.onClick.AddListener(() => OnKey(text));
        }

        cancelButton.onClick.AddListener(OnCancel);
        deleteButton.onClick.AddListener(OnDelete);
        acceptButton.onClick.AddListener(OnAccept);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > cursorDelay)
        {
            timer = 0f;
            blink = !blink;
            UpdateInputField();
        }
    }

    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;
        UpdateInputField();
        base.Open();
    }

    private void UpdateInputField()
    {
        bool ShowCursor = sb.Length < maxCharacters && !blink;
        if (ShowCursor)
        {
            sb.Append('_');
        }
        inputField.SetText(sb);
        if (ShowCursor)
        {
            sb.Length -= 1;
        }
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }

    public void OnCancel()
    {
        sb.Clear();
        UpdateInputField();
    }

    public void OnDelete()
    {
        if (sb.Length > 0)
        {
            sb.Length -= 1;
            UpdateInputField();
        }
    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }
}
