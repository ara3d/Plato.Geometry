using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNQuaternion = System.Numerics.Quaternion;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Quaternion
    {
        // Fields 

        public readonly Number X;
        public readonly Number Y;
        public readonly Number Z;
        public readonly Number W;

        // Constructor 

        [MethodImpl(AggressiveInlining)]
        public Quaternion(Number x, Number y, Number z, Number w) 
            => (X, Y, Z, W) = (x, y, z, w);

        // Static properties 

        public static readonly Quaternion Identity
            = SNQuaternion.Identity;

        // Helper functions 

        [MethodImpl(AggressiveInlining)]
        public SNQuaternion ToSystem()
            => Unsafe.As<Quaternion, SNQuaternion>(ref this);

        // Implicit casts 

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNQuaternion(Quaternion v)
            => v.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Quaternion(SNQuaternion v)
            => Unsafe.As<SNQuaternion, Quaternion>(ref v);

        // Operators

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator +(Quaternion a, Quaternion b)
            => a.ToSystem() + b.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator -(Quaternion a, Quaternion b)
            => a.ToSystem() - b.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator -(Quaternion a)
            => -a.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator *(Quaternion a, Quaternion b)
            => a.ToSystem() * b.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator *(Quaternion a, Number scalar)
            => a.ToSystem() * scalar;

        [MethodImpl(AggressiveInlining)]
        public static Quaternion operator /(Quaternion a, Quaternion b)
            => a.ToSystem() / b.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static bool operator ==(Quaternion a, Quaternion b)
            => a.Equals(b);

        [MethodImpl(AggressiveInlining)]
        public static bool operator !=(Quaternion a, Quaternion b)
            => !a.Equals(b);

        // Forwarded static methods

        [MethodImpl(AggressiveInlining)]
        public static Quaternion CreateFromAxisAngle(Vector3 axis, Number angle)
            => SNQuaternion.CreateFromAxisAngle(axis, angle);

        [MethodImpl(AggressiveInlining)]
        public static Quaternion CreateFromYawPitchRoll(Angle yaw, Angle pitch, Angle roll)
            => SNQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

        [MethodImpl(AggressiveInlining)]
        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
            => SNQuaternion.CreateFromRotationMatrix(matrix);

        //==
        // Static methods converted into instance methods 

        [MethodImpl(AggressiveInlining)]
        public Quaternion Concatenate(Quaternion value2)
            => SNQuaternion.Concatenate(ToSystem(), value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public Number Dot(Quaternion quaternion2)
            => SNQuaternion.Dot(ToSystem(), quaternion2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, Number amount)
            => SNQuaternion.Lerp(quaternion1.ToSystem(), quaternion2.ToSystem(), amount);

        [MethodImpl(AggressiveInlining)]
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, Number amount)
            => SNQuaternion.Slerp(quaternion1.ToSystem(), quaternion2.ToSystem(), amount);
        
        // Properties

        public Number Length
        {
            [MethodImpl(AggressiveInlining)] get => ToSystem().Length();
        }

        public Number LengthSquared
        {
            [MethodImpl(AggressiveInlining)] get => ToSystem().LengthSquared();
        }

        public Quaternion Normalize
        {
            [MethodImpl(AggressiveInlining)]
            get => SNQuaternion.Normalize(ToSystem());
        }

        public Quaternion Conjugate
        {
            [MethodImpl(AggressiveInlining)]
            get => SNQuaternion.Conjugate(ToSystem());
        }

        public Quaternion Inverse
        {
            [MethodImpl(AggressiveInlining)]
            get => SNQuaternion.Inverse(ToSystem());
        }

        // Equality and hashing

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Quaternion other)
            => ToSystem().Equals(other.ToSystem());

        public override bool Equals(object obj)
            => obj is Quaternion q && Equals(q);

        public override int GetHashCode()
            => ToSystem().GetHashCode();

        public override string ToString()
            => ToSystem().ToString();
    }
}