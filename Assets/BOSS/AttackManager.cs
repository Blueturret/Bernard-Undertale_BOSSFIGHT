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

    GameObject player;
    CharacterControls playerControls;
    MenuNavigation playerMenu;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Transform spawner;
    Transform spawnPoint;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerControls = player.GetComponent<CharacterControls>();
        playerMenu = player.GetComponent<MenuNavigation>();
        objPooler = ObjectPooler.instance;

        spawnPoint = GameObject.Find("Player Default Position").transform;

        attacks.Add(NullAttack);
        //attacks.Add(Attack1);
        //attacks.Add(Attack2);
        //attacks.Add(Attack3);
        //attacks.Add(Attack4);
    }

    void NullAttack() { } // Empty fonction to prevent error when 'attacks' list is empty

    void Attack1() // Projectiles qui apparaissent en cercle et foncent vers le joueur
    {
        isAttacking = true;

        borderAnimator.SetBool("isSmol", true);

        // Logique de l'attaque

        int projectileNumber = 300;//UnityEngine.Random.Range(4, 11);

        StartCoroutine(AttackWithCooldown(SpawnProjectileInCircle, .8f, projectileNumber));

        StartCoroutine(StopAttackAfterCooldown(5));
    }

    void SpawnProjectileInCircle()
    {
        // Variables
        float circleRadius = 3.5f;

        float angle = UnityEngine.Random.Range(0, 360);
        float x = spawnPoint.position.x + circleRadius * Mathf.Cos(angle);
        float y = spawnPoint.position.y + circleRadius * Mathf.Sin(angle);

        // Logique pour spawn l'objet
        Vector2 position = new Vector2(x, y);

        GameObject obj = ObjectPooler.instance.SpawnFromPool("Obstacle", position, Quaternion.identity);
        attackObjects.Add(obj);
    }

    void Attack2() // Obstacles bleus et oranges qui vont de droite a gauche
    {
        isAttacking = true;

        // Logique de l'attaque 2
        StartCoroutine(AttackWithCooldown(SpawnColoredObstacles, 0.4f, 0, 0));

        StartCoroutine(StopAttackAfterCooldown(10));
    }

    void SpawnColoredObstacles()
    // Fonction pour faire spawn les obstacles bleus et oranges
    {
        Vector2 spawnPosition = new Vector2(spawnPoint.position.x + 10, spawnPoint.position.y);
        GameObject obj = objPooler.SpawnFromPool("ColoredObstacle", spawnPosition, Quaternion.identity);

        attackObjects.Add(obj);
    }
    void Attack3()
    {
        int amount = 2;
        // Spawn les boomerangs
        for(int i=0; i <= amount; i++)
        {
            float circleRadius = 6.5f;

            float angle = UnityEngine.Random.Range(0, 360);
            float x = spawnPoint.position.x + circleRadius * Mathf.Cos(angle);
            float y = spawnPoint.position.y + circleRadius * Mathf.Sin(angle);

            GameObject obj = ObjectPooler.instance.SpawnFromPool("Boomerang", new Vector2(x, y), Quaternion.identity);

            attackObjects.Add(obj);
        }

        StartCoroutine(RandomizePlayerColor());

        StartCoroutine(StopAttackAfterCooldown(10f));
    }

    void Attack4()
    {
        Vector2 spawnPos = new Vector2(spawnPoint.position.x + 9, spawnPoint.position.y - 1.4f);
        ObjectPooler.instance.SpawnFromPool("FloorIsLava", spawnPos, Quaternion.identity);
    }

    public void LaunchNextAttack()
    // Fonction pour lancer la prochaine attaque
    {
        if (isAttacking) return;

        if (currentIndex != attacks.Count - 1) currentIndex++;
        attacks[currentIndex].Invoke();
    }

    //-----------------------------------------------------------------//
    //------------------------- IEnumerators -------------------------//
    //---------------------------------------------------------------//

    IEnumerator RandomizePlayerColor()
    // Aleatoirement changer la couleurs du personnage, apres un laps de temps egalement aleatoire
    {
        int newColor = UnityEngine.Random.Range(0, 2);
        float cooldown = UnityEngine.Random.Range(2.1f, 3f);

        yield return new WaitForSeconds(cooldown);

        playerControls.ChangeState(newColor);

        StartCoroutine(RandomizePlayerColor());
    }

    IEnumerator AttackWithCooldown(Action action, float cooldown, int iterations, int currentIteration=0)
    // Methode generique pour lancer une fonction, avec un cooldown,
    // un nombre 'iterations' de fois (pendant toute la duree de l'attaque si 'iterations' = 0)
    {
        if (iterations != 0 && currentIteration >= iterations)
        {
            StopCoroutine(AttackWithCooldown(action, cooldown, iterations, currentIteration));
        }
        action.Invoke();

        yield return new WaitForSeconds(cooldown);

        currentIteration += 1;
        StartCoroutine(AttackWithCooldown(action, cooldown, iterations, currentIteration));
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
        StopAllCoroutines();
    }
}
