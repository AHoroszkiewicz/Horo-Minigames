using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimations : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float selectedScale;
    [SerializeField] private float deSelectedScale;
    [SerializeField] private float animationTime;


    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(selectedScale, animationTime);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(deSelectedScale, animationTime);
    }
}
