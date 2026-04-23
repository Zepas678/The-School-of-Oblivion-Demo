using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    [SerializeField] private string nombreDeLaEscenaACargar;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            // Carga la escena especificada
            SceneManager.LoadScene("acto 2");
        }
    }
}
