using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastMira : MonoBehaviour
{
    public Camera playerCamera; // Asigna la cámara del jugador
    public float rayDistance = 100f; // Distancia máxima del raycast
    public LayerMask collisionLayer; // Capa de colisión para filtrar los objetos
    
    public string prefabTag = "MyPrefabTag"; // La etiqueta del prefab
    private bool ColisionValida = false;
    private GameObject objetoColisionado; // Referencia al objeto colisionado

    void Update()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera no está asignado. Arrastra la cámara del jugador al campo correspondiente.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red); // Dibuja el rayo en la escena

        if (Physics.Raycast(ray, out hit, rayDistance, collisionLayer))
        {
            // Verifica si el objeto colisionado tiene la etiqueta específica
            if (hit.collider.CompareTag(prefabTag))
            {
                if (!ColisionValida) {
                    // Si es la primera vez que se detecta un objeto válido, almacenamos la referencia
                    objetoColisionado = hit.collider.gameObject;
                    ColisionValida = true;
                    Debug.Log("¡Raycast colisionó con el prefab específico! (¡haz clic en él!)");
                }
            }
            else
            {
                ColisionValida = false;
                objetoColisionado = null; // Limpiar referencia si el objeto no tiene la etiqueta esperada
            }

            if (Input.GetMouseButtonDown(0)) {
                if (ColisionValida && objetoColisionado != null) {
                    Debug.Log("¡Objeto clickeado! Rotando el objeto.");
                    RotarObjeto(objetoColisionado);
                    ColisionValida = false; // Desactivar la rotación hasta el siguiente clic
                }
            }
        }
    }

    private void RotarObjeto(GameObject objeto)
    {
        // Aplicar rotación de 45 grados en el eje X al objeto
        objeto.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}

