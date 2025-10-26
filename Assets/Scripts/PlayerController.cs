using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float upForce = 250f;
    private Rigidbody rb;
    public float speedForce = 50f;
    private PlayerInput inputPlayer;
    private Vector2 input;
    private Vector3 inicio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputPlayer = GetComponent<PlayerInput>();
        inicio = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        input = inputPlayer.actions["move"].ReadValue<Vector2>();
        if (transform.position.y < -2f)
        {
            Reset();
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(input.x, 0f, input.y) * speedForce);
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            rb.AddForce(Vector3.up * upForce);
            Debug.Log(callbackContext.phase);
        }
    }
    private void Reset()
    {
        transform.position = inicio;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

}
