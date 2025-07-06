using UnityEngine;

public class SwingAnimationController : MonoBehaviour
{
    [SerializeField] GameObject swingPrefab;
    [SerializeField] GameObject star;
    int isOrange = 0;

    public void UpdateColor()
    {
        isOrange = Random.Range(0, 2);

        switch (isOrange)
        {
            case 0:
                star.GetComponent<SpriteRenderer>().color = new Color32(252, 166, 0, 255);
                break;
            case 1:
                star.GetComponent<SpriteRenderer>().color = new Color32(66, 252, 255, 255);
                break;
        }

        AudioManager.instance.PlaySound("Telegraphing");
    }
    public void OnSwingAttack()
    {
        GameObject obj = Instantiate(swingPrefab);

        obj.GetComponent<Swing>().isOrange = isOrange;
    }
}
