using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

using _Editor;
using Cards;
using Managers;

namespace Units
{
	public class Player : Unit
	{
		public int drawCount = 2;

		protected void Start()
		{
		}
		

		public override void StartTurn()
		{
			if (Game.Ctx.activeUnit != this)
			{
				return;
			}
			
			onTurnBegin.Invoke();
			
			Game.Ctx.CardOperator.StartTurn();
		}
		
		public override void EndTurn()
		{
			if (Game.Ctx.activeUnit != this)
			{
				return;
			}
			
			// onAttack Event goes here
            isUnitFlinched = false;
            onAttack.Invoke();
            if (!isUnitFlinched) 
				Game.Ctx.CardOperator.Apply(Game.Ctx.enemy);
            else
            {
	            // Some effect
            }
			
			onTurnEnd.Invoke();

			beingDamagedSomewhere = false;
			if (Game.Ctx.activeUnit == this)
				Game.Ctx.Continue();
			else
				throw new InvalidOperationException("Ending player's turn in non-player round");
		}
	}
}