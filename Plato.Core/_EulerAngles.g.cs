// Autogenerated file: DO NOT EDIT
// Created on 2025-04-03 1:53:19 AM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct EulerAngles: IRotationalTransform3D
    {
        // Fields
        [DataMember] public readonly Angle Yaw;
        [DataMember] public readonly Angle Pitch;
        [DataMember] public readonly Angle Roll;

        // With functions 
        [MethodImpl(AggressiveInlining)] public EulerAngles WithYaw(Angle yaw) => new EulerAngles(yaw, Pitch, Roll);
        [MethodImpl(AggressiveInlining)] public EulerAngles WithPitch(Angle pitch) => new EulerAngles(Yaw, pitch, Roll);
        [MethodImpl(AggressiveInlining)] public EulerAngles WithRoll(Angle roll) => new EulerAngles(Yaw, Pitch, roll);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public EulerAngles(Angle yaw, Angle pitch, Angle roll) { Yaw = yaw; Pitch = pitch; Roll = roll; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static EulerAngles Create(Angle yaw, Angle pitch, Angle roll) => new EulerAngles(yaw, pitch, roll);

        // Static default implementation
        public static readonly EulerAngles Default = default;

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Angle, Angle, Angle)(EulerAngles self) => (self.Yaw, self.Pitch, self.Roll);
        [MethodImpl(AggressiveInlining)] public static implicit operator EulerAngles((Angle, Angle, Angle) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Angle yaw, out Angle pitch, out Angle roll) { yaw = Yaw; pitch = Pitch; roll = Roll;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(EulerAngles other) => Yaw.Equals(other.Yaw) && Pitch.Equals(other.Pitch) && Roll.Equals(other.Roll);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(EulerAngles other) => !Yaw.Equals(other.Yaw) && Pitch.Equals(other.Pitch) && Roll.Equals(other.Roll);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is EulerAngles other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Yaw, Pitch, Roll);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Yaw\" = {Yaw}, \"Pitch\" = {Pitch}, \"Roll\" = {Roll} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented interface functions
        public Quaternion Quaternion { [MethodImpl(AggressiveInlining)] get  => Quaternion.CreateFromYawPitchRoll(this.Yaw, this.Pitch, this.Roll); } 
[MethodImpl(AggressiveInlining)]  public static implicit operator Quaternion(EulerAngles e) => e.Quaternion;
        public static EulerAngles Identity { [MethodImpl(AggressiveInlining)] get  => (((Number)0), ((Number)0), ((Number)0)); } 
public Matrix4x4 Matrix { [MethodImpl(AggressiveInlining)] get  => this.Quaternion.Matrix; } 
// AMBIGUOUS FUNCTIONS 2
        [MethodImpl(AggressiveInlining)]  public Vector3 Transform(Vector3 v) => v.Transform(this.Quaternion);
// AMBIGUOUS FUNCTIONS 2
        [MethodImpl(AggressiveInlining)]  public Vector3 TransformNormal(Vector3 v) => v.TransformNormal(this.Quaternion);
public Rotation3D Rotation { [MethodImpl(AggressiveInlining)] get  => this.Quaternion; } 
public Translation3D Translation { [MethodImpl(AggressiveInlining)] get  => Translation3D.Identity; } 
public Matrix4x4 Matrix4x4 { [MethodImpl(AggressiveInlining)] get  => this.Matrix; } 
[MethodImpl(AggressiveInlining)]  public static implicit operator Matrix4x4(EulerAngles t) => t.Matrix4x4;
        public MatrixTransform3D Inverse { [MethodImpl(AggressiveInlining)] get  => this.Matrix.Invert; } 

        // Unimplemented interface functions
    }
}
