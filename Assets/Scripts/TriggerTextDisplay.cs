using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTextDisplay : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TMP_Text>().SetText(text);
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("ShowText");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetTrigger("HideText");
    }
}
