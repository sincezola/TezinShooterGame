using System.Collections;
using TMPro;
using UnityEngine;

public class RoundTXTFixed : MonoBehaviour {
    private RoundTXTAnim roundAnim;
    private int roundNum;
    private int oldRoundNum;

    [Header("Round Fixed Text")]
    public TextMeshProUGUI roundTxt;

    [Header("Delay Time")]
    public float delayTime = 3f;

    private void Start() {
        roundAnim = FindObjectOfType<RoundTXTAnim>();
        oldRoundNum = 1;
        roundTxt.text = oldRoundNum.ToString();
    }

    private void Update() {
        roundNum = roundAnim.getRoundNumber();

        if (oldRoundNum != roundNum) {
            StartCoroutine(UpdateRoundTextWithDelay());
        }
    }

    private IEnumerator UpdateRoundTextWithDelay() {
        yield return new WaitForSeconds(delayTime);
        roundTxt.text = roundNum.ToString();
        oldRoundNum = roundNum;
    }
}
