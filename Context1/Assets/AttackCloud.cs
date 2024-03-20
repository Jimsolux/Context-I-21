using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCloud : MonoBehaviour
{

    public Animator animator;
    public Transform offset;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayAttackAnimation();
    }

    private void Update()
    {
        Destroy(gameObject, .7f);    // 1 sec?
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.SetActive(false);  //kill
        }
    }

    private IEnumerator AnimatorPlayOnce(string type)
    {
        try
        {
            animator.SetBool(type, true);
        }
        catch { }
        yield return new WaitForSeconds(0.1f);
        try
        {
            animator.SetBool(type, false);
        }
        catch { }
    }
}
