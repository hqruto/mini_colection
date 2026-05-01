using UnityEngine;
using UnityEngine.InputSystem;

public class BallShoot2D : MonoBehaviour
{
    public float shootPower = 5f;
    public Transform shootDirection;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

        if (Touchscreen.current != null &&Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.AddForce(shootDirection.right * shootPower, ForceMode2D.Impulse);
    }
}
