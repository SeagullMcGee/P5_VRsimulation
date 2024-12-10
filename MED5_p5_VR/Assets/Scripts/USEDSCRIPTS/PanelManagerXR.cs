using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagerXR : MonoBehaviour
{
    [SerializeField] private GameObject[] panelPrefabs;  // Array of panel prefabs to instantiate
    [SerializeField] private float fadeDuration = 0.5f;  // Duration for fading in/out panels
    [SerializeField] private Canvas targetCanvas;        // Target canvas for instantiation

    private GameObject currentPanel;  // Track the currently active panel
    private CanvasGroup currentCanvasGroup;  // CanvasGroup of the current panel for fading

    public void ShowPanel(int panelIndex)
    {
        // Ensure the game object is active before starting coroutines
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        Debug.Log("ShowPanel called with index: " + panelIndex);
        StartCoroutine(SwitchPanel(panelIndex));
    }

    private IEnumerator SwitchPanel(int panelIndex)
    {
        Debug.Log("SwitchPanel called with index: " + panelIndex);

        // If there’s a current panel, fade it out and destroy it with a slight delay
        if (currentPanel != null)
        {
            Debug.Log("Fading out and destroying current panel.");
            yield return new WaitForSeconds(0.1f);  // Small delay to allow button click to complete
            yield return StartCoroutine(FadeOutAndDestroy(currentPanel, currentCanvasGroup));
        }

        // Instantiate the new panel if the index is valid
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Length)
        {
            Debug.Log("Instantiating panel at index: " + panelIndex);
            currentPanel = Instantiate(panelPrefabs[panelIndex], targetCanvas.transform);

            if (currentPanel == null)
            {
                Debug.LogError("Panel instantiation failed.");
                yield break;
            }

            Debug.Log("Panel instantiated successfully.");
            currentCanvasGroup = currentPanel.GetComponent<CanvasGroup>();

            if (currentCanvasGroup == null)
            {
                Debug.Log("CanvasGroup not found, adding CanvasGroup component.");
                currentCanvasGroup = currentPanel.AddComponent<CanvasGroup>();
            }

            // Prepare CanvasGroup for fade-in
            currentCanvasGroup.alpha = 0f;  // Start with transparent
            currentCanvasGroup.interactable = true;
            currentCanvasGroup.blocksRaycasts = true;

            // Set up button listeners for the new panel
            SetupPanelButtons(currentPanel);

            // Start fade-in effect
            yield return StartCoroutine(FadeIn(currentCanvasGroup));
        }
        else
        {
            Debug.LogWarning("Panel index out of bounds!");
        }
    }
    private void SetupPanelButtons(GameObject panel)
    {
        Button[] buttons = panel.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            // Assumes each button has a specific index to load the next panel
            int panelIndex = button.GetComponent<PanelButton>().panelIndex;
            button.onClick.RemoveAllListeners();  // Clear any existing listeners
            button.onClick.AddListener(() => ShowPanel(panelIndex));
        }
    }

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

        Destroy(panel);
        currentPanel = null;
        currentCanvasGroup = null;

        Debug.Log("FadeOut completed and panel destroyed");
    }
}
