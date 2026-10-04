using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [Header("Pengaturan Batas Geser (Kiri/Kanan)")]
    [Tooltip("Batas jarak X untuk layar standar (16:9)")]
    public float batasUntuk16_9 = 3.0f;

    [Tooltip("Batas jarak X untuk layar memanjang (21:9)")]
    public float batasUntuk21_9 = 2.4f;

    private float batasAktif;

    // Variabel publik ini hanya bisa dibaca (get) oleh script lain
    public bool HasValidInput { get; private set; }
    public Vector3 InputWorldPosition { get; private set; }
    public bool IsDropping { get; private set; }

    private void Start()
    {
        // 1. Kalkulasi batas dinamis berdasarkan rasio layar HP saat game dimulai
        float rasioLayar = (float)Screen.width / Screen.height;

        // Layar 21:9 rasionya ~0.428 | Layar 16:9 rasionya ~0.562
        float persentase = Mathf.InverseLerp(0.428f, 0.562f, rasioLayar);

        // Menggabungkan angkanya berdasarkan bentuk HP
        batasAktif = Mathf.Lerp(batasUntuk21_9, batasUntuk16_9, persentase);
    }

    void Update()
    {
        // 2. CEK GAME OVER: Jika game sudah berakhir, blokir semua input jari/mouse!
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            HasValidInput = false;
            IsDropping = false;
            return;
        }

        HasValidInput = false;
        IsDropping = false;

        // 3. CEK INPUT ANDROID / MOBILE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Blokir jika menyentuh UI Canvas (seperti tombol pause)
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            HasValidInput = true;
            InputWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Ended)
                IsDropping = true;
        }
        // 4. CEK INPUT PC / WEBGL
        else
        {
            // Blokir jika kursor di atas UI Canvas
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            HasValidInput = true;
            InputWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
                IsDropping = true;
        }

        // 5. BATASI POSISI KIRI DAN KANAN AGAR IKAN TIDAK KELUAR AKUARIUM
        if (HasValidInput)
        {
            float xDibatasi = Mathf.Clamp(InputWorldPosition.x, -batasAktif, batasAktif);
            InputWorldPosition = new Vector3(xDibatasi, InputWorldPosition.y, InputWorldPosition.z);
        }
    }
}