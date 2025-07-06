using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
// Pour etre honnete, j'ai regarde la video de Brackeys sur l'Object Pooling et j'ai modifie quelques trucs ^^
// https://youtu.be/tdSmKaJvCoA?si=FTKi5NCXDZWDurYP
{
    // The class representing a pool for each object we want to spawn
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public List<GameObject> prefab;
        public int size;
    }


    #region Singleton
    public static ObjectPooler instance;
    private void Awake()
    {
        if (instance != null) return;

        instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {   
        // Adding each objects in every pool to a queue, and adding this queue to our poolDictionary
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                int index = Random.Range(0, pool.prefab.Count);
                GameObject prefab = pool.prefab[index];

                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation) 
    {
        // Method for spawning objects from a pool based on the tag. The object is returned afterwards

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag : {tag} doesn't exist!");
            return null;
        }

        GameObject toSpawn = poolDictionary[tag].Dequeue();

        // Enable pooled object
        toSpawn.SetActive(true);
        toSpawn.transform.position = position;
        toSpawn.transform.rotation = rotation;

        // Trigger methods from the interface
        IPooledObject pooledInterface = toSpawn.GetComponent<IPooledObject>();

        if (pooledInterface != null) pooledInterface.OnObjectSpawned();

        // Re adds the dequeued object to the queue
        poolDictionary[tag].Enqueue(toSpawn);

        return toSpawn;
    }
}
