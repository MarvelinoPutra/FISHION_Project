using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSequencePlayer : MonoBehaviour
{
    public Sprite[] frames;       // isi 12 sprite di sini, urut dari frame 0 - 11
    public float frameRate = 12f; // berapa frame per detik

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (frames.Length > 0)
        {
            sr.sprite = frames[0];
        }
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        float frameDuration = 1f / frameRate;

        if (timer >= frameDuration)
        {
            timer -= frameDuration;
            currentFrame = (currentFrame + 1) % frames.Length; // balik ke 0 lagi setelah frame terakhir
            sr.sprite = frames[currentFrame];
        }
    }
}