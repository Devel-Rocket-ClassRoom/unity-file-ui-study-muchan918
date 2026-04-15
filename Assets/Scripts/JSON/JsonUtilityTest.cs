using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string name;
    public int level;
    public float health;
    public Vector3 position;

    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        {"stage1", 100 },
        {"stage2", 200 }
    };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            PlayerInfo obj = new PlayerInfo
            {
                name = "ABC",
                level = 10,
                health = 10.99f,
                position = new Vector3(1f, 2f, 3f)
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
                "player.json"
            );

            // PrettyPrint를 해놓으면 줄바꿈 해주면서 출력
            string json = JsonUtility.ToJson(obj, prettyPrint: true);
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
                "player.json"
            );

            string json = File.ReadAllText(path);

            // 새로운 객체를 new 해서 할당하는 것
            // PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);
            PlayerInfo obj = new PlayerInfo();

            // 이미 있는 obj를 받아서 json내용을 overwrite하는 것
            JsonUtility.FromJsonOverwrite(json, obj);

            Debug.Log($"{obj.name}, {obj.level}, {obj.health}, {obj.position}");
        }
    }
}
