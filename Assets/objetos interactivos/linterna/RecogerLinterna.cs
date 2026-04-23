using UnityEngine;

public class RecogerLinterna : MonoBehaviour
{
    [Header("Asignaciones en Inspector")]
    public Transform mano;
    public GameObject linterna;

    private bool enRango = false;
    private Light luzLinterna;
    private bool linternaEncendida = false; // Inicialmente apagada

    void Start()
    {
        // Buscar la luz en los hijos de la linterna
        luzLinterna = linterna.GetComponentInChildren<Light>();
        if (luzLinterna != null) luzLinterna.enabled = false; // Inicialmente apagada
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🔴 E presionado: Intentando recoger la linterna...");
            Recoger();
        }

        // Encender / Apagar con F (solo si la linterna está en la mano)
        if (linterna != null && linterna.transform.parent == mano && Input.GetKeyDown(KeyCode.F))
        {
            if (luzLinterna != null)
            {
                linternaEncendida = !linternaEncendida;
                luzLinterna.enabled = linternaEncendida;
                Debug.Log("🔦 Linterna " + (linternaEncendida ? "Encendida" : "Apagada"));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Jugador en rango de la linterna");
            enRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("❌ Jugador salió del rango de la linterna");
            enRango = false;
        }
    }

    void Recoger()
    {
        Debug.Log("🟢 Linterna recogida!!!");

        linterna.transform.SetParent(mano);
        linterna.transform.localPosition = Vector3.zero;
        linterna.transform.localRotation = Quaternion.Euler(0f,-92f,0f); // Ajustar según sea necesario

        linterna.GetComponent<Collider>().enabled = false;

        // Encender la luz al recogerla
        if (luzLinterna != null)
        {
            luzLinterna.enabled = true;
            linternaEncendida = true;
            Debug.Log("💡 Luz encendida al recoger."); // Mensaje de verificación
        }
        else
        {
            Debug.LogError("⚠️ No se encontró el componente Light en la linterna al recoger.");
        }
    }
}
