using DG.Tweening;
using System;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private InAnimation inAnimation;
    [SerializeField] private OutAnimation outAnimation;
    private CanvasGroup canvasGroup;
    private bool isActive;

    public void Initialize()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
                transform.position = new Vector3(-Screen.width, transform.position.y, transform.position.z);
                transform.DOMoveX(0, inAnimation.duration);
                break;
            case AnimationType.InRight:
                transform.position = new Vector3(Screen.width, transform.position.y, transform.position.z);
                transform.DOMoveX(0, inAnimation.duration);
                break;
            case AnimationType.InTop:
                transform.position = new Vector3(transform.position.x, Screen.height, transform.position.z);
                transform.DOMoveY(0, inAnimation.duration);
                break;
            case AnimationType.InBottom:
                transform.position = new Vector3(transform.position.x, -Screen.height, transform.position.z);
                transform.DOMoveY(0, inAnimation.duration);
                break;
            case AnimationType.InScale:
                transform.localScale = Vector3.zero;
                transform.DOScale(Vector3.one, inAnimation.duration);
                break;
        }
    }

    public void Deactivate()
    {
        switch (outAnimation.animationType)
        {
            case AnimationType.OutLeft:
                transform.DOMoveX(-Screen.width, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutRight:
                transform.DOMoveX(Screen.width, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutTop:
                transform.DOMoveY(Screen.height, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutBottom:
                transform.DOMoveY(-Screen.height, outAnimation.duration).onComplete = ForceDeatcivate;
                break;
            case AnimationType.OutScale:
                transform.DOScale(Vector3.zero, outAnimation.duration).onComplete=ForceDeatcivate;
                break;
        }       
    }

    public void ForceDeatcivate()
    {
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        isActive = false;
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
