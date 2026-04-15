using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// 이걸 붙여야만 직렬화 역직렬화 해서 넣어줘야 한다
[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            PlayerState obj = new PlayerState
            {
                playerName = "ABC",
                lives = 10,
                health = 10.99f,
                position = new Vector3(1f, 1f, 1f)
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
                "player2.json"
            );

            // PrettyPrint를 해놓으면 줄바꿈 해주면서 출력
            string json = JsonConvert.SerializeObject(obj, jsonSetting);
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
                "player2.json"
            );

            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting);

            Debug.Log($"{json}");
            Debug.Log($"{obj}");
        }
    }
}
