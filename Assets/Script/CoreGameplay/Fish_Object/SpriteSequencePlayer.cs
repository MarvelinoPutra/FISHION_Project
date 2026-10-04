using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Wajib ditambahkan agar bisa mengakses komponen UI Canvas

public class ImageSequencePlayer : MonoBehaviour
{
    public Sprite[] frames;       // isi sprite di sini, urut dari frame awal sampai akhir
    public float frameRate = 12f; // berapa frame per detik

    private Image img;            // BERUBAH: Menggunakan Image, bukan SpriteRenderer
    private int currentFrame;
    private float timer;

    void Awake()
    {
        // BERUBAH: Mengambil komponen Image dari Canvas
        img = GetComponent<Image>();
    }

    void Start()
    {
        if (frames.Length > 0 && img != null)
        {
            img.sprite = frames[0];
        }
    }

    void Update()
    {
        if (frames.Length == 0 || img == null) return;

        timer += Time.deltaTime;
        float frameDuration = 1f / frameRate;

        if (timer >= frameDuration)
        {
            timer -= frameDuration;
            currentFrame = (currentFrame + 1) % frames.Length; // balik ke 0 lagi setelah frame terakhir

            // BERUBAH: Memasukkan gambar ke komponen Image
            img.sprite = frames[currentFrame];
        }
    }
}