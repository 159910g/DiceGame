using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Assuming you're using Unity's UI system

public class HPAnimation : MonoBehaviour
{
    public Slider damageBar; // Reference to the HP bar (Slider UI element)
    public Image damageBarFill;
    public float fadeDuration = 1.0f; // Time for fading the health bar
    public float repeatDelay = 0.5f; // Time between fading to target and returning to current HP

    private float healthLost;
    private float maxHealth;

    float originalAlpha;

    private Color originalColor;

    private  Coroutine fade;
    
    // Called when the attack occurs, passing the damage and health after the attack
    public void FadeHP(float HPafterATK)
    {
        originalColor = damageBarFill.color;
        this.maxHealth = damageBar.maxValue; // Assuming max health is already set on the HP bar
        originalAlpha = 1f;

        StopDmgFade();

        damageBar.value = maxHealth - HPafterATK;
        fade = StartCoroutine(FadeDmgBar());
    }

    public void StopDmgFade()
    {
        if(fade != null)
        {
            StopCoroutine(fade);
            fade = null;
        }
    }

    private IEnumerator FadeDmgBar()
    {
        float elapsedTime = 0f;
        // Fade out the damage fill over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha;
            
            if(originalAlpha == 1)
                alpha = Mathf.Lerp(0f, 1.0f, elapsedTime / fadeDuration);
            else
                alpha = Mathf.Lerp(1.0f, 0f, elapsedTime / fadeDuration);

            damageBarFill.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        if(originalAlpha == 1f)
            originalAlpha = 0;
        else
            originalAlpha = 1;

        fade = StartCoroutine(FadeDmgBar());
    }
}