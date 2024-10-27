using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PanelManagerXR PanelManagerXR;

    public bool firstPanel = false;

    public void OnGrab()
    {
        if (PanelManagerXR != null)
        { 
            firstPanel = true;
            PanelManagerXR.ShowPanel(0);
        }
        else
        {
            Debug.LogError("PanelManager reference is missing");
        }

    }
}
