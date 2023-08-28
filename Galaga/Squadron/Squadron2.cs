using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.Squadron {
    public class Squadron2 : Squadron.ISquadron {
        public EntityContainer<Enemy> Enemies {get; }
        public int MaxEnemies {get; } = 9;
        
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            //Enemies = new EntityContainer<Enemy>(MaxEnemies);
            // enemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", 
            //     "Images", "BlueMonster.png"));
            // var alternativeEnemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", 
            //     "Images", "GreenMonster.png"));
            for (int i = 0; i < MaxEnemies/3; i += 4) {
                for (int j = 0; j < MaxEnemies/3; j++) {
            Enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.8f - (float)j *0.1f), new Vec2F(0.05f, 0.05f)),
            new ImageStride(80, enemyStrides)));
                }
            }
        }   
    }
}
// 

// (0,1 ; 0,9)              (0,9 ; 0,9)
// 0,1;0,8      0,5;0,8         0,9;0,8 
// 0,1;0,7      0,5;0,7         0,9;0,7
// 0,1;0,6      0,5;0,6         0,9;0,6
// (0,1 ; 0,1)              (0,9 ; 0,1)