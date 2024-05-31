using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private UIPanel titlePanel;
    [SerializeField] private UIPanel menuPanel;

    private List<UIPanel> uIPanels = new List<UIPanel>();

    private void Awake()
    {
        titlePanel.Initialize();
        uIPanels.Add(titlePanel);
        menuPanel.Initialize();
        uIPanels.Add(menuPanel);
    }

    private void Start()
    {
        ActivatePanel(titlePanel, true);
    }

    public void StartMenu()
    {
        ActivatePanel(menuPanel);
    }

    public void OpenGamesPanel()
    {
        Debug.Log("OpenGamesPanel");
    }

    private void ActivatePanel(UIPanel panel, bool force = false)
    {
        foreach (var p in uIPanels)
        {
            if (p == panel)
            {
                p.Activate();
            }
            else
            {                
                if (force)
                {
                    p.ForceDeatcivate();
                }
                else
                {
                    p.Deactivate();
                }
            }
        }
    }
}
