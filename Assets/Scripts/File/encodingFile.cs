using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class encodingFile : MonoBehaviour
{
    private string secretFile;
    private string encodeFile;
    private string decodeFile;
    private byte secretKey = 0xAB;
    private string content = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secretFile = Path.Combine(Application.persistentDataPath, "secret.txt");
        File.WriteAllText(secretFile, "Hello Unity World!");

        encodeFile = Path.Combine(Application.persistentDataPath, "encode.dat");
        decodeFile = Path.Combine(Application.persistentDataPath, "decode.txt");

        // 원본
        using (StreamReader streamReader = File.OpenText(secretFile))
        {
            content = "원본:";
            content += streamReader.ReadLine();
            Debug.Log(content);
        }

        // 암호화
        using (FileStream secret = File.OpenRead(secretFile))
        {
            using (FileStream encode = File.OpenWrite(encodeFile))
            {
                while (true)
                {
                    int temp = secret.ReadByte();

                    if (temp == -1) break;

                    encode.WriteByte((byte)(secretKey ^ temp));
                }

                Debug.Log($"암호화 완료 (파일크기: {encode.Length})");
            }
        }

        // 암호화 확인
        using (FileStream encode = File.OpenRead(encodeFile))
        {
            content = "";
            while (true)
            {
                int temp = encode.ReadByte();
                if (temp == -1) break;

                content += encode.ReadByte();
            }

            Debug.Log($"암호화 결과: {content}");
        }

        // 복호화
        using (FileStream encode = File.OpenRead(encodeFile))
        {
            using (StreamWriter decode = new StreamWriter(decodeFile))
            {
                content = "";

                while (true)
                {
                    int temp = encode.ReadByte();
                    if (temp == -1) break;

                    decode.Write((char)(temp ^ secretKey));
                    //Debug.Log((char)(temp ^ secretKey));
                }
            }
            Debug.Log("복호화 완료");
        }

        // 복호화 결과
        using (StreamReader sr = new StreamReader(decodeFile))
        {
            Debug.Log($"복호화 결과: {sr.ReadLine()}");
        }

        // 원본과 일치
        string original = File.ReadAllText(secretFile);
        string decoding = File.ReadAllText(decodeFile);

        Debug.Log($"원본과 일치: {original == decoding}");
    }
}
