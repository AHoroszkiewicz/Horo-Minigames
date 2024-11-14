using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private UIPanel titlePanel;
    [SerializeField] private UIPanel menuPanel;
    [SerializeField] private UIPanel gamePanel;

    private List<UIPanel> uIPanels = new List<UIPanel>();
    private UIPanel currentPanel;
    private Stack<UIPanel> panelStack = new Stack<UIPanel>();

    private void Awake()
    {
        titlePanel.Initialize();
        uIPanels.Add(titlePanel);
        menuPanel.Initialize();
        uIPanels.Add(menuPanel);
        gamePanel.Initialize();
        uIPanels.Add(gamePanel);
    }

    private void Start()
    {
        ActivatePanel(titlePanel);
    }

    public void OpenMenuPanel()
    {
        ActivatePanel(menuPanel);
    }

    public void OpenGamesPanel()
    {
        ActivatePanel(gamePanel);
    }

    private void ActivatePanel(UIPanel panel)
    {
        {
            if (panel == currentPanel) return;
            if (panelStack.Count > 0)
            {
                UIPanel currentPanel = panelStack.Peek();
                currentPanel.Deactivate();
            }

            panel.Activate();
            panelStack.Push(panel);
            currentPanel = panel;
        }
    }

    public void ClosePanel(InputAction.CallbackContext ctx)
    {
        if (panelStack.Count > 1 && ctx.performed)
        {
            UIPanel activePanel = panelStack.Pop();
            activePanel.Deactivate();
            UIPanel previousPanel = panelStack.Peek();
            previousPanel.Activate();
            currentPanel = previousPanel;
        }
    }

    public void Submit(InputAction.CallbackContext ctx)
    {      
        if (ctx.performed)
        {
            currentPanel.Submit();
        }
    }
}
