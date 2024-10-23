using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PanelManagerAI panelManagerAI;

    private void OnMouseDown()
    {
        if (panelManagerAI != null)
        { 
            panelManagerAI.InstantiatePanel();
        }
        else
        {
            Debug.LogError("PanelManager reference is missing");
        }

    }
}
