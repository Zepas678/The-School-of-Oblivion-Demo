using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [Header("Configuración")]
    public string targetDoorTag = "Puerta"; // Etiqueta de la puerta objetivo
    public AudioClip pickupSound; // Sonido al recolectar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca todas las puertas con la etiqueta especificada
            GameObject[] doors = GameObject.FindGameObjectsWithTag(targetDoorTag);
            
            foreach (GameObject door in doors)
            {
                // Activa el componente de puerta 
                DoorController doorController = door.GetComponent<DoorController>();
                if (doorController != null)
                {
                    doorController.hasKey = true;
                }
            }

            // Reproduce sonido
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            
            Destroy(gameObject);
        }
    }
}