namespace Plato.DoublePrecision
{
    public partial struct Vector2D
    {
        public Vector3D To3D
            => this;

        public Vector2D MidPoint(Vector2D other)
            => Lerp(other, 0.5);
    }
}