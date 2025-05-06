using System; // System.Delete(C:/Windows/System32) hihihi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
// FONCTIONNEMENT : Chaque attaque est representee par une fonction, qu'on appelle selon certains criteres
{
    public static AttackManager instance { get; private set; } // Singleton

    List<Action> attacks = new List<Action>(); // Liste des differentes attaques
    public static List<GameObject> attackObjects { get; private set; } = new List<GameObject>(); // Liste de tous les objets d'une attaque

    [SerializeField] Animator borderAnimator; // Animator du bord du terrain, avec deux animations pour le rétrécir et le remettre par défaut

    GameObject player;
    CharacterControls playerControls;
    MenuNavigation playerMenu;
    Transform spawnPoint;

    int currentIndex = -1; // L'index de l'attaque qu'on veut effectuer
    bool isAttacking = false;

    private void Awake()
    {
        instance = this; // Singleton

        player = GameObject.Find("Player");
        playerControls = player.GetComponent<CharacterControls>();
        playerMenu = player.GetComponent<MenuNavigation>();

        spawnPoint = GameObject.Find("Player Default Position").transform;

        //attacks.Add(NullAttack);
        //attacks.Add(Attack1);
        //attacks.Add(Attack2);
        //attacks.Add(Attack3);
        attacks.Add(Attack4);
        //attacks.Add(Attack5);
    }

    void NullAttack() {} // Fonction vide pour empêcher d'avoir des erreurs quand la liste 'attacks' est vide

    void Attack1() // Projectiles qui apparaissent en cercle et foncent vers le joueur
    {
        isAttacking = true;

        borderAnimator.SetBool("isSmol", true);

        // Logique de l'attaque

        int projectileNumber = 300;//UnityEngine.Random.Range(4, 11);

        StartCoroutine(AttackWithCooldown(SpawnProjectileInCircle, .5f, projectileNumber));

        StartCoroutine(StopAttackAfterCooldown(10));
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
        GameObject obj = ObjectPooler.instance.SpawnFromPool("ColoredObstacle", spawnPosition, Quaternion.identity);

        attackObjects.Add(obj);
    }
    
    void Attack3() // Boomerangs
    {
        // Spawn une plateforme au milieu de l'ecran
        Vector2 platformSpawn = new Vector2(spawnPoint.position.x, spawnPoint.position.y - .3f);
        GameObject softPlatform = ObjectPooler.instance.SpawnFromPool("SoftPlatform", platformSpawn, Quaternion.identity);
        attackObjects.Add(softPlatform);

        StartCoroutine(RandomizePlayerColor());

        StartCoroutine(AttackWithCooldown(SpawnBoomerangs, 1.5f, 3));

        StartCoroutine(StopAttackAfterCooldown(10f));
    }

    void SpawnBoomerangs()
    {
        // Spawn un boomerang
        float circleRadius = 6f;

        float angle = UnityEngine.Random.Range(0, 360);
        float x = spawnPoint.position.x + circleRadius * Mathf.Cos(angle);
        float y = spawnPoint.position.y + circleRadius * Mathf.Sin(angle);

        GameObject obj = ObjectPooler.instance.SpawnFromPool("Boomerang", new Vector2(x, y), Quaternion.identity);

        attackObjects.Add(obj);
    }

    void Attack4() // Phase de plateformes avec le sol qui fait des degats
    {
        playerControls.ChangeState(1);

        Vector2 spawnPos = new Vector2(0, -2.5f);
        GameObject floor = ObjectPooler.instance.SpawnFromPool("FloorIsLava", spawnPos, Quaternion.identity);
        GameObject movingPlatforms = ObjectPooler.instance.SpawnFromPool("MovingPlatformSequence", Vector3.zero, Quaternion.identity);
        attackObjects.Add(floor);
        attackObjects.Add(movingPlatforms);

        StartCoroutine(StopAttackAfterCooldown(17));
    } 

    void SpawnPlatforms()
    {
        // Spawn la plateforme
        float offset = UnityEngine.Random.Range(-0.8f, 0.12f);
        Vector2 platformSpawn = new Vector2(spawnPoint.position.x + 8, spawnPoint.position.y + offset);

        GameObject softPlatform = ObjectPooler.instance.SpawnFromPool("MovingPlatform", platformSpawn, Quaternion.identity);
        
        attackObjects.Add(softPlatform);
    }

    void Attack5() // Bernard met un coup de baton
    // Cette attaque est majoritairement geree par des animations events et d'autres scripts, on se contente juste
    // de lancer l'animation dans cette fonction
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("mustSwing");

        StartCoroutine(StopAttackAfterCooldown(3f));
    }

    public void LaunchNextAttack()
    // Fonction pour lancer la prochaine attaque
    {
        if (isAttacking) return;

        if (currentIndex != attacks.Count - 1) currentIndex++;
        attacks[currentIndex].Invoke();
    }

    public void StopCurrentAttack()
    // Fonction pour arreter l'/les attaque(s) en cours (faut apprendre a lire l'anglais les mecs/filles/helicopteres/eponges...)
    {
        // Desactive tous les objets a la fin d'une attaque
        foreach (GameObject obj in attackObjects.ToArray())
        {
            obj.SetActive(false);
            attackObjects.Remove(obj);
        }

        isAttacking = false;

        borderAnimator.SetBool("isSmol", false); // Remettre le bord a la bonne taille pour eviter de casser l'UI

        playerMenu.ChangeToMenu();
        StopAllCoroutines();
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
            yield break;
        }
        action.Invoke();

        yield return new WaitForSeconds(cooldown);

        StartCoroutine(AttackWithCooldown(action, cooldown, iterations, currentIteration + 1));
    }

    IEnumerator StopAttackAfterCooldown(float cooldown)
    // Arrete l'attaque apres une certaine duree
    {
        yield return new WaitForSeconds(cooldown);

        StopCurrentAttack();
    }
}
