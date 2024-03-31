using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTXTAnim : MonoBehaviour
{   
    private Animator anim;
    private EnemySpawner enemySp;

    [Header("Texto Round")]
    public TextMeshProUGUI RoundTXT;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemySp = FindObjectOfType<EnemySpawner>();
    }

    public void AnimateRoundTXT(int animNumber)
    {   
        if(animNumber == 0)
        {
            anim.SetTrigger("backTxtAnim");
        }

        else if(animNumber == 1)
        {
            anim.SetTrigger("txtAnim");
        }

        Debug.Log("Triggei animação do Texto");
    }

    public IEnumerator FirstEnemySpawn()
    {
        yield return new WaitForSeconds(2.0f);

        enemySp.InstanciarInimigos(1);
    }

    public IEnumerator changeRoundTXT()
    {
        AnimateRoundTXT(0);

        yield return new WaitForSeconds(3f);

        RoundTXT.text = "Round " + enemySp.Round;

        yield return new WaitForSeconds(1f);

        AnimateRoundTXT(1);

        yield return new WaitForSeconds(2.2f);

        enemySp.InstanciarInimigos(enemySp.Round);
    }
}
