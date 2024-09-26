using System;
using Plato.DoublePrecision;

namespace Plato.Geometry.Graphics
{
    public static class Colors
    {
        public static Color RGBA(int r, int g, int b, int a)
        {
            return new Color(r / 255.0, g / 255.0, b / 255.0, a / 255.0);
        }

        public static Random Rng = new Random();

        public static Color GetRandom()
        {
            return new Color(Rng.NextDouble(), Rng.NextDouble(), Rng.NextDouble(), 1.0);
        }

        public static readonly Color AliceBlue = RGBA(240, 248, 255, 255);
        public static readonly Color AntiqueWhite = RGBA(250, 235, 215, 255);
        public static readonly Color Aqua = RGBA(0, 255, 255, 255);
        public static readonly Color Aquamarine = RGBA(127, 255, 212, 255);
        public static readonly Color Azure = RGBA(240, 255, 255, 255);
        public static readonly Color Beige = RGBA(245, 245, 220, 255);
        public static readonly Color Bisque = RGBA(255, 228, 196, 255);
        public static readonly Color Black = RGBA(0, 0, 0, 255);
        public static readonly Color BlanchedAlmond = RGBA(255, 235, 205, 255);
        public static readonly Color Blue = RGBA(0, 0, 255, 255);
        public static readonly Color BlueViolet = RGBA(138, 43, 226, 255);
        public static readonly Color Brown = RGBA(165, 42, 42, 255);
        public static readonly Color BurlyWood = RGBA(222, 184, 135, 255);
        public static readonly Color CadetBlue = RGBA(95, 158, 160, 255);
        public static readonly Color Chartreuse = RGBA(127, 255, 0, 255);
        public static readonly Color Chocolate = RGBA(210, 105, 30, 255);
        public static readonly Color Coral = RGBA(255, 127, 80, 255);
        public static readonly Color CornflowerBlue = RGBA(100, 149, 237, 255);
        public static readonly Color Cornsilk = RGBA(255, 248, 220, 255);
        public static readonly Color Crimson = RGBA(220, 20, 60, 255);
        public static readonly Color Cyan = RGBA(0, 255, 255, 255);
        public static readonly Color DarkBlue = RGBA(0, 0, 139, 255);
        public static readonly Color DarkCyan = RGBA(0, 139, 139, 255);
        public static readonly Color DarkGoldenRod = RGBA(184, 134, 11, 255);
        public static readonly Color DarkGray = RGBA(169, 169, 169, 255);
        public static readonly Color DarkGreen = RGBA(0, 100, 0, 255);
        public static readonly Color DarkKhaki = RGBA(189, 183, 107, 255);
        public static readonly Color DarkMagenta = RGBA(139, 0, 139, 255);
        public static readonly Color DarkOliveGreen = RGBA(85, 107, 47, 255);
        public static readonly Color DarkOrange = RGBA(255, 140, 0, 255);
        public static readonly Color DarkOrchid = RGBA(153, 50, 204, 255);
        public static readonly Color DarkRed = RGBA(139, 0, 0, 255);
        public static readonly Color DarkSalmon = RGBA(233, 150, 122, 255);
        public static readonly Color DarkSeaGreen = RGBA(143, 188, 143, 255);
        public static readonly Color DarkSlateBlue = RGBA(72, 61, 139, 255);
        public static readonly Color DarkSlateGray = RGBA(47, 79, 79, 255);
        public static readonly Color DarkTurquoise = RGBA(0, 206, 209, 255);
        public static readonly Color DarkViolet = RGBA(148, 0, 211, 255);
        public static readonly Color DeepPink = RGBA(255, 20, 147, 255);
        public static readonly Color DeepSkyBlue = RGBA(0, 191, 255, 255);
        public static readonly Color DimGray = RGBA(105, 105, 105, 255);
        public static readonly Color DodgerBlue = RGBA(30, 144, 255, 255);
        public static readonly Color FireBrick = RGBA(178, 34, 34, 255);
        public static readonly Color FloralWhite = RGBA(255, 250, 240, 255);
        public static readonly Color ForestGreen = RGBA(34, 139, 34, 255);
        public static readonly Color Fuchsia = RGBA(255, 0, 255, 255);
        public static readonly Color Gainsboro = RGBA(220, 220, 220, 255);
        public static readonly Color GhostWhite = RGBA(248, 248, 255, 255);
        public static readonly Color Gold = RGBA(255, 215, 0, 255);
        public static readonly Color GoldenRod = RGBA(218, 165, 32, 255);
        public static readonly Color Gray = RGBA(128, 128, 128, 255);
        public static readonly Color Green = RGBA(0, 128, 0, 255);
        public static readonly Color GreenYellow = RGBA(173, 255, 47, 255);
        public static readonly Color HoneyDew = RGBA(240, 255, 240, 255);
        public static readonly Color HotPink = RGBA(255, 105, 180, 255);
        public static readonly Color IndianRed = RGBA(205, 92, 92, 255);
        public static readonly Color Indigo = RGBA(75, 0, 130, 255);
        public static readonly Color Ivory = RGBA(255, 255, 240, 255);
        public static readonly Color Khaki = RGBA(240, 230, 140, 255);
        public static readonly Color Lavender = RGBA(230, 230, 250, 255);
        public static readonly Color LavenderBlush = RGBA(255, 240, 245, 255);
        public static readonly Color LawnGreen = RGBA(124, 252, 0, 255);
        public static readonly Color LemonChiffon = RGBA(255, 250, 205, 255);
        public static readonly Color LightBlue = RGBA(173, 216, 230, 255);
        public static readonly Color LightCoral = RGBA(240, 128, 128, 255);
        public static readonly Color LightCyan = RGBA(224, 255, 255, 255);
        public static readonly Color LightGoldenRodYellow = RGBA(250, 250, 210, 255);
        public static readonly Color LightGray = RGBA(211, 211, 211, 255);
        public static readonly Color LightGreen = RGBA(144, 238, 144, 255);
        public static readonly Color LightPink = RGBA(255, 182, 193, 255);
        public static readonly Color LightSalmon = RGBA(255, 160, 122, 255);
        public static readonly Color LightSeaGreen = RGBA(32, 178, 170, 255);
        public static readonly Color LightSkyBlue = RGBA(135, 206, 250, 255);
        public static readonly Color LightSlateGray = RGBA(119, 136, 153, 255);
        public static readonly Color LightSteelBlue = RGBA(176, 196, 222, 255);
        public static readonly Color LightYellow = RGBA(255, 255, 224, 255);
        public static readonly Color Lime = RGBA(0, 255, 0, 255);
        public static readonly Color LimeGreen = RGBA(50, 205, 50, 255);
        public static readonly Color Linen = RGBA(250, 240, 230, 255);
        public static readonly Color Magenta = RGBA(255, 0, 255, 255);
        public static readonly Color Maroon = RGBA(128, 0, 0, 255);
        public static readonly Color MediumAquaMarine = RGBA(102, 205, 170, 255);
        public static readonly Color MediumBlue = RGBA(0, 0, 205, 255);
        public static readonly Color MediumOrchid = RGBA(186, 85, 211, 255);
        public static readonly Color MediumPurple = RGBA(147, 112, 219, 255);
        public static readonly Color MediumSeaGreen = RGBA(60, 179, 113, 255);
        public static readonly Color MediumSlateBlue = RGBA(123, 104, 238, 255);
        public static readonly Color MediumSpringGreen = RGBA(0, 250, 154, 255);
        public static readonly Color MediumTurquoise = RGBA(72, 209, 204, 255);
        public static readonly Color MediumVioletRed = RGBA(199, 21, 133, 255);
        public static readonly Color MidnightBlue = RGBA(25, 25, 112, 255);
        public static readonly Color MintCream = RGBA(245, 255, 250, 255);
        public static readonly Color MistyRose = RGBA(255, 228, 225, 255);
        public static readonly Color Moccasin = RGBA(255, 228, 181, 255);
        public static readonly Color NavajoWhite = RGBA(255, 222, 173, 255);
        public static readonly Color Navy = RGBA(0, 0, 128, 255);
        public static readonly Color OldLace = RGBA(253, 245, 230, 255);
        public static readonly Color Olive = RGBA(128, 128, 0, 255);
        public static readonly Color OliveDrab = RGBA(107, 142, 35, 255);
        public static readonly Color Orange = RGBA(255, 165, 0, 255);
        public static readonly Color OrangeRed = RGBA(255, 69, 0, 255);
        public static readonly Color Orchid = RGBA(218, 112, 214, 255);
        public static readonly Color PaleGoldenRod = RGBA(238, 232, 170, 255);
        public static readonly Color PaleGreen = RGBA(152, 251, 152, 255);
        public static readonly Color PaleTurquoise = RGBA(175, 238, 238, 255);
        public static readonly Color PaleVioletRed = RGBA(219, 112, 147, 255);
        public static readonly Color PapayaWhip = RGBA(255, 239, 213, 255);
        public static readonly Color PeachPuff = RGBA(255, 218, 185, 255);
        public static readonly Color Peru = RGBA(205, 133, 63, 255);
        public static readonly Color Pink = RGBA(255, 192, 203, 255);
        public static readonly Color Plum = RGBA(221, 160, 221, 255);
        public static readonly Color PowderBlue = RGBA(176, 224, 230, 255);
        public static readonly Color Purple = RGBA(128, 0, 128, 255);
        public static readonly Color RebeccaPurple = RGBA(102, 51, 153, 255);
        public static readonly Color Red = RGBA(255, 0, 0, 255);
        public static readonly Color RosyBrown = RGBA(188, 143, 143, 255);
        public static readonly Color RoyalBlue = RGBA(65, 105, 225, 255);
        public static readonly Color SaddleBrown = RGBA(139, 69, 19, 255);
        public static readonly Color Salmon = RGBA(250, 128, 114, 255);
        public static readonly Color SandyBrown = RGBA(244, 164, 96, 255);
        public static readonly Color SeaGreen = RGBA(46, 139, 87, 255);
        public static readonly Color SeaShell = RGBA(255, 245, 238, 255);
        public static readonly Color Sienna = RGBA(160, 82, 45, 255);
        public static readonly Color Silver = RGBA(192, 192, 192, 255);
        public static readonly Color SkyBlue = RGBA(135, 206, 235, 255);
        public static readonly Color SlateBlue = RGBA(106, 90, 205, 255);
        public static readonly Color SlateGray = RGBA(112, 128, 144, 255);
        public static readonly Color Snow = RGBA(255, 250, 250, 255);
        public static readonly Color SpringGreen = RGBA(0, 255, 127, 255);
        public static readonly Color SteelBlue = RGBA(70, 130, 180, 255);
        public static readonly Color Tan = RGBA(210, 180, 140, 255);
        public static readonly Color Teal = RGBA(0, 128, 128, 255);
        public static readonly Color Thistle = RGBA(216, 191, 216, 255);
        public static readonly Color Tomato = RGBA(255, 99, 71, 255);
        public static readonly Color Turquoise = RGBA(64, 224, 208, 255);
        public static readonly Color Violet = RGBA(238, 130, 238, 255);
        public static readonly Color Wheat = RGBA(245, 222, 179, 255);
        public static readonly Color White = RGBA(255, 255, 255, 255);
        public static readonly Color WhiteSmoke = RGBA(245, 245, 245, 255);
        public static readonly Color Yellow = RGBA(255, 255, 0, 255);
        public static readonly Color YellowGreen = RGBA(154, 205, 50, 255);

