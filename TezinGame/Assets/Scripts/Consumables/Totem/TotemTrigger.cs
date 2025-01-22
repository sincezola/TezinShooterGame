using System.Collections;
using UnityEngine;

public class TotemTrigger : MonoBehaviour
{
    [Header("Life Settings")]
    public float delayToGainLife = 1.0f;
    public float lifeToGain = 1.0f;

    private GameManager manager;
    private Coroutine lifeCoroutine;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        if (manager == null)
        {
            Debug.LogError("GameManager não encontrado na cena!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (lifeCoroutine == null)
            {
                lifeCoroutine = StartCoroutine(GainLifeCoroutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (lifeCoroutine != null)
            {
                StopCoroutine(lifeCoroutine);
                lifeCoroutine = null;
            }
        }
    }

    private IEnumerator GainLifeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayToGainLife);
            manager.IncreasePlayerHp(lifeToGain);
        }
    }
}
