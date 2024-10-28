using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerXR : MonoBehaviour
{
    [SerializeField] private GameObject[] panelPrefabs;  // Array of panel prefabs to instantiate
    [SerializeField] private float fadeDuration = 0.5f;  // Duration for fading in/out panels
    [SerializeField] private Canvas targetCanvas; // Target canvas for instantiation

    private GameObject currentPanel;  // Track the currently active panel
    private CanvasGroup currentCanvasGroup;  // CanvasGroup of the current panel for fading

    /// <summary>
    /// Shows a panel with a fade-in effect based on the chosen index.
    /// </summary>
    /// <param name="panelIndex">Index of the panel prefab to instantiate</param>
    /// 

    public void Awake()
    {
        Instantiate(targetCanvas);
    }

    public void ShowPanel(int panelIndex)
    {
        Debug.Log("ShowPanel called with index: " + panelIndex);

        // If there is a panel currently displayed, fade it out first
        if (currentPanel != null)
        {
            StartCoroutine(FadeOutAndDestroy(currentPanel, currentCanvasGroup));
        }

        // Instantiate the selected panel prefab if the index is within bounds
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Length)
        {
            Debug.Log("Instantiating panel at index: " + panelIndex);
            currentPanel = Instantiate(panelPrefabs[panelIndex], targetCanvas.transform);

            // Check if panel instantiated correctly
            if (currentPanel == null)
            {
                Debug.LogError("Panel instantiation failed.");
                return;
            }

            Debug.Log("Panel instantiated successfully.");
            currentCanvasGroup = currentPanel.GetComponent<CanvasGroup>();

            // Ensure a CanvasGroup component exists for fading
            if (currentCanvasGroup == null)
            {
                Debug.Log("CanvasGroup not found, adding CanvasGroup component.");
                currentCanvasGroup = currentPanel.AddComponent<CanvasGroup>();
            }

            // Set up initial CanvasGroup properties for fade-in
            currentCanvasGroup.alpha = 0f;  // Start with transparent
            currentCanvasGroup.interactable = true;
            currentCanvasGroup.blocksRaycasts = true;

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
        Debug.Log("FadeIn started");

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        Debug.Log("FadeIn completed");
    }

    /// <summary>
    /// Coroutine to fade out the CanvasGroup and destroy the panel afterwards.
    /// </summary>
    private IEnumerator FadeOutAndDestroy(GameObject panel, CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        Debug.Log("FadeOut started");

        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                yield return null;
            }
        }

        // Destroy the panel after fade-out
        Destroy(panel);
        currentPanel = null;
        currentCanvasGroup = null;

        Debug.Log("FadeOut completed and panel destroyed");
    }
}
