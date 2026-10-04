using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    // Gunakan 'async' karena proses menyambung ke server Unity butuh waktu beberapa milidetik
    async void Start()
    {
        try
        {
            // 1. Menyalakan mesin utama Unity Gaming Services
            await UnityServices.InitializeAsync();

            // 2. Memberi izin Analytics untuk mulai merekam data (Wajib panggil ini!)
            AnalyticsService.Instance.StartDataCollection();

            Debug.Log("Unity Analytics Berhasil Dinyalakan!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Gagal menyalakan Analytics: " + e.Message);
        }
    }
}