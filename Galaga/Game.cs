using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;

namespace Galaga
{
    public class Game : IGameEventProcessor<object> {
        private GameEventBus<object> eventBus;
        private Window window;
        private GameTimer gameTimer;
        private Player player;
        private EntityContainer<Enemy> enemies;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        
        private const int EXPLOSION_LENGTH_MS = 500;

        public Game() {
            window = new Window("Galaga", 500, 500);

            gameTimer = new GameTimer(30, 30);

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus<object>();

            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.RegisterEventBus(eventBus);

            eventBus.Subscribe(GameEventType.InputEvent, this);

            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", 
                "Images", "BlueMonster.png"));

            const int numEnemies = 8;

            enemies = new EntityContainer<Enemy>(numEnemies);

            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, images)));

            
            enemyExplosions = new AnimationContainer(numEnemies);
            
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            }
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(new StationaryShape(position, extent), EXPLOSION_LENGTH_MS, 
                new ImageStride (EXPLOSION_LENGTH_MS/8, explosionStrides));
        }
 
         private void IterateShots() {
            player.playerShots.Iterate(shot => {
                shot.Shape.Position.Y += 0.025f;
                if (shot.Shape.Position.Y > (1.0 - shot.Shape.Extent.Y)) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                            if (enemy.hitpoints == 1) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                shot.DeleteEntity();
                                enemy.DeleteEntity(); 
                            } else if (enemy.hitpoints < 3 && enemy.hitpoints > 1) {
                                shot.DeleteEntity();
                                enemy.Image = new ImageStride(2,
                                    Path.Combine("Assets", "Images", "RedMonster.png"));
                                enemy.hitpoints -= 1;
                            } else {
                                shot.DeleteEntity();
                                enemy.hitpoints -= 1;
                            }
                        }
                    });
                }
            }); 
        }

        ///<summary> functions for the game </summary>
        ///<param name ="key"> argument is a given key-input as string </param>
        public void KeyPress(string key) {
            switch (key) {
                default:
                    break;
            }
        }

        ///<summary> Game functions that is close game. </summary>
        ///<param name="key"> string as a key on the keyboard </param>
        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_ESCAPE":
                    eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                    break;
                default:
                    break;
                
            }
        }

        ///<summary> Check if a key is pressed or released and acts accordingly 
        ///using KeyPress or KeyRelease</summary>
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    player.KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    player.KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;
            }
        }


        public void Run() {
            while(window.IsRunning()) {
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();

                    eventBus.ProcessEvents();
                    IterateShots();
                    player.Move();
                }

                if (gameTimer.ShouldRender()) {
                    window.Clear();

                    player.Render();
                    enemies.RenderEntities();
                    player.playerShots.RenderEntities();
                    enemyExplosions.RenderAnimations();
                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }
    }
}
