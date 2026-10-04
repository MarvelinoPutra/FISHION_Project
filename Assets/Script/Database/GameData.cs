using UnityEngine;

// Wajib ditambahkan agar Unity bisa mengubah data ini menjadi file JSON
[System.Serializable]
public class GameData
{
    // ==========================================
    // ISI DENGAN DATA YANG MAU DISIMPAN DI SINI
    // ==========================================

    public int skorTertinggi; // High Score
    public bool sudahTutorial = false;

    // (Bisa tambah pengaturan volume kalau mau, dll)
    // public float musicVolume;

    // Nilai Default saat pertama kali game diinstal
    public GameData()
    {
        skorTertinggi = 0;
        sudahTutorial = false;
    }
}