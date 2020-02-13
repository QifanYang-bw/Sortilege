using System;
using _Editor;
using Managers;

namespace Units
{
	public class Player : Unit
	{
		public int chainStreak;
		
		void Start()
		{
		}

		public override void Play()
		{
			chainStreak = 0;

			CardManager op = Game.Ctx.CardOperator;
			
			op.DrawFullHand();
			op.Hand[0].LogInfo();
			op.Hand[1].LogInfo();
		}

		public override void EndTurn()
		{
			if (Game.Ctx.RunningMethod == Game.Ctx.Player.Play)
				Game.Ctx.Continue();
			else
				throw new InvalidOperationException("Ending player's turn in non-player round");
		}
	}
}