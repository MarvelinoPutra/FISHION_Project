using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    // Variabel publik ini hanya bisa dibaca (get) oleh script lain
    public bool HasValidInput { get; private set; }
    public Vector3 InputWorldPosition { get; private set; }
    public bool IsDropping { get; private set; }

    void Update()
    {
        HasValidInput = false;
        IsDropping = false;

        // 1. Cek Input Android / Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return; // Blokir jika menyentuh UI

            HasValidInput = true;
            InputWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Ended)
                IsDropping = true;
        }
        // 2. Cek Input PC / WebGL
        else
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return; // Blokir jika kursor di atas UI

            HasValidInput = true;
            InputWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
                IsDropping = true;
        }
    }
}