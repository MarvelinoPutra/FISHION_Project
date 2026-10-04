using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panel Utama")]
    public GameObject Menu;
    public GameObject Setting;
    public GameObject Credits_UI;
    public GameObject Tutorial_UI;

    [Header("Pengaturan Tutorial Tap-Tap")]
    public Image layarTutorial;      // Tempat gambar ditampilkan
    public Sprite[] gambarTutorial;  // Kumpulan gambar tutorialmu
    private int indeksTutorial = 0;  // Penanda gambar ke-berapa

    // =====================================
    // FUNGSI MENU UTAMA
    // =====================================

    private void Start()
    {
        // Cek apakah SaveManager sudah siap dan apakah ini pemain baru
        StartCoroutine(CekPemainBaruRoutine());
    }

    private IEnumerator CekPemainBaruRoutine()
    {
        // Berikan jeda 1 frame sebentar agar SaveManager di objek sebelah siap terlebih dahulu
        yield return null; 

        if (SaveManager.Instance != null)
        {
            // Jika pemain belum pernah menyelesaikan tutorial
            if (SaveManager.Instance.dataDatabase.tutorialSelesai == false)
            {
                // Langsung buka panel tutorial secara otomatis!
                Tutorial(); 

                // Ubah statusnya menjadi true supaya di masa depan tidak muncul terus
                SaveManager.Instance.dataDatabase.tutorialSelesai = true;
                SaveManager.Instance.SaveData(); // Simpan perubahan ke memori HP Android
            }
        }
    }
    public void PlayGame()
    {
        PutarSuaraKlik();
        SceneManager.LoadSceneAsync("MainGame");
    }

    public void ExitGame()
    {
        PutarSuaraKlik();
        Application.Quit();
    }

    public void Settings()
    {
        PutarSuaraKlik();
        Menu.SetActive(false);
        Setting.SetActive(true);
    }

    public void Credits()
    {
        PutarSuaraKlik();
        Menu.SetActive(false);
        Credits_UI.SetActive(true);
    }

    public void BackToMenu()
    {
        PutarSuaraKlik();
        Menu.SetActive(true);
        Setting.SetActive(false);
        Credits_UI.SetActive(false);
        if (Tutorial_UI != null) Tutorial_UI.SetActive(false);
    }

    // =====================================
    // SISTEM TUTORIAL TAP-TAP
    // =====================================
    public void Tutorial()
    {
        PutarSuaraKlik();
        Menu.SetActive(false);
        Tutorial_UI.SetActive(true);

        indeksTutorial = 0; // Mulai dari gambar pertama
        UpdateGambarTutorial();
    }

    public void LanjutTutorial()
    {
        PutarSuaraKlik();
        indeksTutorial++; // Geser ke gambar berikutnya

        // Cek apakah masih ada sisa gambar tutorial
        if (indeksTutorial < gambarTutorial.Length)
        {
            UpdateGambarTutorial(); // Tampilkan gambar selanjutnya
        }
        else
        {
            // Kalau gambar sudah habis, otomatis kembali ke menu utama
            BackToMenu();
        }
    }

    private void UpdateGambarTutorial()
    {
        if (gambarTutorial.Length > 0 && layarTutorial != null)
        {
            layarTutorial.sprite = gambarTutorial[indeksTutorial];
        }
    }

    private void PutarSuaraKlik()
    {
        if (SoundManager.Instance != null) SoundManager.Instance.PlayClickSound();
    }
}