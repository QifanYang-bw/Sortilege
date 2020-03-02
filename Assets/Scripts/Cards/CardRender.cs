using System;
using System.Collections;
using System.Collections.Generic;
using _Editor;
using UnityEngine;
using TMPro;

using Managers;
using UI;
using UnityEditor.U2D;
using Effects;

namespace Cards
{
    public class CardRender : MonoBehaviour
    {
        public bool visible = true;
        
        // public float moveSpeed = 0.1f;

        private SpriteRenderer borderSprite, bgSprite, attRenderer, strRenderer;
        private TextMeshProUGUI cardText;

        private float onSelectZoomScale = 1.7f;

        public void Start()
        {
            MetaData meta = GetComponent<MetaData>();
            
            borderSprite = transform.Find("CardBorder").GetComponent<SpriteRenderer>();
            bgSprite = transform.Find("CardBackground").GetComponent<SpriteRenderer>();
            attRenderer = transform.Find("AttributeSprite").GetComponent<SpriteRenderer>();
            strRenderer = transform.Find("StrategySprite").GetComponent<SpriteRenderer>();
            
            // Set strategy color
            borderSprite.color = VfxManager.strategyColors[meta.strategy];

            // Set attribute 
            attRenderer.sprite = Resources.Load<Sprite>(VfxManager.AttributeSpritePaths[meta.attribute]);
            
            // Set strategy
            strRenderer.sprite = Resources.Load<Sprite>(VfxManager.StrategySpritePaths[meta.strategy]);

            // Set name
            transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = meta.title;

            // Set rules text
            cardText = transform.Find("CardText").GetComponent<TextMeshProUGUI>();
            cardText.text = GetComponent<MetaData>().description;

            SetOrder();
        }

        
        public void OnSelectZoom()
        {
            Vector3 newLocalScale = transform.localScale *  onSelectZoomScale;
            transform.localScale = newLocalScale;
        }

        public void SetAvailability(bool availability)
        {
            if (availability)
            {
                bgSprite.color = Game.Ctx.VfxOperator.isAvailableColor;
            }
            else
            {
                bgSprite.color = Game.Ctx.VfxOperator.notAvailableColor;
            }
        }

        public void LateUpdate()
        {
            cardText.text = Text();
        }

        public void SetOrder()
        {
            int sortOrder = Game.Ctx.VfxOperator.GetSortOrder();
            var canvas = GetComponent<Canvas>();
            canvas.sortingLayerName = "Card";
            canvas.sortingOrder = sortOrder;
            bgSprite.sortingOrder = sortOrder;
            sortOrder = Game.Ctx.VfxOperator.GetSortOrder();
            borderSprite.sortingOrder = sortOrder;
            sortOrder = Game.Ctx.VfxOperator.GetSortOrder();
            attRenderer.sortingOrder = sortOrder;
            strRenderer.sortingOrder = sortOrder;
        }

        public void Hide()
        {
            visible = false;
            TextMeshProUGUI[] tmPros = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var r in tmPros) {
                r.enabled = false;
            }
            SpriteRenderer[] spRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var r in spRenderers) {
                r.enabled = false;
            }
        }
    
        public void Show()
        {
            visible = true;
            TextMeshProUGUI[] tmPros = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var r in tmPros) {
                r.enabled = true;
            }
            SpriteRenderer[] spRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (var r in spRenderers) {
                r.enabled = true;
            }
        }

        private string Text()
        {
            var playPile = Game.Ctx.CardOperator.pilePlay;
            if (playPile.Count() > 0 && GetComponent<Card>() == 
                playPile.Get(playPile.Count() - 1))
            {
                return GetComponent<Ability>().Description();
            }
            else
            {
                return GetComponent<MetaData>().description;
            }

        }
    }
    
}