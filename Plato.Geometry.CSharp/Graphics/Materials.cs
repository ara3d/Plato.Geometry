namespace Plato.Geometry.Graphics
{
    public static class Materials
    {
        public static readonly Material Water = new Material(64, 164, 223, 255, 1.0, 0.0, 0.02, 0.0, 0.0, true, 0.02, 1.333, 0.0, 0.1, 64, 164, 223);
        public static readonly Material Glass = new Material(255, 255, 255, 255, 1.0, 0.0, 0.0, 0.0, 0.0, true, 0.04, 1.5, 0.0, 0.0, 255, 255, 255);
        public static readonly Material Diamond = new Material(255, 255, 255, 255, 1.0, 0.0, 0.0, 0.0, 0.0, true, 0.17, 2.417, 0.0, 0.0, 255, 255, 255);
        public static readonly Material Iron = new Material(190, 190, 190, 255, 0.0, 1.0, 0.5, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Gold = new Material(255, 215, 0, 255, 0.0, 1.0, 0.3, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Copper = new Material(184, 115, 51, 255, 0.0, 1.0, 0.2, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Silver = new Material(192, 192, 192, 255, 0.0, 1.0, 0.1, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Aluminum = new Material(211, 211, 211, 255, 0.0, 1.0, 0.15, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Plastic = new Material(255, 255, 255, 255, 0.0, 0.0, 0.4, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.0, 255, 255, 255);
        public static readonly Material Rubber = new Material(30, 30, 30, 255, 0.0, 0.0, 0.8, 0.0, 0.0, true, 0.02, 1.519, 0.0, 0.0, 30, 30, 30);
        public static readonly Material Wood = new Material(133, 94, 66, 255, 0.0, 0.0, 0.6, 0.1, 0.2, true, 0.04, 1.55, 0.0, 0.2, 133, 94, 66);
        public static readonly Material Concrete = new Material(200, 200, 200, 255, 0.0, 0.0, 0.9, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.0, 200, 200, 200);
        public static readonly Material Asphalt = new Material(30, 30, 30, 255, 0.0, 0.0, 1.0, 0.0, 0.0, true, 0.02, 1.635, 0.0, 0.0, 30, 30, 30);
        public static readonly Material Brick = new Material(150, 50, 50, 255, 0.0, 0.0, 0.8, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.1, 150, 50, 50);
        public static readonly Material Leather = new Material(139, 69, 19, 255, 0.0, 0.0, 0.7, 0.1, 0.2, true, 0.04, 1.53, 0.0, 0.1, 139, 69, 19);
        public static readonly Material Cotton = new Material(255, 255, 255, 255, 0.0, 0.0, 0.85, 0.2, 0.1, true, 0.035, 1.53, 0.0, 0.1, 255, 255, 255);
        public static readonly Material Skin = new Material(255, 224, 189, 255, 0.0, 0.0, 0.6, 0.2, 0.3, true, 0.028, 1.4, 0.0, 0.8, 255, 200, 180);
        public static readonly Material Silk = new Material(245, 245, 245, 255, 0.0, 0.0, 0.2, 0.3, 0.2, true, 0.04, 1.53, 0.0, 0.1, 245, 245, 245);
        public static readonly Material Marble = new Material(255, 255, 255, 255, 0.0, 0.0, 0.3, 0.0, 0.0, true, 0.05, 1.49, 0.0, 0.5, 255, 255, 255);
        public static readonly Material Ice = new Material(180, 225, 255, 255, 1.0, 0.0, 0.02, 0.0, 0.0, true, 0.018, 1.31, 0.0, 0.2, 180, 225, 255);
        public static readonly Material Snow = new Material(255, 255, 255, 255, 0.0, 0.0, 0.9, 0.0, 0.0, true, 0.04, 1.31, 0.0, 0.9, 255, 255, 255);
        public static readonly Material Paper = new Material(255, 255, 255, 255, 0.0, 0.0, 0.7, 0.0, 0.0, true, 0.035, 1.5, 0.0, 0.1, 255, 255, 255);
        public static readonly Material Ceramic = new Material(245, 245, 245, 255, 0.0, 0.0, 0.3, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.1, 245, 245, 245);
        public static readonly Material Milk = new Material(255, 255, 255, 255, 1.0, 0.0, 0.5, 0.0, 0.0, true, 0.022, 1.35, 0.0, 1.0, 255, 255, 255);
        public static readonly Material Hair = new Material(80, 50, 20, 255, 0.0, 0.0, 0.8, 0.0, 0.0, true, 0.05, 1.55, 0.0, 0.3, 80, 50, 20);
        public static readonly Material Wool = new Material(255, 255, 255, 255, 0.0, 0.0, 0.85, 0.2, 0.2, true, 0.04, 1.53, 0.0, 0.3, 255, 255, 255);
        public static readonly Material Nylon = new Material(255, 255, 255, 255, 0.0, 0.0, 0.4, 0.1, 0.0, true, 0.04, 1.53, 0.0, 0.0, 255, 255, 255);
        public static readonly Material Polyester = new Material(255, 255, 255, 255, 0.0, 0.0, 0.5, 0.1, 0.0, true, 0.04, 1.55, 0.0, 0.0, 255, 255, 255);
        public static readonly Material CarbonFiber = new Material(50, 50, 50, 255, 0.0, 0.0, 0.3, 0.0, 0.0, true, 0.05, 2.42, 0.0, 0.0, 50, 50, 50);
        public static readonly Material Porcelain = new Material(245, 245, 245, 255, 0.0, 0.0, 0.2, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.1, 245, 245, 245);
        public static readonly Material Granite = new Material(112, 128, 144, 255, 0.0, 0.0, 0.6, 0.0, 0.0, true, 0.05, 1.54, 0.0, 0.1, 112, 128, 144);
        public static readonly Material Sand = new Material(194, 178, 128, 255, 0.0, 0.0, 0.7, 0.0, 0.0, true, 0.05, 1.54, 0.0, 0.1, 194, 178, 128);
        public static readonly Material Soil = new Material(101, 67, 33, 255, 0.0, 0.0, 0.8, 0.0, 0.0, true, 0.05, 1.52, 0.0, 0.1, 101, 67, 33);
        public static readonly Material Grass = new Material(34, 139, 34, 255, 0.0, 0.0, 0.9, 0.0, 0.0, true, 0.05, 1.53, 0.0, 0.3, 34, 139, 34);
        public static readonly Material Leaves = new Material(34, 139, 34, 255, 0.0, 0.0, 0.5, 0.0, 0.0, true, 0.05, 1.53, 0.0, 0.8, 34, 139, 34);
        public static readonly Material IceCube = new Material(180, 225, 255, 255, 1.0, 0.0, 0.02, 0.0, 0.0, true, 0.018, 1.31, 0.0, 0.2, 180, 225, 255);
        public static readonly Material Cloud = new Material(255, 255, 255, 255, 1.0, 0.0, 1.0, 0.0, 0.0, true, 0.0, 1.0, 0.0, 0.9, 255, 255, 255);
        public static readonly Material Smoke = new Material(105, 105, 105, 255, 1.0, 0.0, 1.0, 0.0, 0.0, true, 0.0, 1.0, 0.0, 0.9, 105, 105, 105);
        public static readonly Material Blood = new Material(138, 7, 7, 255, 1.0, 0.0, 0.6, 0.0, 0.0, true, 0.022, 1.35, 0.0, 0.9, 138, 7, 7);
        public static readonly Material Honey = new Material(255, 193, 7, 255, 1.0, 0.0, 0.5, 0.0, 0.0, true, 0.04, 1.504, 0.0, 0.8, 255, 193, 7);
        public static readonly Material Oil = new Material(255, 255, 255, 255, 1.0, 0.0, 0.3, 0.0, 0.0, true, 0.04, 1.47, 0.0, 0.2, 255, 255, 255);
        public static readonly Material RubberDuck = new Material(255, 255, 0, 255, 0.0, 0.0, 0.6, 0.0, 0.0, true, 0.05, 1.519, 0.0, 0.0, 255, 255, 0);
        public static readonly Material Chocolate = new Material(123, 63, 0, 255, 0.0, 0.0, 0.4, 0.0, 0.0, true, 0.04, 1.5, 0.0, 0.1, 123, 63, 0);
        public static readonly Material PlasticBottle = new Material(255, 255, 255, 255, 1.0, 0.0, 0.3, 0.0, 0.0, true, 0.05, 1.5, 0.0, 0.0, 255, 255, 255);
        public static readonly Material Bubble = new Material(255, 255, 255, 255, 1.0, 0.0, 0.0, 0.0, 0.0, true, 0.0, 1.0, 0.0, 0.0, 255, 255, 255);
        public static readonly Material CandleWax = new Material(255, 255, 224, 255, 0.0, 0.0, 0.4, 0.0, 0.0, true, 0.05, 1.44, 0.0, 0.7, 255, 255, 224);
        public static readonly Material Sugar = new Material(255, 255, 255, 255, 0.0, 0.0, 0.5, 0.0, 0.0, true, 0.05, 1.56, 0.0, 0.5, 255, 255, 255);
        public static readonly Material Salt = new Material(255, 255, 255, 255, 0.0, 0.0, 0.5, 0.0, 0.0, true, 0.05, 1.54, 0.0, 0.5, 255, 255, 255);
        public static readonly Material Ink = new Material(0, 0, 0, 255, 1.0, 0.0, 0.8, 0.0, 0.0, true, 0.04, 1.6, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Styrofoam = new Material(255, 255, 255, 255, 0.0, 0.0, 0.9, 0.0, 0.0, true, 0.05, 1.06, 0.0, 0.3, 255, 255, 255);
        public static readonly Material Mercury = new Material(230, 230, 250, 255, 0.0, 1.0, 0.0, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Lead = new Material(70, 70, 70, 255, 0.0, 1.0, 0.3, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Brass = new Material(181, 166, 66, 255, 0.0, 1.0, 0.3, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Bronze = new Material(205, 127, 50, 255, 0.0, 1.0, 0.3, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Steel = new Material(192, 192, 192, 255, 0.0, 1.0, 0.4, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Titanium = new Material(135, 135, 135, 255, 0.0, 1.0, 0.4, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Chromium = new Material(200, 200, 200, 255, 0.0, 1.0, 0.05, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
        public static readonly Material Tungsten = new Material(90, 90, 90, 255, 0.0, 1.0, 0.2, 0.0, 0.0, false, 0.0, 0.0, 0.0, 0.0, 0, 0, 0);
    }
}