        public static Color ToColor(this WellKnownColor color)
        {
            switch (color)
            {
                case WellKnownColor.AliceBlue: return AliceBlue;
                case WellKnownColor.AntiqueWhite: return AntiqueWhite;
                case WellKnownColor.Aqua: return Aqua;
                case WellKnownColor.Aquamarine: return Aquamarine;
                case WellKnownColor.Azure: return Azure;
                case WellKnownColor.Beige: return Beige;
                case WellKnownColor.Bisque: return Bisque;
                case WellKnownColor.Black: return Black;
                case WellKnownColor.BlanchedAlmond: return BlanchedAlmond;
                case WellKnownColor.Blue: return Blue;
                case WellKnownColor.BlueViolet: return BlueViolet;
                case WellKnownColor.Brown: return Brown;
                case WellKnownColor.BurlyWood: return BurlyWood;
                case WellKnownColor.CadetBlue: return CadetBlue;
                case WellKnownColor.Chartreuse: return Chartreuse;
                case WellKnownColor.Chocolate: return Chocolate;
                case WellKnownColor.Coral: return Coral;
                case WellKnownColor.CornflowerBlue: return CornflowerBlue;
                case WellKnownColor.Cornsilk: return Cornsilk;
                case WellKnownColor.Crimson: return Crimson;
                case WellKnownColor.Cyan: return Cyan;
                case WellKnownColor.DarkBlue: return DarkBlue;
                case WellKnownColor.DarkCyan: return DarkCyan;
                case WellKnownColor.DarkGoldenRod: return DarkGoldenRod;
                case WellKnownColor.DarkGray: return DarkGray;
                case WellKnownColor.DarkGreen: return DarkGreen;
                case WellKnownColor.DarkKhaki: return DarkKhaki;
                case WellKnownColor.DarkMagenta: return DarkMagenta;
                case WellKnownColor.DarkOliveGreen: return DarkOliveGreen;
                case WellKnownColor.DarkOrange: return DarkOrange;
                case WellKnownColor.DarkOrchid: return DarkOrchid;
                case WellKnownColor.DarkRed: return DarkRed;
                case WellKnownColor.DarkSalmon: return DarkSalmon;
                case WellKnownColor.DarkSeaGreen: return DarkSeaGreen;
                case WellKnownColor.DarkSlateBlue: return DarkSlateBlue;
                case WellKnownColor.DarkSlateGray: return DarkSlateGray;
                case WellKnownColor.DarkTurquoise: return DarkTurquoise;
                case WellKnownColor.DarkViolet: return DarkViolet;
                case WellKnownColor.DeepPink: return DeepPink;
                case WellKnownColor.DeepSkyBlue: return DeepSkyBlue;
                case WellKnownColor.DimGray: return DimGray;
                case WellKnownColor.DodgerBlue: return DodgerBlue;
                case WellKnownColor.FireBrick: return FireBrick;
                case WellKnownColor.FloralWhite: return FloralWhite;
                case WellKnownColor.ForestGreen: return ForestGreen;
                case WellKnownColor.Fuchsia: return Fuchsia;
                case WellKnownColor.Gainsboro: return Gainsboro;
                case WellKnownColor.GhostWhite: return GhostWhite;
                case WellKnownColor.Gold: return Gold;
                case WellKnownColor.GoldenRod: return GoldenRod;
                case WellKnownColor.Gray: return Gray;
                case WellKnownColor.Green: return Green;
                case WellKnownColor.GreenYellow: return GreenYellow;
                case WellKnownColor.HoneyDew: return HoneyDew;
                case WellKnownColor.HotPink: return HotPink;
                case WellKnownColor.IndianRed: return IndianRed;
                case WellKnownColor.Indigo: return Indigo;
                case WellKnownColor.Ivory: return Ivory;
                case WellKnownColor.Khaki: return Khaki;
                case WellKnownColor.Lavender: return Lavender;
                case WellKnownColor.LavenderBlush: return LavenderBlush;
                case WellKnownColor.LawnGreen: return LawnGreen;
                case WellKnownColor.LemonChiffon: return LemonChiffon;
                case WellKnownColor.LightBlue: return LightBlue;
                case WellKnownColor.LightCoral: return LightCoral;
                case WellKnownColor.LightCyan: return LightCyan;
                case WellKnownColor.LightGoldenRodYellow: return LightGoldenRodYellow;
                case WellKnownColor.LightGray: return LightGray;
                case WellKnownColor.LightGreen: return LightGreen;
                case WellKnownColor.LightPink: return LightPink;
                case WellKnownColor.LightSalmon: return LightSalmon;
                case WellKnownColor.LightSeaGreen: return LightSeaGreen;
                case WellKnownColor.LightSkyBlue: return LightSkyBlue;
                case WellKnownColor.LightSlateGray: return LightSlateGray;
                case WellKnownColor.LightSteelBlue: return LightSteelBlue;
                case WellKnownColor.LightYellow: return LightYellow;
                case WellKnownColor.Lime: return Lime;
                case WellKnownColor.LimeGreen: return LimeGreen;
                case WellKnownColor.Linen: return Linen;
                case WellKnownColor.Magenta: return Magenta;
                case WellKnownColor.Maroon: return Maroon;
                case WellKnownColor.MediumAquaMarine: return MediumAquaMarine;
                case WellKnownColor.MediumBlue: return MediumBlue;
                case WellKnownColor.MediumOrchid: return MediumOrchid;
                case WellKnownColor.MediumPurple: return MediumPurple;
                case WellKnownColor.MediumSeaGreen: return MediumSeaGreen;
                case WellKnownColor.MediumSlateBlue: return MediumSlateBlue;
                case WellKnownColor.MediumSpringGreen: return MediumSpringGreen;
                case WellKnownColor.MediumTurquoise: return MediumTurquoise;
                case WellKnownColor.MediumVioletRed: return MediumVioletRed;
                case WellKnownColor.MidnightBlue: return MidnightBlue;
                case WellKnownColor.MintCream: return MintCream;
                case WellKnownColor.MistyRose: return MistyRose;
                case WellKnownColor.Moccasin: return Moccasin;
                case WellKnownColor.NavajoWhite: return NavajoWhite;
                case WellKnownColor.Navy: return Navy;
                case WellKnownColor.OldLace: return OldLace;
                case WellKnownColor.Olive: return Olive;
                case WellKnownColor.OliveDrab: return OliveDrab;
                case WellKnownColor.Orange: return Orange;
                case WellKnownColor.OrangeRed: return OrangeRed;
                case WellKnownColor.Orchid: return Orchid;
                case WellKnownColor.PaleGoldenRod: return PaleGoldenRod;
                case WellKnownColor.PaleGreen: return PaleGreen;
                case WellKnownColor.PaleTurquoise: return PaleTurquoise;
                case WellKnownColor.PaleVioletRed: return PaleVioletRed;
                case WellKnownColor.PapayaWhip: return PapayaWhip;
                case WellKnownColor.PeachPuff: return PeachPuff;
                case WellKnownColor.Peru: return Peru;
                case WellKnownColor.Pink: return Pink;
                case WellKnownColor.Plum: return Plum;
                case WellKnownColor.PowderBlue: return PowderBlue;
                case WellKnownColor.Purple: return Purple;
                case WellKnownColor.RebeccaPurple: return RebeccaPurple;
                case WellKnownColor.Red: return Red;
                case WellKnownColor.RosyBrown: return RosyBrown;
                case WellKnownColor.RoyalBlue: return RoyalBlue;
                case WellKnownColor.SaddleBrown: return SaddleBrown;
                case WellKnownColor.Salmon: return Salmon;
                case WellKnownColor.SandyBrown: return SandyBrown;
                case WellKnownColor.SeaGreen: return SeaGreen;
                case WellKnownColor.SeaShell: return SeaShell;
                case WellKnownColor.Sienna: return Sienna;
                case WellKnownColor.Silver: return Silver;
                case WellKnownColor.SkyBlue: return SkyBlue;
                case WellKnownColor.SlateBlue: return SlateBlue;
                case WellKnownColor.SlateGray: return SlateGray;
                case WellKnownColor.Snow: return Snow;
                case WellKnownColor.SpringGreen: return SpringGreen;
                case WellKnownColor.SteelBlue: return SteelBlue;
                case WellKnownColor.Tan: return Tan;
                case WellKnownColor.Teal: return Teal;
                case WellKnownColor.Thistle: return Thistle;
                case WellKnownColor.Tomato: return Tomato;
                case WellKnownColor.Turquoise: return Turquoise;
                case WellKnownColor.Violet: return Violet;
                case WellKnownColor.Wheat: return Wheat;
                case WellKnownColor.White: return White;
                case WellKnownColor.WhiteSmoke: return WhiteSmoke;
                case WellKnownColor.Yellow: return Yellow;
                case WellKnownColor.YellowGreen: return YellowGreen;
                default: return White; // Default case
            }
        }
    }

