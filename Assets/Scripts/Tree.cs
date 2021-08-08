using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IProperty
{
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = PlayerController.Instance.GetComponent<Animator>();
    }
    public void Interact()
    {
        playerAnimator.SetBool(Constants.RUN_ANIM, false);
        playerAnimator.SetBool(Constants.CUT_ANIM, true);
        playerAnimator.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerAnimator.gameObject.transform.LookAt(gameObject.transform);
        print("Hit");
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(Constants.CUT_ANIM))
        {
            Invoke(Constants.STOP_CUT_ANIM, 1.5f);
            Destroy(gameObject, 1.5f);
        }

    }

    private void StopCutAnim()
    {
        playerAnimator.SetBool(Constants.CUT_ANIM, false);
    }

   
}
