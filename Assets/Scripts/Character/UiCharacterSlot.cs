using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveCharacterData saveCharacterData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        saveCharacterData = null;
    }

    public void SetCharacter(SaveCharacterData data)
    {
        saveCharacterData = data;
        imageIcon.sprite = saveCharacterData.CharacterData.SpriteIcon;
        textName.text = saveCharacterData.CharacterData.StringName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetEmpty();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var saveCharacterData = new SaveCharacterData();
            saveCharacterData.CharacterData = DataTableManager.CharacterTable.GetRandom();

            SetCharacter(saveCharacterData);
        }
    }
}
