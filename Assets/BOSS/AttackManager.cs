using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public List<Action> attacks = new List<Action>();
    List<GameObject> prefabList = new List<GameObject>();

    int currentIndex = 0;
    bool isAttacking;

    MenuNavigation playerMenu;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Transform spawner;

    private void Start()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        
        attacks.Add(Attack1);
        attacks.Add(Attack2);

        attacks[currentIndex].Invoke();
    }

    void Attack1()
    {
        isAttacking = true;
        
        for (int i = 0; i < 5; i++)
        {
            float defaultY = spawner.position.y;
            
            GameObject obj = Instantiate(cubePrefab, new Vector2(spawner.position.x,
                UnityEngine.Random.Range(defaultY-1, defaultY+1)), Quaternion.identity);

            prefabList.Add(obj);
        }

        StartCoroutine(StopAttackAfterCooldown(5));
    }

    void Attack2() 
    {
        isAttacking = true;

        Transform spawnPoint = GameObject.Find("Player Default Position").transform;

        GameObject obj = 
            Instantiate(cubePrefab, new Vector2(spawnPoint.position.x + 2, spawnPoint.position.y), Quaternion.identity);

        obj.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void LaunchNextAttack()
    {
        if (isAttacking) return; 
        
        if (attacks[currentIndex+1] != null) currentIndex++;
        attacks[currentIndex].Invoke();
    }

    IEnumerator StopAttackAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        
        foreach(GameObject obj in prefabList.ToArray())
        {
            prefabList.Remove(obj);

            Destroy(obj);
        }

        isAttacking = false;

        playerMenu.ChangeToMenu();
    }
}
