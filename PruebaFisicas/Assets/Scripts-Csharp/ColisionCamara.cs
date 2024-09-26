using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ColisionCamara : MonoBehaviour
{
    //aca solo se dibujan los parametros del método (method) OnDrawGizmos
    public float x = 10f;
    public float y = 10f;
    public float z = 10f;

    
    public string prefabTag = "MyPrefabTag";    //REFERENCIA a la etiqueta "MyPrefabTag"
    public float aumentarDrag = 5f;   //Cantidad por la que aumentara el drag cuando colisionemos con un gameobject con "MyPrefabTag"
    //*cambiar referencia
    private RandomObjectSpawner randomObjectSpawnerScript; //REFERENCIA al script randomObjectSpawner del gameobject SpawnerContenedor (siempre se debe especificar el script especifico del gameobject especifico que queremos obtener, buscamos el gameobject mas adelante)



    void Start()
    {   

        GameObject spawnerContenedorObj = GameObject.Find("SpawnerContenedor");    // Encuentra el gameobject "SpawnerContenedor" en la escena, lo REFERENCIA como spawnerObject

        if (spawnerContenedorObj != null)
        {
            randomObjectSpawnerScript = spawnerContenedorObj.GetComponent<RandomObjectSpawner>(); //Obtiene el script RandomObjectSpawner (-) del GameObject "SpawnerContenedor" (spawnerContenedor).

            if (randomObjectSpawnerScript == null)
            {
                Debug.LogError("No se encontró el script RandomObjectSpawner en el GameObject 'SpawnerContenedor'.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el GameObject con el nombre 'SpawnerContenedor'.");
        }
    }

    //"Collider collider" es un objeto con el cual queremos colisonar (especificamos cual és mas adelante)
    void OnTriggerEnter(Collider collider) 
    {        
        //Se inicia si la tag del gameobject collider es "MyPrefabTag"
        if (collider.gameObject.tag == prefabTag)
        {
            print("ENTER");

            // medida de seguridad por si no encuentra el script
            if (randomObjectSpawnerScript != null)
            {
                
                List<Rigidbody> rigidbodies = randomObjectSpawnerScript.GetInstantiatedRigidbodies(); // Obtiene lista de Rigidbody instanciados en el script randomObjectSpawner, los REFERENCIA como rigidbodies

                //"para cada uno" de los Rigidbody (rb) en la lista rigidbodies:
                foreach (Rigidbody rb in rigidbodies)
                {
                    
                    if (rb != null)
                    {
                        rb.drag += aumentarDrag; // AUMENTA EL DRAG (dragIncreaseAmount)
                    }
                }

            } else {Debug.LogError("No se encontró el script RandomObjectSpawner en el GameObject 'SpawnerContenedor'.");}
        }
    }

    /*private void OnDrawGizmos()
    {
       // GameObject colisionador = GameObject.Find("Colisionador");

        //BoxCollider boxCollider = colisionador.GetComponent<BoxCollider>();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        //new Vector3(x, y, z)
    }*/
}