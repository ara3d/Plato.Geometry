// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Integer: IWholeNumber<Integer>
    {
        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Integer Create() => new Integer();

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Integer other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Integer other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override string ToString() => Value.ToString();
        [MethodImpl(AggressiveInlining)] public static Boolean operator==(Integer a, Integer b) => a.Equals(b);
        [MethodImpl(AggressiveInlining)] public static Boolean operator!=(Integer a, Integer b) => !a.Equals(b);

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        public IArray<Integer> Range { [MethodImpl(AggressiveInlining)] get  => this.MapRange((i) => i); } 
        public Number ToNumber { [MethodImpl(AggressiveInlining)] get  => this.Number; } 
        public Integer PlusOne { [MethodImpl(AggressiveInlining)] get  => this.Add(((Integer)1)); } 
        public Integer MinusOne { [MethodImpl(AggressiveInlining)] get  => this.Subtract(((Integer)1)); } 
        public Integer FromOne { [MethodImpl(AggressiveInlining)] get  => ((Integer)1).Subtract(this); } 
        [MethodImpl(AggressiveInlining)]  public Number FloatDivision(Integer y) => this.ToNumber.Divide(y.ToNumber);
        public IArray<Number> Fractions { [MethodImpl(AggressiveInlining)] get {
            var _var302 = this;
            return this.Range.Map((i) => i.FloatDivision(_var302.Subtract(((Integer)1))));
        }
         } 
        public IArray<Number> FractionsExclusive { [MethodImpl(AggressiveInlining)] get {
            var _var303 = this;
            return this.Range.Map((i) => i.FloatDivision(_var303));
        }
         } 
        public IArray<Vector2> CirclePoints { [MethodImpl(AggressiveInlining)] get  => this.Fractions.Map((x) => x.Turns.UnitCircle); } 
        public IArray<Number> LinearSpace { [MethodImpl(AggressiveInlining)] get  => this.Fractions; } 
        public IArray<Number> LinearSpaceExclusive { [MethodImpl(AggressiveInlining)] get  => this.FractionsExclusive; } 
        public IArray<Number> GeometricSpace { [MethodImpl(AggressiveInlining)] get  => this.LinearSpace.Map((x) => ((Number)1).Pow(x)); } 
        public IArray<Number> GeometricSpaceExclusive { [MethodImpl(AggressiveInlining)] get  => this.LinearSpaceExclusive.Map((x) => ((Number)1).Pow(x)); } 
        public IArray<NumberInterval> Intervals { [MethodImpl(AggressiveInlining)] get  => this.Add(((Integer)1)).LinearSpace.WithNext((a, b) => new Any(NumberInterval, a, b)); } 
        [MethodImpl(AggressiveInlining)]  public Integer4 QuadFaceIndices(Integer row, Integer nCols, Integer nRows){
            var a = row.Multiply(nCols).Add(this);
            var b = row.Multiply(nCols).Add(this.Add(((Integer)1)).Modulo(nCols));
            var c = row.Add(((Integer)1)).Modulo(nRows).Multiply(nCols).Add(this.Add(((Integer)1)).Modulo(nCols));
            var d = row.Add(((Integer)1)).Modulo(nRows).Multiply(nCols).Add(this);
            return (a, b, c, d);
        }

        [MethodImpl(AggressiveInlining)]  public IArray2D<Integer4> AllQuadFaceIndices(Integer nRows, Boolean closedX, Boolean closedY){
            var _var305 = nRows;
            {
                var _var304 = this;
                {
                    var nx = this.Subtract(closedX ? ((Integer)0) : ((Integer)1));
                    var ny = nRows.Subtract(closedY ? ((Integer)0) : ((Integer)1));
                    return nx.MakeArray2D(ny, (col, row) => col.QuadFaceIndices(row, _var304, _var305));
                }
            }
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Integer> Repeat(Integer n){
            var _var306 = this;
            return n.MapRange((i) => _var306);
        }

        [MethodImpl(AggressiveInlining)]  public Integer Lesser(Integer b) => this.LessThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public Integer Greater(Integer b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer Pow2 { [MethodImpl(AggressiveInlining)] get  => this.Multiply(this); } 
        public Integer Pow3 { [MethodImpl(AggressiveInlining)] get  => this.Pow2.Multiply(this); } 
        public Integer Pow4 { [MethodImpl(AggressiveInlining)] get  => this.Pow3.Multiply(this); } 
        public Integer Pow5 { [MethodImpl(AggressiveInlining)] get  => this.Pow4.Multiply(this); } 
        public Integer Square { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Integer Sqr { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Integer Cube { [MethodImpl(AggressiveInlining)] get  => this.Pow3; } 
        public Integer Parabola { [MethodImpl(AggressiveInlining)] get  => this.Sqr; } 

        // Unimplemented concept functions
    }
}