using DIKUArcade.Entities;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron {
    public class Squadron1 : Squadron.ISquadron {
        public EntityContainer<Enemy> Enemies {get; }
        public int MaxEnemies {get; } = 23;
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", 
                "Images", "BlueMonster.png"));
            for (int i = 0; i < 8; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images)));
            }
            for (int i = 0; i < 7; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.15f + (float)i * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images)));
            }
            for (int i = 0; i < 8; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images)));
            }
        }
    }
}