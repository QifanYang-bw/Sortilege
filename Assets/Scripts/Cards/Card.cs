using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Units;
using Effects;
using Data;

using _Editor;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private CardData cardData;

        public void Info()
        {
            Debugger.Log(cardData.title + ' ' + cardData.strategy + ' ' + cardData.attribute);
        }

        public void Initialize(CardData newCardData)
        {
            cardData = newCardData;
            
            // For inspector visualization
            gameObject.name = cardData.title;
            
            GetComponent<MetaData>().title = cardData.title;
            GetComponent<MetaData>().strategy = cardData.strategy;
            GetComponent<MetaData>().attribute = cardData.attribute;
            
            GetComponent<MetaData>().description = cardData.description;

            GetComponent<Ability>().disableRetract = cardData.disableRetract;
            GetComponent<Ability>().effectList = new List<Effect>(cardData.effectList);
            GetComponent<Ability>().buffEffectList = new List<BuffEffect>(cardData.buffList);
        }
        
        public void Apply(Unit target, float streakCount)
        {
            // onAttack Event goes here
            Game.Ctx.CardOperator.isCurrentCardFlinched = false;
            Game.Ctx.player.onAttack.Invoke();
            if (Game.Ctx.CardOperator.isCurrentCardFlinched) return;
                
            GetComponent<Ability>().Apply(target, streakCount);

            if (Game.Ctx.IsBattleEnded()) Game.Ctx.EndGame();
        }
    }
}