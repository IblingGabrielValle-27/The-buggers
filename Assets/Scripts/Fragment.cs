using UnityEngine;

public class Fragment : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Vector3 spawnAreaMin = new Vector3(-20, 0.5f, -20);
    [SerializeField] private Vector3 spawnAreaMax = new Vector3(20, 0.5f, 20);
    [SerializeField] private float respawnInterval = 15f;

    [Header("Visual Settings")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.3f;

    private Vector3 startPosition;
    private float bobTimer;
    private float respawnTimer;

    void Start()
    {
        Respawn();
        respawnTimer = respawnInterval;
    }

    void Update()
    {
        // Rotación constante
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Efecto de flotación (bobbing)
        bobTimer += Time.deltaTime * bobSpeed;
        float newY = startPosition.y + Mathf.Sin(bobTimer) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Timer de reposicionamiento
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0)
        {
            Respawn();
            respawnTimer = respawnInterval;
        }
    }

    public void Respawn()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        Vector3 newPosition = new Vector3(randomX, randomY, randomZ);
        transform.position = newPosition;
        startPosition = newPosition;
        bobTimer = 0;

        // Reiniciar timer
        respawnTimer = respawnInterval;
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}