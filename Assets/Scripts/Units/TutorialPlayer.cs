using System;
using TMPro;
using UnityEngine;

namespace Units
{
	public class TutorialPlayer : Player
	{
		public override void StartTurn()
		{
			Game.Ctx.CardOperator.StartTurn();
			if (Game.Ctx.turnCount == 4)
				Game.Ctx.CardOperator.DrawCards(2, true, false);
			
			
			onTurnBegin.Invoke();
			switch (Game.Ctx.turnCount)
			{
				case 0:
					break;
				case 1:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "Before you start your adventure, let's get familiar with the battle system.\nDrag your card into the yellow queue zone and click End Turn to play it.";
					break;
				case 2:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "Great! Now you might notice you can't play both of your cards this turn. \nThis is because you can only play cards with same class/attribute as the last card you played this turn.\nJust gain some armors this turn.";
					break;
				case 3:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"The effect of all cards you play this turn = Effect of last card you play * number of cards you play this turn. \nNow try maximize your damage by playing Slam > Flare Blitz > Toast The Avocado.";
					break;
				case 4:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"What? 11111 damage? The stupid game designer must fell asleep while coding. \nDon't worry, I will give you 2 extra cards. \nJust make sure you play Electric Shield the last because it requires streak >= 4 to trigger.";
					break;
				case 5:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"Phew.. That was close. We need to find some way to end this.";
					break;
				case 6:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "TADA! I found the avocado peeler. However it requires streak >= 5 to take effect. \nSave your cards this turn..";
					break;
				case 7:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "And also this turn. Trust me, we can end this..";
					break;
				case 8:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "Yes! Do it now!";
					break;
				case 9:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"Ehh.. Did you mess something up?";
					break;
				case 10:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"Sorry kiddo.. You can restart now.";
					break;
				case 11:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "What? You are not supposed to be here..";
					break;
				case 12:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text =
						"Congrats! Even the game developer did not figure out how to come here.. \nAnyway, you are going to die. Die. Now.";
					break;
				default:
					GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "Sometimes I miss those old good days when players don't try to break the game  :(";
					break;
			}
		}

		public override void EndTurn()
		{
			// Something something coroutine + ienum
			GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "";
			Game.Ctx.CardOperator.Apply(Game.Ctx.enemy);
			
			onTurnEnd.Invoke();

			beingDamagedSomewhere = false;
			if (Game.Ctx.activeUnit == this)
				Game.Ctx.Continue();
			else
				throw new InvalidOperationException("Ending player's turn in non-player round");
		}
	}
}