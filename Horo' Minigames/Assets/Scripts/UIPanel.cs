using DG.Tweening;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private InAnimation inAnimation;
    [SerializeField] private OutAnimation outAnimation;
    [SerializeField] private UIButton[] buttons;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isActive;
    private Button selectedButton;
    private int selectedButtonId;

    public UIButton[] Buttons => buttons;
    public int SelectedButtonId {get => selectedButtonId; set => selectedButtonId = value;}
    public Button SelectedButton {get => selectedButton; set => selectedButton = value;}

    public void Initialize()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Initialize(this, i);
            }
        }      
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        isActive = true;
        switch (inAnimation.animationType)
        {
            case AnimationType.InLeft:
                transform.position = new Vector3(-Screen.width*1.5f, transform.position.y, transform.position.z);
                transform.DOMoveX(Screen.width / 2, inAnimation.duration).onComplete = SelectFirstButton;
                break;
            case AnimationType.InRight:
                transform.position = new Vector3(Screen.width* 1.5f, transform.position.y, transform.position.z);
                transform.DOMoveX(Screen.width / 2, inAnimation.duration).onComplete = SelectFirstButton;
                break;
            case AnimationType.InTop:
                transform.position = new Vector3(transform.position.x, Screen.height * 1.5f, transform.position.z);
                transform.DOMoveY(Screen.height / 2, inAnimation.duration).onComplete = SelectFirstButton;
                break;
            case AnimationType.InBottom:
                transform.position = new Vector3(transform.position.x, -Screen.height * 1.5f, transform.position.z);
                transform.DOMoveY(Screen.height / 2, inAnimation.duration).onComplete = SelectFirstButton;
                break;
            case AnimationType.InScale:
                transform.localScale = Vector3.zero;
                transform.position = new Vector3(Screen.width / 2, Screen.height / 2, transform.position.z);
                transform.DOScale(Vector3.one, inAnimation.duration).onComplete = SelectFirstButton;
                break;
        }       
    }

    public void Deactivate()
    {
        isActive = false;
        switch (outAnimation.animationType)
        {
            case AnimationType.OutLeft:
                transform.DOMoveX(-Screen.width* 1.5f, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutRight:
                transform.DOMoveX(Screen.width * 1.5f, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutTop:
                transform.DOMoveY(Screen.height* 1.5f, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutBottom:
                transform.DOMoveY(-Screen.height * 1.5f, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutScale:
                transform.DOScale(Vector3.zero, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
        }
    }

    private void SelectFirstButton()
    {
        if (selectedButton != null) selectedButton.Select();
        else if (buttons.Length > 0)
        {
            selectedButton = buttons[0].Button;
            selectedButtonId = 0;
            selectedButton.Select();
        }      
    }

    public void ForceDeatcivate()
    {
        isActive = false;
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void Submit()
    {
        if (!isActive || selectedButton == null) return;
        selectedButton.onClick.Invoke();
    }

    public enum AnimationType
    {
        None,
        InLeft,
        InRight,
        InTop,
        InBottom,
        InScale,
        OutLeft,
        OutRight,
        OutTop,
        OutBottom,
        OutScale
    }

    [Serializable]
    class InAnimation
    {
        public AnimationType animationType;
        public float duration;
    }
    [Serializable]
    class OutAnimation
    {
        public AnimationType animationType;
        public float duration;
    }
}
