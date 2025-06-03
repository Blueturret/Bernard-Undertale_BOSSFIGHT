using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HandleHealthbarDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Image bossHealthbar;
    [SerializeField] TextMeshProUGUI damageText;
    Animation damageTextAnim;

    private void Awake()
    {
        damageTextAnim = damageText.GetComponent<Animation>();
    }

    public void DisplayHealthbar(float fillAmount, int dmg)
    // Fonction pour afficher et animer la barre de vie du boss
    {
        // Gestion de la barre de vie
        bossHealthbar.transform.parent.gameObject.SetActive(true);
        bossHealthbar.fillAmount = fillAmount;

        // Gestion du texte
        damageText.gameObject.SetActive(true);
        damageText.text = dmg.ToString();
        damageTextAnim.Play();

        StartCoroutine(DisplayCooldown(2));
    }

    IEnumerator DisplayCooldown(float cooldown)
    // Desactive la barre de vie apres n secondes
    {
        yield return new WaitForSeconds(cooldown);

        bossHealthbar.transform.parent.gameObject.SetActive(false);
        damageText.gameObject.SetActive(false);
    }
}
