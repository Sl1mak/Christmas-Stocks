using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAnimation : MonoBehaviour
{
    private Button button;

    private Vector3 startScale;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        startScale = transform.localScale;        
    }

    public void PointerEnter()
    {
        if (button.interactable) { button.transform.DOScale(1.1f, 0.2f); }
    }

    public void PointerExit()
    {
        button.transform.DOScale(startScale, 0.2f);
    }
}
