using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float saltoFuerza = 7f;
    private CharacterController controller;
    private Vector3 velocidadVertical;
    private bool enSuelo;

    [Header("Cámara")]
    public Transform camara;
    public float sensibilidad = 1f;
    private float rotacionX = 0f;
    public float minVertical = -90f;
    public float maxVertical = 90f;

    [Header("Sonidos")]
    public AudioSource audioSource; 
    public AudioClip pasoClip; // Asigna el sonido de pasos en el Inspector
    private float tiempoPaso = 0f;
    public float intervaloPasos = 0.5f; // Tiempo entre pasos

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Movimiento ---
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento = (transform.right * horizontal + transform.forward * vertical).normalized;
        controller.Move(movimiento * velocidad * Time.deltaTime);

        // --- Reproducir sonido de pasos ---
        if ((horizontal != 0 || vertical != 0) && enSuelo)
        {
            if (Time.time > tiempoPaso)
            {
                ReproducirPaso();
                tiempoPaso = Time.time + intervaloPasos;
            }
        }

        // --- Rotación Cámara ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * 1f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * 1f * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, minVertical, maxVertical);
        camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Desbloquear el cursor con Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void ReproducirPaso()
    {
        if (pasoClip != null && !audioSource.isPlaying) 
        {
            audioSource.PlayOneShot(pasoClip);
        }
    }
}
