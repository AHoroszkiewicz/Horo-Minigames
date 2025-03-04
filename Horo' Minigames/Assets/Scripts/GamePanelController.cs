using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePanelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIPanel gamePanel;
    [SerializeField] private TextMeshProUGUI gameTitleTxt;
    [SerializeField] private TextMeshProUGUI gameDscTxt;

    [Header("Animation Values")]
    [SerializeField] private float animationTime;
    [SerializeField] private float selectedScale;
    [SerializeField] private float deselectedScale;
    [SerializeField] private float hiddenScale;

    private UIButton[] buttons => gamePanel.Buttons;
    private int selectedButtonId => gamePanel.SelectedButtonId;
    private Tween titleTween;
    private Tween dscTween;

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

    public void UpdateGameInfo(string title, string dsc, int titleSize, int dscSize)
    {
        UpdateTitle(title, titleSize);
        UpdateDsc(dsc, dscSize);
    }

    public void SelectGame(string sceneName)
    {
        //TODO add scene manager with more functionality
        try { SceneManager.LoadScene(sceneName); }
        catch { Debug.LogError("Scene not found"); }
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
        foreach(var a in buttons)
        {
            a.Button.enabled = false;
        }
        button.transform.DOScale(deselectedScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2 + 500, animationTime).OnComplete(() =>
        {
            foreach (var a in buttons)
            {
                a.Button.enabled = true;
            }
        });
    }

    private void SetPreviousGame(Button button)
    {
        foreach (var a in buttons)
        {
            a.Button.enabled = false;
        }
        button.transform.DOScale(deselectedScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2 - 500, animationTime).OnComplete(() =>
        {
            foreach (var a in buttons)
            {
                a.Button.enabled = true;
            }
        });
    }

    private void SetHiddenGame(Button button)
    {
        button.transform.SetAsFirstSibling();
        button.transform.DOScale(hiddenScale, animationTime);
        button.transform.DOMoveX(Screen.width / 2, animationTime);
    }

    private void UpdateTitle(string title, int size)
    {
        titleTween?.Kill();
        string text = gameTitleTxt.text;    

        titleTween = DOTween.To(() => text.Length, x => text = text.Substring(0, x), 0, animationTime / 2)
            .OnUpdate(() => gameTitleTxt.text = text)
            .OnComplete(() =>
            {
                if (size == 0)
                {
                    gameTitleTxt.enableAutoSizing = true;
                }
                else
                {
                    gameTitleTxt.enableAutoSizing = false;
                    gameTitleTxt.fontSize = size;
                }
                titleTween = DOTween.To(() => text.Length, x => text = title.Substring(0, x), title.Length, animationTime / 2)
                    .OnUpdate(() => gameTitleTxt.text = text);
            });
    }


    private void UpdateDsc(string dsc, int size)
    {
        dscTween?.Kill();
        string text = gameDscTxt.text;

        dscTween = DOTween.To(() => text.Length, x => text = text.Substring(0, x), 0, animationTime / 2)
            .OnUpdate(() => gameDscTxt.text = text)
            .OnComplete(() =>
            {
                if (size == 0)
                {
                    gameDscTxt.enableAutoSizing = true;
                }
                else
                {
                    gameDscTxt.enableAutoSizing = false;
                    gameDscTxt.fontSize = size;
                }
                dscTween = DOTween.To(() => text.Length, x => text = dsc.Substring(0, x), dsc.Length, animationTime / 2)
                    .OnUpdate(() => gameDscTxt.text = text);
            });
    }
}
