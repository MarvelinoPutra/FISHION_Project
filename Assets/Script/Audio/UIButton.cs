using UnityEngine;
using UnityEngine.UI; // Wajib untuk tombol

[RequireComponent(typeof(Button))] // Otomatis memastikan objek ini punya komponen Button
public class UIButton : MonoBehaviour
{
    [Header("Drag Suara Klik ke Sini")]
    public AudioClip clickSFX;

    private void Start()
    {
        // Secara otomatis mendaftarkan fungsi PlaySound saat tombol ini diklik
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySoundOnClick);
    }

    private void PlaySoundOnClick()
    {
        // Cari wasit suara (SoundManager) dan putar suaranya
        if (SoundManager.Instance != null && clickSFX != null)
        {
            SoundManager.Instance.PlaySFX(clickSFX);
        }
    }
}