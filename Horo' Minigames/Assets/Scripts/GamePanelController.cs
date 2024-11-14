using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private UIPanel gamePanel;
    [SerializeField] private float animationTime;
    [SerializeField] private float selectedScale;
    [SerializeField] private float deselectedScale;
    [SerializeField] private float hiddenScale;

    private UIButton[] buttons => gamePanel.Buttons;
    private int selectedButtonId => gamePanel.SelectedButtonId;

    private void Awake()
    {
        if (gamePanel == null && gameObject.TryGetComponent(out UIPanel panel))
        {
            gamePanel = panel;
        }
    }

    public void UpdateGameCards()
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedButtonId)
                {
                    SetSelectedGame(buttons[i].Button);
                }
                else if (isNextButtonId(i))
                {
                    SetNextGame(buttons[i].Button);
                }
                else if (isPreviousButtonId(i))
                {
                    SetPreviousGame(buttons[i].Button);
                }
                else
                {
                    SetHiddenGame(buttons[i].Button);
                }
            }
        }
    }

    private bool isNextButtonId(int id)
    {
        if (gamePanel.SelectedButtonId == buttons.Length - 1)
        {
            return id == 0;
        }
        return id == gamePanel.SelectedButtonId + 1;
    }

    private bool isPreviousButtonId(int id)
    {
        if (gamePanel.SelectedButtonId == 0)
        {
            return id == buttons.Length - 1;
        }
        return id == gamePanel.SelectedButtonId - 1;
    }

    private void SetSelectedGame(Button button)
    {
        button.transform.SetAsLastSibling();
        button.transform.DOScale(selectedScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2, animationTime);
    }

    private void SetNextGame(Button button)
    {
        button.transform.DOScale(deselectedScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2 + 500, animationTime);
    }

    private void SetPreviousGame(Button button)
    {
        button.transform.DOScale(deselectedScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2 - 500, animationTime);
    }

    private void SetHiddenGame(Button button)
    {
        button.transform.SetAsFirstSibling();
        button.transform.DOScale(hiddenScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2, animationTime);
    }
}
