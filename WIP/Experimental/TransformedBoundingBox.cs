using System;
using System.Collections.Generic;
using System.Linq;
using Plato;

public static class TransformedBoundingBox
{
    public static TRSTransform3D ComputeBestFitBoundingBox(this IEnumerable<Vector3> points)
        => ComputeBestFitBoundingBox(points.ToArray());

    public static TRSTransform3D ComputeBestFitBoundingBox(this Vector3[] points)
    {
        // Compute the mean of the points
        var mean = Vector3.Default;
        foreach (var point in points)
        {
            mean += point;
        }
        mean /= points.Length;

        // Compute the covariance matrix elements
        float xx = 0, xy = 0, xz = 0;
        float yy = 0, yz = 0, zz = 0;
        foreach (var point in points)
        {
            var d = point - mean;
            xx += d.X * d.X;
            xy += d.X * d.Y;
            xz += d.X * d.Z;
            yy += d.Y * d.Y;
            yz += d.Y * d.Z;
            zz += d.Z * d.Z;
        }
        xx /= points.Length;
        xy /= points.Length;
        xz /= points.Length;
        yy /= points.Length;
        yz /= points.Length;
        zz /= points.Length;

        // Compute eigenvalues and eigenvectors of the covariance matrix
        EigenDecompositionSymmetric3x3(
            xx, xy, xz,
            yy, yz, zz,
            out var eigenvectors,
            out var eigenvalues);

        // Ensure eigenvectors are normalized
        var u = eigenvectors[0].Normalize;
        var v = eigenvectors[1].Normalize;
        var w = eigenvectors[2].Normalize;

        // Ensure the eigenvectors form a right-handed coordinate system
        if (u.Cross(v).Dot(w) < 0)
            w = -w;

        // Create rotation matrix from eigenvectors
        var rotation = new Matrix4x4(
               (u.X, v.X, w.X, 0),
               (u.Y, v.Y, w.Y, 0),
               (u.Z, v.Z, w.Z, 0),
               (0, 0, 0, 1));

        // Compute inverse rotation (transpose of the rotation matrix)
        var invRotation = rotation.Transpose;

        // Transform points to rotated space
        var rotatedPoints = new Vector3[points.Length];
        var min = Vector3.MaxValue;
        var max = Vector3.MinValue;
        for (var i = 0; i < points.Length; i++)
        {
            var p = points[i] - mean; // Center the points
            var rp = invRotation.Transform(p);
            rotatedPoints[i] = rp;
            min = rp.Min(min);
            max = rp.Max(max);
        }

        // Compute scale (size of the bounding box)
        var scale = max - min;

        // Compute the center of the bounding box in rotated space
        var rotatedCenter = (max + min) * 0.5f;

        // Transform the center back to original space
        var obbCenter = rotation.Transform(rotatedCenter) + mean;

        // Extract rotation as a quaternion
        var rotationQuat = Quaternion.CreateFromRotationMatrix(rotation);

        // Assemble the TRS structure
        return (obbCenter, rotationQuat, scale);
    }

    // Helper function to compute eigenvalues and eigenvectors of a symmetric 3x3 matrix
    public static void EigenDecompositionSymmetric3x3(
         float m00, float m01, float m02,
         float m11, float m12, float m22,
         out Vector3[] eigenvectors,
         out float[] eigenvalues)
    {
        // Compute the eigenvalues using the analytical solution
        var p1 = m01 * m01 + m02 * m02 + m12 * m12;
        if (p1 == 0)
        {
            // The matrix is diagonal.
            eigenvalues = new float[] { m00, m11, m22 };
            eigenvectors = new Vector3[]
            {
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1)
            };
            return;
        }

        var q = (m00 + m11 + m22) / 3f;
        var p2 = (m00 - q) * (m00 - q) + (m11 - q) * (m11 - q) + (m22 - q) * (m22 - q) + 2f * p1;
        var p = (float)Math.Sqrt(p2 / 6f);

        // Compute the B matrix
        var invP = 1f / p;
        var b00 = invP * (m00 - q);
        var b01 = invP * m01;
        var b02 = invP * m02;
        var b11 = invP * (m11 - q);
        var b12 = invP * m12;
        var b22 = invP * (m22 - q);

        // Compute the determinant of B
        var detB = b00 * b11 * b22 + 2f * b01 * b12 * b02 - b02 * b02 * b11 - b01 * b01 * b22 - b00 * b12 * b12;
        var r = detB / 2f;

        // Compute the angle phi
        float phi;
        var pi = (float)Math.PI;
        if (r <= -1f)
        {
            phi = pi / 3f;
        }
        else if (r >= 1f)
        {
            phi = 0f;
        }
        else
        {
            phi = (float)Math.Acos(r) / 3f;
        }

        // Compute the eigenvalues
        var eig1 = q + 2f * p * (float)Math.Cos(phi);
        var eig2 = q + 2f * p * (float)Math.Cos(phi + (2f * pi / 3f));
        var eig3 = q + 2f * p * (float)Math.Cos(phi + (4f * pi / 3f));

        eigenvalues = new float[] { eig1, eig2, eig3 };

        // Compute the eigenvectors
        eigenvectors = new Vector3[3];
        eigenvectors[0] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig1);
        eigenvectors[1] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig2);
        eigenvectors[2] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig3);
    }

    // Helper function to compute an eigenvector for a given eigenvalue
    public static Vector3 EigenVectorSymmetric3x3(
           float m00, float m01, float m02,
           float m11, float m12, float m22,
           float eigenvalue)
    {
        // Adjust the matrix by subtracting the eigenvalue
        var a00 = m00 - eigenvalue;
        var a11 = m11 - eigenvalue;
        var a22 = m22 - eigenvalue;

        // Set up the equations
        var row0 = new Vector3(a00, m01, m02);
        var row1 = new Vector3(m01, a11, m12);
        var row2 = new Vector3(m02, m12, a22);

        // Use cross products to find the eigenvector
        var v1 = row1.Cross(row0);
        var v2 = row2.Cross(row0);
        var v3 = row2.Cross(row1);

        // Select the vector with the largest magnitude
        var len1 = v1.LengthSquared;
        var len2 = v2.LengthSquared;
        var len3 = v3.LengthSquared;

        if (len1 >= len2 && len1 >= len3)
        {
            return v1.Normalize;
        }
        else if (len2 >= len1 && len2 >= len3)
        {
            return v2.Normalize;
        }
        else
        {
            return v3.Normalize;
        }
    }
}