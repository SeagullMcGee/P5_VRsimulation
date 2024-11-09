using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnSelectXR : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab; // Prefab to instantiate on select
    [SerializeField] private Canvas targetCanvas; // Target canvas for instantiation
    [SerializeField] private Vector3 panelOffset = new Vector3(0, 0, 0); // Offset within the canvas space
    private GameObject instantiatedPanel; // Track the instantiated panel

    /// <summary>
    /// Instantiates the panel within the specified Canvas.
    /// </summary>
    public void InstantiatePanel()
    {
        Debug.Log("InstantiatePanel called");  // Add this line to confirm the method is triggered

        if (instantiatedPanel == null)
        {
            instantiatedPanel = Instantiate(panelPrefab, targetCanvas.transform);
            instantiatedPanel.GetComponent<RectTransform>().localPosition = panelOffset;

            Debug.Log("Panel instantiated within canvas on cube selection.");
        }
        else
        {
            Debug.LogWarning("Panel is already instantiated.");
        }
    }

    /// <summary>
    /// Optional method to destroy the panel.
    /// </summary>
    public void DestroyPanel()
    {
        if (instantiatedPanel != null)
        {
            Destroy(instantiatedPanel);
            instantiatedPanel = null;
            Debug.Log("Panel destroyed.");
        }
        else
        {
            Debug.LogWarning("No panel to destroy.");
        }
    }
}
