using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardUIButton : UIButton
{
    [SerializeField] GamePanelController gamePanelController;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        gamePanelController.UpdateGameCards();
    }
}
