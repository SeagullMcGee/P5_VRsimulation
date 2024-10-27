using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerXR : MonoBehaviour
{
    [SerializeField] private GameObject[] panelPrefabs;  // Array of panel prefabs to instantiate
    [SerializeField] private float fadeDuration = 0.5f;  // Duration for fading in/out panels

    private GameObject currentPanel;  // Track the currently active panel
    private CanvasGroup currentCanvasGroup;  // CanvasGroup of the current panel for fading

    /// <summary>
    /// Shows a panel with a fade-in effect based on the chosen index.
    /// </summary>
    /// <param name="panelIndex">Index of the panel prefab to instantiate</param>
    public void ShowPanel(int panelIndex)
    {
        Debug.Log("NOT WORKING");
        
        // If there is a panel currently displayed, fade it out first
        if (currentPanel != null)
        {
            StartCoroutine(FadeOutAndDestroy(currentPanel, currentCanvasGroup));
        }

        // Instantiate the selected panel prefab if the index is within bounds
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Length)
        {
            currentPanel = Instantiate(panelPrefabs[panelIndex], transform);
            currentCanvasGroup = currentPanel.GetComponent<CanvasGroup>();

            // Ensure a CanvasGroup component exists for fading
            if (currentCanvasGroup == null)
            {
                currentCanvasGroup = currentPanel.AddComponent<CanvasGroup>();
            }

            // Start fade-in effect
            StartCoroutine(FadeIn(currentCanvasGroup));
        }
        else
        {
            Debug.LogWarning("Panel index out of bounds!");
        }
    }

    /// <summary>
    /// Hides the currently active panel with a fade-out effect.
    /// </summary>
    public void HidePanel()
    {
        if (currentPanel != null)
        {
            StartCoroutine(FadeOutAndDestroy(currentPanel, currentCanvasGroup));
        }
    }

    /// <summary>
    /// Coroutine to fade in the CanvasGroup.
    /// </summary>
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 0f;  // Start with transparent
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine to fade out the CanvasGroup and destroy the panel afterwards.
    /// </summary>
    private IEnumerator FadeOutAndDestroy(GameObject panel, CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }

        // Destroy the panel after fade out
        Destroy(panel);
        currentPanel = null;
        currentCanvasGroup = null;
    }
}


