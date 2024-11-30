using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardUIButton : UIButton
{
    [SerializeField] GameCardSO gameCardSO;
    [SerializeField] GamePanelController gamePanelController;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        gamePanelController.UpdateGameCards();
        gamePanelController.UpdateGameInfo(gameCardSO.GameName, gameCardSO.GameDescription, gameCardSO.TitleSize, gameCardSO.DscSize);
    }
}
