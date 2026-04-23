using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Configuración")]
    public bool hasKey = false;
    public Animator doorAnimator;
    public string openAnimationName = "AbrirPuertaAnimation";
    public AudioClip openSound;

    private bool isOpen = false;
    private BoxCollider doorCollider;

    void Start()
    {
        doorCollider = GetComponent<BoxCollider>(); // Cachea el collider
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && hasKey && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("Open", true); // Activa la animación
            isOpen = true;
            
            // Sonido
            if (openSound != null)
            {
                AudioSource.PlayClipAtPoint(openSound, transform.position);
            }

            // Collider
            if (doorCollider != null) 
            {
                doorCollider.enabled = false; // Desactiva solo el collider de trigger
            }
        }
        else
        {
            Debug.LogError("¡Animator no asignado en el Inspector!");
        }
    }
}
