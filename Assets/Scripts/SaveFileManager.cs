using UnityEngine;
using System.IO;

public class SaveFileManager : MonoBehaviour
{
    private int index = 0;
    private string saveDir;
    private string saveFile1;
    private string saveFile2;
    private string saveFile3;
    private string saveBackupFile1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string saveDir = Path.Combine(Application.persistentDataPath, "Save");
        string saveFile1 = Path.Combine(saveDir, "save1.txt");
        string saveFile2 = Path.Combine(saveDir, "save2.txt");
        string saveFile3 = Path.Combine(saveDir, "save3.txt");
        string saveBackupFile1 = Path.Combine(saveDir, "save1_backup.txt");

        // 폴더가 없으면 생성
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            Debug.Log($"폴더 생성: {saveDir}");
        }
        else
        {
            Debug.Log("폴더가 이미 존재합니다.");
        }

        File.WriteAllText(saveFile1, "내용1");
        File.WriteAllText(saveFile2, "내용2");
        File.WriteAllText(saveFile3, "내용3");

        // 폴더 내 파일 목록
        string[] files = Directory.GetFiles(saveDir);
        foreach (string file in files)
        {
            Debug.Log($"파일: {Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 복사
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CopyFile();
        }

        // 파일 내용 변경
        if (Input.GetKeyDown(KeyCode.W))
        {
            using (StreamWriter w = File.CreateText(saveFile1))
            {
                w.Write($"변경 {index}");
            }
            index += 1;
        }

        // 읽기
        if (Input.GetKeyDown(KeyCode.E))
        {

        }

        // 삭제
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    void CopyFile()
    {
        string copyContent;

        using (StreamReader reader = File.OpenText(saveFile1))
        {
            copyContent = reader.ReadLine();
        }

        using (FileStream copy = File.Create(saveBackupFile1))
        using (StreamWriter w = File.CreateText(saveBackupFile1))
            w.Write(copyContent);
    }
}
