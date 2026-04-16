using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManager windowManager;

    public void Init(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        // Canvas 추가할때 생성되는 EventSystem
        EventSystem.current.SetSelectedGameObject(firstSelected);

    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
