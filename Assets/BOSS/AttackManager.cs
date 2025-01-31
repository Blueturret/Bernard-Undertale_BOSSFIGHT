using System; // System.Delete(C:/Windows/System32) hihihi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
// FONCTIONNEMENT : Chaque attaque est representee par une fonction, qu'on appelle selon certains criteres
{
    public List<Action> attacks = new List<Action>(); // Liste des differentes attaques
    List<GameObject> attackObjects = new List<GameObject>(); // Liste de tous les objets d'une attaque

    ObjectPooler objPooler; // Object Pooling

    [SerializeField] Animator borderAnimator; // Animator du bord du terrain, avec deux animations pour le rétrécir et le remettre par défaut

    int currentIndex = -1; // L'index de l'attaque qu'on veut effectuer
    bool isAttacking = false;

    MenuNavigation playerMenu;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Transform spawner;

    private void Start()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        objPooler = ObjectPooler.instance;
        
        attacks.Add(Attack1);
        attacks.Add(Attack2);
    }

    void Attack1()
    {
        isAttacking = true;

        borderAnimator.SetBool("isSmol", true);
        
        // Logique de l'attaque 1
        for (int i = 0; i < 5; i++)
        {
            float defaultY = spawner.position.y;

            GameObject obj = objPooler.SpawnFromPool("Obstacle", new Vector2(spawner.position.x,
                UnityEngine.Random.Range(defaultY - 1, defaultY + 1)), Quaternion.identity);

            attackObjects.Add(obj);
        }

        StartCoroutine(StopAttackAfterCooldown(5));
    }

    void Attack2() 
    {
        isAttacking = true;

        // Logique de l'attaque 2
        Transform spawnPoint = GameObject.Find("Player Default Position").transform;

        GameObject obj = objPooler.SpawnFromPool("Obstacle", new Vector2(spawnPoint.position.x + 2, spawnPoint.position.y), Quaternion.identity);

        obj.GetComponent<SpriteRenderer>().color = Color.red;

        attackObjects.Add(obj);

        StartCoroutine(StopAttackAfterCooldown(4));
    }

    public void LaunchNextAttack()
    {
        if (isAttacking) return; 
        
        if (currentIndex != attacks.Count - 1) currentIndex++;
        attacks[currentIndex].Invoke();
    }

    IEnumerator StopAttackAfterCooldown(float cooldown)
    // Arrete l'attaque apres une certaine duree
    {
        yield return new WaitForSeconds(cooldown);
        
        // Desactive tous les objets a la fin d'une attaque
        foreach (GameObject obj in attackObjects.ToArray())
        {
            obj.SetActive(false);
        }

        isAttacking = false;

        borderAnimator.SetBool("isSmol", false); // Remettre le bord a la bonne taille pour eviter de casser l'UI

        playerMenu.ChangeToMenu();
    }
}
