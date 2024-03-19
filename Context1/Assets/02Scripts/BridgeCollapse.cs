using System.Collections;
using UnityEngine;

public class BridgeCollapse : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private MeshCollider coll;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    bool playerEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!playerEntered)
            {
                playerEntered = true;
                animator.SetInteger("State", 1);
            }
            else StartCoroutine(DestroyBridge());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyBridge());
        }
    }

    private IEnumerator DestroyBridge()
    {
        animator.SetInteger("State", 2);
        yield return new WaitForSeconds(0.2f);
        coll.enabled = false;
    }
}
