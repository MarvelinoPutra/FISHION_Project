using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    public Sprite openEyes;            // frame 1: mata terbuka
    public Sprite closedEyes;          // frame 2: mata tertutup
    public float minOpenTime = 1f;     // durasi minimal mata terbuka
    public float maxOpenTime = 3f;     // durasi maksimal mata terbuka
    public float blinkDuration = 0.15f; // lama mata tertutup

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float timer;
    private bool isBlinking;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        sr.sprite = openEyes;
        timer = Random.Range(minOpenTime, maxOpenTime);
    }

    void Update()
    {
        // Selama masih menunggu di atas (Kinematic), jangan animasi dulu
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isBlinking = !isBlinking;
            sr.sprite = isBlinking ? closedEyes : openEyes;
            timer = isBlinking ? blinkDuration : Random.Range(minOpenTime, maxOpenTime);
        }
    }
}