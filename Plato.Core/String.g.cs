// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct String: IValue<String>, IOrderable<String>, IArray<Character>
    {
        // Static factory function
        [MethodImpl(AggressiveInlining)] public static String Create() => new String();

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(String other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is String other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override string ToString() => Value.ToString();
        [MethodImpl(AggressiveInlining)] public static Boolean operator==(String a, String b) => a.Equals(b);
        [MethodImpl(AggressiveInlining)] public static Boolean operator!=(String a, String b) => !a.Equals(b);

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Character> GetEnumerator() => new ArrayEnumerator<Character>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Character System.Collections.Generic.IReadOnlyList<Character>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Character>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public IArray<String> Repeat(Integer n){
            var _var307 = this;
            return n.MapRange((i) => _var307);
        }

        [MethodImpl(AggressiveInlining)]  public Boolean LessThan(String b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThan(String b) => b.LessThan(this);
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThanOrEquals(String b) => b.LessThanOrEquals(this);
        [MethodImpl(AggressiveInlining)]  public String Lesser(String b) => this.LessThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public String Greater(String b) => this.GreaterThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public Integer CompareTo(String b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);

        // Unimplemented concept functions
        [MethodImpl(AggressiveInlining)]  public Boolean LessThanOrEquals(String y) => Intrinsics.LessThanOrEquals(this, y);
    }
}
