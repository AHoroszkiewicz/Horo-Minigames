using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] Button button;

    private UIPanel panel;
    private int buttonId;

    public Button Button => button;

    public virtual void Initialize(UIPanel uiPanel, int id)
    {
        panel = uiPanel;
        buttonId = id;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        panel.SelectedButtonId = buttonId;
        panel.SelectedButton = button;
    }
}
