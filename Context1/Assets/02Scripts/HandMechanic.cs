using System.Collections;
using UnityEngine;

public class HandMechanic : MonoBehaviour
{
    [SerializeField] private bool dangerous;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Dangerous", dangerous);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && dangerous)
        {
            StartCoroutine(CrushPlayer(other.GetComponent<PlayerSystem>()));
        }
    }

    private IEnumerator CrushPlayer(PlayerSystem player)
    {
        animator.SetBool("Closed", true);
        yield return new WaitForSeconds(0.2f);
        player.Die();
        animator.SetBool("Closed", false);
    }
}
