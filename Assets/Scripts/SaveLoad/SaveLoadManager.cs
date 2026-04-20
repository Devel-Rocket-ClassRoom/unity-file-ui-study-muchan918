using UnityEngine;
using SaveDataVC = SaveDataV4; // 이걸로 버전을 한번에 관리
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text, // .json
        Encrypted, // .dat
    }

    public static SaveMode Mode { get; set; } = SaveMode.Text;

    private static int SaveDataVersion { get; } = 4;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };

    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    public static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }

    public static string GetSaveFilePath(int slot, SaveMode mode)
    {
        string ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
        // 밑에 추가 컨버터
    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    // 0번 슬롯이 자동 저장
    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        // 파일 입출력에서 예외 처리는 써야한다
        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSaveFilePath(0, mode);

            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }

            // if (Mode == SaveMode.Text)
            // {
            //     File.WriteAllText(path, json);
            // }
            // else
            // {
            //     byte[] encrypted = CryptoUtil.Encrypt(json);
            //     File.WriteAllBytes(path, encrypted);
            // }

            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        string path = GetSaveFilePath(0, mode);

        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            SaveData saveData = null;

            if (Mode == SaveMode.Text)
            {
                string json = File.ReadAllText(path);
                saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
            }
            else
            {
                byte[] decode = File.ReadAllBytes(path);
                string json = CryptoUtil.Decrypt(decode);
                saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
            }

            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}
