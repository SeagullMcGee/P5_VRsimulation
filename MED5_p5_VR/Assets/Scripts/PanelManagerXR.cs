using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManagerXR : MonoBehaviour
{
    public static PanelManagerXR Instance { get; private set; } // Singleton instance

    public List<PanelModel> Panels; // List of panel models
    private GameObject currentPanel; // Track the currently active panel

    private void Awake()
    {
        // Check if there is already an instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to show a panel by index
    public void ShowPanel(int panelIndex)
    {
        // Validate the panel index
        if (panelIndex < 0 || panelIndex >= Panels.Count)
        {
            Debug.LogWarning("Panel index out of bounds!");
            return;
        }

        Debug.Log("ShowPanel called with index: " + panelIndex);

        // If there is a panel currently displayed, hide it first
        if (currentPanel != null)
        {
            HideCurrentPanel();
        }

        // Instantiate and display the selected panel prefab
        currentPanel = Instantiate(Panels[panelIndex].PanelPrefab, transform);
        currentPanel.transform.localPosition = Vector3.zero; // Center the panel in the canvas

        // Ensure it is fully visible
        CanvasGroup canvasGroup = currentPanel.GetComponent<CanvasGroup>() ?? currentPanel.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;  // Make it fully visible
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        Debug.Log("Panel instantiated successfully.");
    }

    // Method to hide the current panel
    public void HideCurrentPanel()
    {
        if (currentPanel != null)
        {
            Destroy(currentPanel); // Destroys the current panel immediately
            currentPanel = null;   // Reset the current panel reference
            Debug.Log("Current panel hidden.");
        }
    }
}
