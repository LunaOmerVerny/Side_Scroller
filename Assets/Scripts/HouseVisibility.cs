using System.Collections;  // ← nécessaire pour IEnumerator
using UnityEngine;

public class HouseVisibility : MonoBehaviour
{
    [Header("Visuels")]
    public GameObject exterior;        // Toit + murs extérieurs
    public GameObject interior;        // Intérieur (si caché de base)

    [Header("Options")]
    public bool fadeInstead = false;   // true = transparence, false = disparition
    public float fadeAlpha = 0.05f;     // Opacité si on fait un fade
    public float fadeDuration = 0.6f;   // durée du fade
    private Coroutine currentFade;      // pour pas avoir deux fades en même temps

    private SpriteRenderer[] extRenderers;
    private bool playerInside = false;


    void Start()
    {
        extRenderers = exterior.GetComponentsInChildren<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            SetExteriorVisible(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            SetExteriorVisible(true);
        }
    }

    void SetExteriorVisible(bool visible)
    {
        if (fadeInstead)
        {
            float targetAlpha = visible ? 1f : fadeAlpha;
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(Fade(targetAlpha));
        }
        else
        {
            exterior.SetActive(visible);
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = extRenderers[0].color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            float tSmooth = t * t * (3f - 2f * t); // smoothstep
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, tSmooth);

            foreach (var sr in extRenderers)
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
            yield return null;
        }
    }

}