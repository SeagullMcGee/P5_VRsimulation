using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelInstantiator : MonoBehaviour
{
    // Reference to the PanelManager to manage the panels
    private PanelManagerXR PanelManagerXR;

    // Index of the panel to be shown when interacting with the cube
    public int panelIndex;

    private void Start()
    {
        // Find the PanelManager in the scene
        PanelManagerXR = FindObjectOfType<PanelManagerXR>();
        if (PanelManagerXR == null)
        {
            Debug.LogError("PanelManager not found in the scene. Please ensure it is added.");
        }
    }

    // This method is called when the object is interacted with
    public void OnInteract()
    {
        if (PanelManagerXR != null)
        {
            // Show the specified panel using the PanelManager
            PanelManagerXR.ShowPanel(0);
        }
    }
}