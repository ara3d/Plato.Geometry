namespace Ara3D.Mathematics
{
    public class Angle
    {
        public float Radians { get; }
        public float Turns => Radians / Constants.TwoPi;
        public float Degrees => Turns * 360;
        public float Gradians => Turns / 400;
        public Angle(float x) => Radians = x;
        public static implicit operator Angle(float x) => new Angle(x);
        public static implicit operator float(Angle x) => x.Radians;
        public static Angle FromRadians(float x) => new Angle(x);
        public static Angle FromTurns(float x) => FromRadians(x * Constants.TwoPi);
        public static Angle FromDegrees(float x) => FromTurns(x / 360);
        public static Angle FromGradians(float x) => FromTurns(x / 400);
        public static Angle operator *(Angle x, float y) => x.Radians * y;
        public static Angle operator /(Angle x, float y) => x.Radians / y;
        public static Angle operator +(Angle x, float y) => x.Radians + y;
        public static Angle operator -(Angle x, float y) => x.Radians - y;
        public static Angle operator +(Angle x, Angle y) => x.Radians + y.Radians;
        public static Angle operator -(Angle x, Angle y) => x.Radians - y.Radians;
        public static Angle operator -(Angle x) => -x.Radians;
        public float Cos => Radians.Cos();
        public float Sin => Radians.Sin();
        public float Tan => Radians.Tan();
    }

    public static class AngleExtensions
    {
        public static Angle Degrees(this float x) => x.Turns() / 360;
        public static Angle Turns(this float x) => new Angle(x * Constants.Pi * 2);
        public static Angle HalfTurns(this float x) => x.Turns() / 2;
        public static Angle Gradians(this float x) => x.Turns() / 400;
   
        public static Angle Degrees(this int x) => x.Turns() / 360;
        public static Angle Turns(this int x) => new Angle(x * Constants.Pi * 2);
        public static Angle HalfTurns(this int x) => x.Turns() / 2;
        public static Angle Gradians(this int x) => x.Turns() / 400;
    }
}