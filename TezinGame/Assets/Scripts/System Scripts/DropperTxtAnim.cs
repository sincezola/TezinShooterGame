using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperTxtAnim : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimateDropperTxT(int animNum)
    {
        if (animNum == 0)
        {
            anim.SetTrigger("BackDropperAnim");
        }

        else if (animNum == 1)
        {
            anim.SetTrigger("DropperAnim");
        };

        Debug.Log("Triggei animação do Dropper");
    }
}
