using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {
    public class Enemy : Entity {
        public Shape shape {get; set;}
        public IBaseImage image {get;set;}
        public int hitpoints = 3;
        public Enemy(DynamicShape Shape, IBaseImage Image)
            : base(Shape, Image) {}
        public DynamicShape AsDynamicShape() {
            return shape.AsDynamicShape();
        }
    }
}
