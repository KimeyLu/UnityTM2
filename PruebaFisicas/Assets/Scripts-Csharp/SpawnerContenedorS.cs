using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects; // Arreglo de objetos a instanciar
    public float spawnX = 11f;
    public float spawnY = 5f;
    public float spawnZ = 11f;
    public float rangoMinimoX = -10f;
    public float spawnInterval = 1f; // Intervalo de aparición en segundos

    private List<Rigidbody> instantiatedRigidbodies = new List<Rigidbody>();

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true) // Bucle infinito para generar objetos continuamente
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(rangoMinimoX, spawnX), spawnY, spawnZ);
            GameObject newObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);

            // Obtén el Rigidbody del objeto instanciado y agrégalo a la lista
            Rigidbody newRb = newObject.GetComponent<Rigidbody>();
            if (newRb != null)
            {
                instantiatedRigidbodies.Add(newRb);
            }

            yield return new WaitForSeconds(spawnInterval); // Espera antes de instanciar el siguiente
        }
    }

    public List<Rigidbody> GetInstantiatedRigidbodies()
    {
        return instantiatedRigidbodies;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(spawnX, spawnY, spawnZ), new Vector3(2f, 2f, 2f));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(rangoMinimoX, spawnY, spawnZ), new Vector3(spawnX, spawnY, spawnZ));
    }
}
