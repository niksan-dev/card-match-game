using UnityEngine;
using System.IO;

public static class BinarySaveLoadSystem
{
    private static string GetFilePath<T>(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".data");
    }

    public static void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        string encryptedJson = EncryptionHelper.Encrypt(json);
        File.WriteAllText(GetFilePath<T>(fileName), encryptedJson);
    }

    public static T Load<T>(string fileName) where T : new()
    {
        string path = GetFilePath<T>(fileName);
        Debug.Log($"path : {path}");
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionHelper.Decrypt(encryptedJson);
            return JsonUtility.FromJson<T>(decryptedJson);
        }
        Debug.LogWarning($"Save file not found: {path}");
        return new T();
    }
}
