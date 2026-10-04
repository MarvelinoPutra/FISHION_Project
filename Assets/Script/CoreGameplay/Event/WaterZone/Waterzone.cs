using UnityEngine;

public class Waterzone : MonoBehaviour
{
    [Header("Pengaturan Fisika Dalam Air")]
    public float waterGravityScale = 0.5f; // Gravitasi melayang/lambat
    public float waterDrag = 4.0f;         // Hambatan air (viskositas)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan kita hanya memproses ikan (objek yang punya script FishMechanic)
        // Ini mencegah error kalau objek lain nggak sengaja masuk
        if (other.GetComponent<Fish>() != null)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Ubah fisika ikan menjadi gaya dalam air SEKALI SAJA saat lewat
                rb.gravityScale = waterGravityScale;
                rb.drag = waterDrag;
            }
        }
    }
}