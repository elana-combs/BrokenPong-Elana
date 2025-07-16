using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType { Player1, Player2 }

public class Paddle : MonoBehaviour
{
    public PlayerType playerType;
    public float s = 10f;
    public float d = 4f;

    [SerializeField] private InputAction ia;
    private float m;

    void Awake()
    {
        ia.performed += OnMovePerformed;
        ia.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        ia.Enable();
    }
    
    void OnDisable()
    {
        ia.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        m = context.ReadValue<float> ();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        m = 0;
    }

    void Update()
    {
        float movement = m * s * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -d, d);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
