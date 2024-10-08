using System;
using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Systems
{
    public class BlockGraphicsSystem : IEcsRunSystem
    {
        private BlocksImagesContainer _blocksImagesContainer;
        private readonly EcsFilter<BlockGraphicsComponent, ChangeGraphicsEvent> _blockGraphicsFilter = null;

        public void Run()
        {
            foreach (var i in _blockGraphicsFilter)
            {
                ref var entity = ref _blockGraphicsFilter.GetEntity(i);
                ref var blockGraphicsComponent = ref _blockGraphicsFilter.Get1(i);
                ref var changeGraphicsEvent = ref _blockGraphicsFilter.Get2(i);

                ref var blockImage = ref blockGraphicsComponent.BlockImage;
                ref var blockGlow = ref blockGraphicsComponent.BlockGlow;
                ref var typeBlock = ref blockGraphicsComponent.TypeBlock;
                ref var amountLife = ref changeGraphicsEvent.AmountLife;

                CheckTypeBlock(ref typeBlock, amountLife, ref blockImage, ref blockGlow);

                entity.Del<ChangeGraphicsEvent>();
            }
        }

        private void CheckTypeBlock(
            ref TypeBlock typeBlock, 
            int currentAmount,
            ref Image blockImage,
            ref Image blockGlow)
        {
            switch (typeBlock)
            {
                case TypeBlock.Square:
                {
                    ChangeSquareImage(currentAmount, ref blockImage, ref blockGlow);
                    break;
                }
                case TypeBlock.Triangle:
                {
                    ChangeTriangleImage(currentAmount, ref blockImage, ref blockGlow);
                    break;
                }
                case TypeBlock.Rhombus:
                {
                    ChangeRhombusImage(currentAmount, ref blockImage, ref blockGlow);
                    break;
                }
            }
        }

        private void ChangeSquareImage(int currentAmount, ref Image blockImage, ref Image blockGlow)
        {
            var images = GetImages(
                currentAmount,
                _blocksImagesContainer.SquarePink,
                _blocksImagesContainer.SquareGlowPink,
                _blocksImagesContainer.SquareBlue,
                _blocksImagesContainer.SquareGlowBlue,
                _blocksImagesContainer.SquareGreen,
                _blocksImagesContainer.SquareGlowGreen,
                _blocksImagesContainer.SquareTurquoise,
                _blocksImagesContainer.SquareGlowTurquoise,
                _blocksImagesContainer.SquareOrange,
                _blocksImagesContainer.SquareGlowOrange);

            if (images != null)
                ChangeImages(images, ref blockImage, ref blockGlow);
        }

        private void ChangeTriangleImage(int currentAmount, ref Image blockImage, ref Image blockGlow)
        {
            var images = GetImages(
                currentAmount,
                _blocksImagesContainer.TrianglePink,
                _blocksImagesContainer.TriangleGlowPink,
                _blocksImagesContainer.TriangleYellow,
                _blocksImagesContainer.TriangleGlowYellow,
                _blocksImagesContainer.TriangleGreen,
                _blocksImagesContainer.TriangleGlowGreen,
                _blocksImagesContainer.TriangleTurquoise,
                _blocksImagesContainer.TriangleGlowTurquoise,
                _blocksImagesContainer.TriangleOrange,
                _blocksImagesContainer.TriangleGlowOrange);

            if (images != null)
                ChangeImages(images, ref blockImage, ref blockGlow);
        }

        private void ChangeRhombusImage(int currentAmount, ref Image blockImage, ref Image blockGlow)
        {
            var images = GetImages(
                currentAmount,
                _blocksImagesContainer.RhombusPink,
                _blocksImagesContainer.RhombusGlowPink,
                _blocksImagesContainer.RhombusYellow,
                _blocksImagesContainer.RhombusGlowYellow,
                _blocksImagesContainer.RhombusGreen,
                _blocksImagesContainer.RhombusGlowGreen,
                _blocksImagesContainer.RhombusTurquoise,
                _blocksImagesContainer.RhombusGlowTurquoise,
                _blocksImagesContainer.RhombusOrange,
                _blocksImagesContainer.RhombusGlowOrange);

            if (images != null)
                ChangeImages(images, ref blockImage, ref blockGlow);
        }

        private void ChangeImages(Tuple<Sprite, Sprite> images, ref Image blockImage, ref Image blockGlow)
        {
            blockImage.sprite = images.Item1;
            blockGlow.sprite = images.Item2;
        }

        private Tuple<Sprite, Sprite> GetImages(
            int currentAmount,
            Sprite image1,
            Sprite glow1,
            Sprite image2,
            Sprite glow2,
            Sprite image3,
            Sprite glow3,
            Sprite image4,
            Sprite glow4,
            Sprite image5,
            Sprite glow5)
        {
            if (currentAmount <= SettingsGameData.BorderChangeSprite1)
                return new Tuple<Sprite, Sprite>(image1, glow1);
            else if(currentAmount <= SettingsGameData.BorderChangeSprite2)
                return new Tuple<Sprite, Sprite>(image2, glow2);
            else if(currentAmount <= SettingsGameData.BorderChangeSprite3)
                return new Tuple<Sprite, Sprite>(image3, glow3);
            else if(currentAmount <= SettingsGameData.BorderChangeSprite4)
                return new Tuple<Sprite, Sprite>(image4, glow4);
            else if (currentAmount > SettingsGameData.BorderChangeSprite4)
                return new Tuple<Sprite, Sprite>(image5, glow5);

            return null;
        }
    }
}

