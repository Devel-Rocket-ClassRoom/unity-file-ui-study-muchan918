using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class KeyValueFileSystem : MonoBehaviour
{
    private Dictionary<string, string> settings;
    private string settingPath;

    private string content =
    @"master_volume=80
bgm_volume=70
sfx_volume=90
language=kr
show_damage = true";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingPath = Path.Combine(Application.persistentDataPath, "settings.cfg");
        settings = new Dictionary<string, string>();

        File.WriteAllText(settingPath, content);
        Debug.Log("== 변경전 == \n" + File.ReadAllText(settingPath));

        using (StreamReader sr = new StreamReader(settingPath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split('=');
                settings[temp[0]] = temp[1];
            }
        }

        Debug.Log(string.Join(", ", settings.Keys));

        settings["bgm_volume"] = "50";
        settings["language"] = "en";

        using (StreamWriter sw = new StreamWriter(settingPath))
        {
            string line;
            foreach (var kv in settings)
            {
                line = kv.Key + "=" + kv.Value;
                sw.WriteLine(line);
            }
        }

        Debug.Log("== 변경후 == \n" + File.ReadAllText(settingPath));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
