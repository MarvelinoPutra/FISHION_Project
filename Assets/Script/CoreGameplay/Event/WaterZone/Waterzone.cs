using UnityEngine;

public class Waterzone : MonoBehaviour
{
    [Header("Pengaturan Fisika Dalam Air")]
    public float waterGravityScale = 0.5f; // Gravitasi melayang
    public float waterDrag = 4.0f;         // Hambatan air (viskositas)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang menembus garis adalah Ikan (punya Rigidbody2D)
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Ubah fisika ikan menjadi gaya dalam air SEKALI SAJA saat lewat
            rb.gravityScale = waterGravityScale;

            // Properti yang benar untuk Rigidbody2D adalah .drag
            rb.drag = waterDrag;
        }
    }
}