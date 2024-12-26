using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonScript : MonoBehaviour
{
    public int cost;

    public Text costText;

    private void Start()
    {
        costText.text = cost.ToString();
    }

    public void Unlock(float cost)
    {
        if (PlayerScript.balance >= cost)
        {
            PlayerScript.balance -= cost;
            Destroy(gameObject);
        }
    }
}
