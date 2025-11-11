using UnityEngine;

public class CamaraFollower : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Ajustes de cámara")]
    public Vector3 offset = new Vector3(0, 2f, -4f);
    public float sensibilidadX = 150f;
    public float sensibilidadY = 100f;
    public float minY = -35f;
    public float maxY = 60f;

    private float rotY;
    private float rotX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 euler = transform.rotation.eulerAngles;
        rotY = euler.y;
        rotX = euler.x;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        Quaternion rotacion = Quaternion.Euler(rotX, rotY, 0f);

        Vector3 posicionDeseada = player.position + rotacion * offset;
        transform.position = posicionDeseada;

        transform.LookAt(player.position + Vector3.up * 1.5f);

        Vector3 direccionJugador = new Vector3(transform.forward.x, 0f, transform.forward.z);
        player.forward = direccionJugador;
    }
}
