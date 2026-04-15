using System;
using UnityEngine;

[Serializable]
public class SomeClass
{
    public int objectType;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

[Serializable]
public class ObjectSaveData
{
    public string prefabName;
    public PrimitiveType objType;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}