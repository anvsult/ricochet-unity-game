using UnityEngine;
using TMPro;
using System.Collections;


public class InstructionsManager : MonoBehaviour
{
    public TextMeshProUGUI greetingUIText;
    public CanvasGroup greetingUI;
    public PlayerMovement playerMovement;
    public Gun gun;
    public GameObject instructionsLegend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // playerMovement.enabled = false;
        // gun.enabled = false;
        instructionsLegend.SetActive(false);

        greetingUI.alpha = 0f;
        greetingUI.interactable = false;
        greetingUI.blocksRaycasts = false;
        StartCoroutine(FadeInAndOut());
    }

    // Update is called once per frame
    IEnumerator FadeInAndOut()
    {
        // playerMovement.enabled = false;
        // gun.enabled = false;

        string[] instructions = new string[]
        {
            "THIS IS RICOCHET",
            "Shooting straight on the enemy does nothing",
            "Make bullets ricochet to deal damage"
        };

        float fadeDuration = 1f;
        float visibleDuration = 2f;

        foreach (string message in instructions)
        {
            greetingUIText.text = message;

            // Fade In
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                greetingUI.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }

            yield return new WaitForSeconds(visibleDuration);

            // Fade Out
            t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                greetingUI.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f); // brief pause between messages
        }

        playerMovement.enabled = true;
        gun.enabled = true;
        instructionsLegend.SetActive(true);
    }
}
