using UnityEngine;

public interface IPooledObject
// Interface custom pour l'object pooling.
{
    // La fonction OnObjectSpawned() est appelee a chaque fois qu'on reactive un GameObject dans la Queue<GameObject>
    // C'est l'equivalent d'un Start()
    void OnObjectSpawned();
}
