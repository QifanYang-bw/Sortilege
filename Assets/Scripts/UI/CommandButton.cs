using System;
using _Editor;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleTMP, _descTMP;

        public void Start()
        {
            _titleTMP = transform.Find("TMPTitle").GetComponent<TextMeshProUGUI>();
            _descTMP = transform.Find("TMPDescription").GetComponent<TextMeshProUGUI>();
        }

        public void Click()
        {
            if (Game.Ctx.paused) return;
            
	        if (Game.Ctx.BattleOperator.player.waitingForAction)
			{
				SetStatus(!Game.Ctx.BattleOperator.inSelectEnemyMode);
			}
			else
			{
				Debugger.Log("Nope, you can't end your turn now");
			}
        }

        public void SetStatus(bool setToAttack)
        {
	        if (setToAttack)
			{
				Game.Ctx.BattleOperator.inSelectEnemyMode = true;
				
				Game.Ctx.VfxOperator.SetAllBrightnessInAimMode(0.48f, true);
				_titleTMP.text = "Cancel";
				_descTMP.text = "Back to card arrangement";
			}
			else
			{
				Game.Ctx.BattleOperator.inSelectEnemyMode = false;
				
				Game.Ctx.VfxOperator.SetAllBrightnessInAimMode(0.0f, false);
				_titleTMP.text = "Attack!";
				_descTMP.text = "Choose your target";
			}
        }
    }
}