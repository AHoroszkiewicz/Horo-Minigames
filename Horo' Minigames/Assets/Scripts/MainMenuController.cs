using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject startHint;
    [SerializeField] private DOTweenAnimation titleAnimation;

    private bool isStarted;

    private void Awake()
    {

    }

    public void StartMenu()
    {
        if (isStarted)
            return;
        buttons.SetActive(true);
        startHint.SetActive(false);
        isStarted = true;
        titleAnimation.DOPlay();
    }

    public void OpenGamesPanel()
    {
        Debug.Log("OpenGamesPanel");
    }
}
