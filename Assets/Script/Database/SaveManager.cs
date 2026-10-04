using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // Ini adalah keranjang datamu yang bisa diakses dari script mana saja
    public GameData dataDatabase;

    // Jalur rahasia penyimpanan Android
    private string pathSimpan;

    private void Awake()
    {
        // Sistem Singleton (Biar bisa dipanggil dari mana saja)
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject); // Bawa terus ke semua scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Tentukan lokasi file save khusus Android
        pathSimpan = Application.persistentDataPath + "/SaveDataIkan.json";

        LoadData(); // Otomatis baca database saat game dibuka
    }

    // ==========================================
    // FUNGSI UNTUK MENYIMPAN KE HP ANDROID
    // ==========================================
    public void SaveData()
    {
        // 1. Ubah keranjang data menjadi teks JSON
        string json = JsonUtility.ToJson(dataDatabase, true);

        // 2. Tulis teksnya ke memori internal Android
        File.WriteAllText(pathSimpan, json);

        Debug.Log("Database Berhasil Disimpan di: " + pathSimpan);
    }

    // ==========================================
    // FUNGSI UNTUK MEMBACA DARI HP ANDROID
    // ==========================================
    public void LoadData()
    {
        // Cek apakah pemain sudah punya file save? (Bukan pemain baru)
        if (File.Exists(pathSimpan))
        {
            // 1. Ambil teks JSON dari memori HP
            string isiFile = File.ReadAllText(pathSimpan);

            // 2. Ubah teks JSON kembali menjadi bentuk keranjang data
            dataDatabase = JsonUtility.FromJson<GameData>(isiFile);
            Debug.Log("Database Berhasil Dimuat!");
        }
        else
        {
            // Kalau pemain baru, buatkan keranjang kosong yang baru
            Debug.Log("Pemain Baru! Membuat Database Baru...");
            dataDatabase = new GameData();
            SaveData(); // Langsung save file default-nya
        }
    }
}