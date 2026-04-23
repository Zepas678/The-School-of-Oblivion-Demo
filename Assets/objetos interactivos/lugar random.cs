using UnityEngine;
using System.Collections.Generic;

public class LugarRandom : MonoBehaviour  
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform position;
        public Vector3 rotation = Vector3.zero;
        public bool useRandomRotation = false;
        [Range(0, 45)] public float rotationVariation = 15f;
    }

    [Header("Configuración")]
    public GameObject objetoPrefab;
    public List<SpawnPoint> puntosSpawn = new List<SpawnPoint>();
    public int maxObjetos = 1;

    private List<SpawnPoint> puntosUsados = new List<SpawnPoint>();

    void Start()
    {
        GenerarObjetos();
    }

    void GenerarObjetos()
    {
        // Validaciones básicas
        if (puntosSpawn.Count == 0 || objetoPrefab == null)
        {
            Debug.LogError("Configuración incompleta: Asigna puntos y prefab");
            return;
        }

        // Filtra puntos disponibles
        List<SpawnPoint> puntosDisponibles = new List<SpawnPoint>();
        foreach (var punto in puntosSpawn)
        {
            if (!puntosUsados.Contains(punto))
                puntosDisponibles.Add(punto);
        }

        // Reinicia si no hay puntos libres
        if (puntosDisponibles.Count == 0 && puntosUsados.Count > 0)
        {
            Debug.Log("Reiniciando puntos de spawn...");
            puntosUsados.Clear();
            puntosDisponibles = new List<SpawnPoint>(puntosSpawn);
        }

        // Genera objetos
        for (int i = 0; i < maxObjetos && i < puntosDisponibles.Count; i++)
        {
            SpawnPoint puntoElegido = puntosDisponibles[Random.Range(0, puntosDisponibles.Count)];
            CrearObjeto(puntoElegido);
            puntosUsados.Add(puntoElegido);
        }
    }

    void CrearObjeto(SpawnPoint punto)
    {
        Vector3 rotacionFinal = punto.rotation;

        // Añade aleatoriedad 
        if (punto.useRandomRotation)
        {
            rotacionFinal.x += Random.Range(-punto.rotationVariation, punto.rotationVariation);
            rotacionFinal.y += Random.Range(-punto.rotationVariation, punto.rotationVariation);
            rotacionFinal.z += Random.Range(-punto.rotationVariation, punto.rotationVariation);
        }

        Instantiate(
            objetoPrefab,
            punto.position.position,
            Quaternion.Euler(rotacionFinal)
        );

        Debug.Log($"Objeto generado en {punto.position.name} " + 
                 $"con rotación: {rotacionFinal}");
    }

    // Visualización en el Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        foreach (var punto in puntosSpawn)
        {
            if (punto.position != null)
            {
                Gizmos.DrawSphere(punto.position.position, 0.1f);
                Gizmos.DrawLine(
                    punto.position.position,
                    punto.position.position + punto.position.forward * 0.5f
                );
            }
        }
    }
}