    public enum WellKnownColor
    {
        AliceBlue,
        AntiqueWhite,
        Aqua,
        Aquamarine,
        Azure,
        Beige,
        Bisque,
        Black,
        BlanchedAlmond,
        Blue,
        BlueViolet,
        Brown,
        BurlyWood,
        CadetBlue,
        Chartreuse,
        Chocolate,
        Coral,
        CornflowerBlue,
        Cornsilk,
        Crimson,
        Cyan,
        DarkBlue,
        DarkCyan,
        DarkGoldenRod,
        DarkGray,
        DarkGreen,
        DarkKhaki,
        DarkMagenta,
        DarkOliveGreen,
        DarkOrange,
        DarkOrchid,
        DarkRed,
        DarkSalmon,
        DarkSeaGreen,
        DarkSlateBlue,
        DarkSlateGray,
        DarkTurquoise,
        DarkViolet,
        DeepPink,
        DeepSkyBlue,
        DimGray,
        DodgerBlue,
        FireBrick,
        FloralWhite,
        ForestGreen,
        Fuchsia,
        Gainsboro,
        GhostWhite,
        Gold,
        GoldenRod,
        Gray,
        Green,
        GreenYellow,
        HoneyDew,
        HotPink,
        IndianRed,
        Indigo,
        Ivory,
        Khaki,
        Lavender,
        LavenderBlush,
        LawnGreen,
        LemonChiffon,
        LightBlue,
        LightCoral,
        LightCyan,
        LightGoldenRodYellow,
        LightGray,
        LightGreen,
        LightPink,
        LightSalmon,
        LightSeaGreen,
        LightSkyBlue,
        LightSlateGray,
        LightSteelBlue,
        LightYellow,
        Lime,
        LimeGreen,
        Linen,
        Magenta,
        Maroon,
        MediumAquaMarine,
        MediumBlue,
        MediumOrchid,
        MediumPurple,
        MediumSeaGreen,
        MediumSlateBlue,
        MediumSpringGreen,
        MediumTurquoise,
        MediumVioletRed,
        MidnightBlue,
        MintCream,
        MistyRose,
        Moccasin,
        NavajoWhite,
        Navy,
        OldLace,
        Olive,
        OliveDrab,
        Orange,
        OrangeRed,
        Orchid,
        PaleGoldenRod,
        PaleGreen,
        PaleTurquoise,
        PaleVioletRed,
        PapayaWhip,
        PeachPuff,
        Peru,
        Pink,
        Plum,
        PowderBlue,
        Purple,
        RebeccaPurple,
        Red,
        RosyBrown,
        RoyalBlue,
        SaddleBrown,
        Salmon,
        SandyBrown,
        SeaGreen,
        SeaShell,
        Sienna,
        Silver,
        SkyBlue,
        SlateBlue,
        SlateGray,
        Snow,
        SpringGreen,
        SteelBlue,
        Tan,
        Teal,
        Thistle,
        Tomato,
        Turquoise,
        Violet,
        Wheat,
        White,
        WhiteSmoke,
        Yellow,
        YellowGreen
    }
}