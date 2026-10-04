using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // Keranjang data (mengambil dari file GameData yang asli)
    public GameData dataDatabase;

    private string pathSimpan;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pathSimpan = Application.persistentDataPath + "/SaveDataIkan.json";
        LoadData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataDatabase, true);
        File.WriteAllText(pathSimpan, json);
    }

    public void LoadData()
    {
        if (File.Exists(pathSimpan))
        {
            string isiFile = File.ReadAllText(pathSimpan);
            dataDatabase = JsonUtility.FromJson<GameData>(isiFile);
        }
        else
        {
            dataDatabase = new GameData();
            SaveData();
        }
    }
}