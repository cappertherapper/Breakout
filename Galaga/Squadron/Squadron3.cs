using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.Squadron {
    public class Squadron3 : Squadron.ISquadron {
        public EntityContainer<Enemy> Enemies {get; }
        public int MaxEnemies {get; }
        
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
        for (int i = 0; i < 3; i++) {
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", 
            "Images", "BlueMonster.png"));
            Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.55f + (float)i * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, images)));
        }
        for(int a = 0; a<3; a++) {
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", 
            "Images", "BlueMonster.png"));
            Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.45f + (float)a * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, images)));
        }
        for(int h = 0; h<3; h++) {
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", 
            "Images", "BlueMonster.png"));
            Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.35f + (float)h * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, images)));
        }
    }
}
}