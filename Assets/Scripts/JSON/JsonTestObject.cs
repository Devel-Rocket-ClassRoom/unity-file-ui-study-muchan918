using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    private Material mat;
    public string prefabName;

    private void Awake()
    {
        mat = GetComponent<Material>();
    }

    public void Set(ObjectSaveData data)
    {
        // prefabName = data.prefabName;
        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
    }

    public ObjectSaveData GetSaveData()
    {
        ObjectSaveData obj = new ObjectSaveData();
        obj.prefabName = prefabName;
        obj.pos = transform.position;
        obj.rot = transform.rotation;
        obj.scale = transform.localScale;
        obj.color = mat.color;

        return obj;
    }
}
