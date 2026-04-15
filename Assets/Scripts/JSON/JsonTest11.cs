using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonTest11 : MonoBehaviour
{
    public string fileName = "test.json";
    public string FullFilePath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    public JsonSerializerSettings jsonSettings;

    public GameObject cube;

    public GameObject[] Prefabs;

    private List<GameObject> sceneObjects;
    private List<int> objectType;

    private void Awake()
    {
        sceneObjects = new List<GameObject>();
        objectType = new List<int>();

        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            SomeClass obj = new SomeClass
            {
                pos = new Vector3(1f, 1f, 1f),
                rot = new Quaternion(1f, 1f, 1f, 0f),
                scale = new Vector3(1f, 1f, 1f),
                color = new Color(255f, 255f, 255f, 255f)
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
            );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "some.json"
            );

            // PrettyPrint를 해놓으면 줄바꿈 해주면서 출력
            string json = JsonConvert.SerializeObject(obj, jsonSettings);
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Load
            string path = Path.Combine(
                Application.persistentDataPath,
                "JsonTest",
                "some.json"
            );

            string json = File.ReadAllText(path);
            SomeClass obj = JsonConvert.DeserializeObject<SomeClass>(json, jsonSettings);

            Debug.Log($"{json}");
            Debug.Log($"{obj}");
        }
    }

    public void Save()
    {
        List<SomeClass> list = new List<SomeClass>();
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            SomeClass obj = new SomeClass();
            obj.objectType = objectType[i];
            obj.pos = sceneObjects[i].transform.position;
            obj.rot = sceneObjects[i].transform.rotation;
            obj.scale = sceneObjects[i].transform.localScale;
            obj.color = sceneObjects[i].GetComponent<Renderer>().material.color;
            list.Add(obj);
        }
        string json = JsonConvert.SerializeObject(list, jsonSettings);
        File.WriteAllText(FullFilePath, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FullFilePath);
        var obj = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSettings);

        Clear();

        for (int i = 0; i < obj.Count; i++)
        {
            var o = obj[i];
            GameObject go = Instantiate(Prefabs[o.objectType], o.pos, o.rot);
            go.transform.localScale = o.scale;
            go.GetComponent<Renderer>().material.color = o.color;
            sceneObjects.Add(go);
            objectType.Add(o.objectType);
        }

        //Debug.Log(json);
    }

    public void Create()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = new Vector3(
                UnityEngine.Random.Range(0f, 50f),
                UnityEngine.Random.Range(0f, 50f),
                UnityEngine.Random.Range(0f, 50f)
            );
            Quaternion randomRot = Quaternion.Euler(
                UnityEngine.Random.Range(0f, 360f),
                UnityEngine.Random.Range(0f, 360f),
                UnityEngine.Random.Range(0f, 360f)
            );
            Vector3 randomScale = new Vector3(
                UnityEngine.Random.Range(0f, 5f),
                UnityEngine.Random.Range(0f, 5f),
                UnityEngine.Random.Range(0f, 5f)
            );
            Color randomColor = new Color(
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f)
            );

            int randomIndex = UnityEngine.Random.Range(0, 4);
            GameObject obj = Instantiate(Prefabs[randomIndex], randomPos, randomRot);
            obj.transform.localScale = randomScale;
            obj.GetComponent<Renderer>().material.color = randomColor;
            sceneObjects.Add(obj);
            objectType.Add(randomIndex);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            Destroy(sceneObjects[i]);
        }
        sceneObjects.Clear();
        objectType.Clear();
    }
}
