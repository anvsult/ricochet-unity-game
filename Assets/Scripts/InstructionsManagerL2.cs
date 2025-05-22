using System.Collections;
using TMPro;
using UnityEngine;

public class InstructionsManagerL2 : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    private CanvasGroup instructionsUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructionsUI = gameObject.GetComponent<CanvasGroup>();
        instructionsUI.alpha = 0f;
        instructionsUI.interactable = false;
        instructionsUI.blocksRaycasts = false;
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {

        float fadeDuration = 1f;
        float visibleDuration = 2f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            instructionsUI.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        // Fade Out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            instructionsUI.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // brief pause between messages
    }

}
