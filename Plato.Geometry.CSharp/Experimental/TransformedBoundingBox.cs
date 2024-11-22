using System;
using System.Collections.Generic;
using System.Linq;
using Plato.SinglePrecision;

    /*
public static class TransformedBoundingBox
{
    public static Transform3D ComputeBestFitBoundingBox(this IEnumerable<Vector3D> points)
        => ComputeBestFitBoundingBox(points.ToArray());

    public static Transform3D ComputeBestFitBoundingBox(this Vector3D[] points)
    {
        if (points == null || points.Length == 0)
            throw new ArgumentException("Points array is null or empty.");

        // Compute the mean of the points
        var mean = Vector3D.Default;
        foreach (var point in points)
            mean += point;
        mean /= points.Length;

        // Compute the covariance matrix elements
        double xx = 0, xy = 0, xz = 0;
        double yy = 0, yz = 0, zz = 0;
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
        double invN = 1.0 / points.Length;
        xx *= invN; xy *= invN; xz *= invN;
        yy *= invN; yz *= invN; zz *= invN;

        // Construct the covariance matrix
        var covarianceMatrix = new double[3, 3]
        {
        { xx, xy, xz },
        { xy, yy, yz },
        { xz, yz, zz }
        };

        // Compute eigenvalues and eigenvectors using the Jacobi method
        double[] eigenvalues;
        double[,] eigenvectors;
        JacobiEigenvalue(covarianceMatrix, out eigenvalues, out eigenvectors);

        // Convert eigenvectors matrix to Vector3D array and ensure right-handed coordinate system
        var u = new Vector3D((float)eigenvectors[0, 0], (float)eigenvectors[1, 0], (float)eigenvectors[2, 0]);
        var v = new Vector3D((float)eigenvectors[0, 1], (float)eigenvectors[1, 1], (float)eigenvectors[2, 1]);
        var w = new Vector3D((float)eigenvectors[0, 2], (float)eigenvectors[1, 2], (float)eigenvectors[2, 2]);

        // Ensure the eigenvectors form a right-handed coordinate system
        if (u.Cross(v).Dot(w) < 0)
            w = -w;

        // Create rotation matrix from eigenvectors
        var rotation = new Matrix4x4(
            ((float)u.X, (float)v.X, (float)w.X, 0),
            ((float)u.Y, (float)v.Y, (float)w.Y, 0),
            ((float)u.Z, (float)v.Z, (float)w.Z, 0),
            (0, 0, 0, 1));

        // Compute inverse rotation (transpose of the rotation matrix)
        var invRotation = rotation.Transpose;

        // Transform points to rotated space and find min/max extents
        var min = Vector3D.MaxValue;
        var max = Vector3D.MinValue;
        foreach (var point in points)
        {
            var centeredPoint = point - mean;
            var rotatedPoint = invRotation.Transform(centeredPoint);
            min = min.Min(rotatedPoint);
            max = max.Max(rotatedPoint);
        }

        // Compute scale and center in rotated space
        var scale = max - min;
        var rotatedCenter = (max + min) * 0.5f;

        // Transform center back to original space
        var obbCenter = rotation.Transform(rotatedCenter) + mean;

        // Extract rotation as a quaternion
        var rotationQuat = rotation.QuaternionFromRotationMatrix;

        return new Transform3D(obbCenter, rotationQuat, scale);
    }

    // Jacobi eigenvalue algorithm for symmetric 3x3 matrices
    public static void JacobiEigenvalue(double[,] a, out double[] d, out double[,] v)
    {
        int n = 3;
        d = new double[n];
        v = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            v[i, i] = 1.0;
            for (int j = 0; j < n; j++)
                if (i != j)
                    v[i, j] = 0.0;
            d[i] = a[i, i];
        }

        const int maxIterations = 50;
        double[] b = new double[n];
        double[] z = new double[n];
        for (int i = 0; i < n; i++)
        {
            b[i] = d[i];
            z[i] = 0.0;
        }

        for (int iter = 0; iter < maxIterations; iter++)
        {
            double sm = 0.0;
            for (int p = 0; p < n - 1; p++)
                for (int q = p + 1; q < n; q++)
                    sm += Math.Abs(a[p, q]);
            if (sm == 0.0)
                break;

            double tresh = (iter < 3) ? (0.2 * sm / (n * n)) : 0.0;
            for (int p = 0; p < n - 1; p++)
            {
                for (int q = p + 1; q < n; q++)
                {
                    double g = 100.0 * Math.Abs(a[p, q]);
                    if (iter > 3 && (Math.Abs(d[p]) + g == Math.Abs(d[p])) && (Math.Abs(d[q]) + g == Math.Abs(d[q])))
                        a[p, q] = 0.0;
                    else if (Math.Abs(a[p, q]) > tresh)
                    {
                        double h = d[q] - d[p];
                        double t;
                        if (Math.Abs(h) + g == Math.Abs(h))
                            t = (a[p, q]) / h;
                        else
                        {
                            double theta = 0.5 * h / a[p, q];
                            t = 1.0 / (Math.Abs(theta) + Math.Sqrt(1.0 + theta * theta));
                            if (theta < 0.0)
                                t = -t;
                        }
                        double c = 1.0 / Math.Sqrt(1 + t * t);
                        double s = t * c;
                        double tau = s / (1.0 + c);
                        h = t * a[p, q];
                        z[p] -= h;
                        z[q] += h;
                        d[p] -= h;
                        d[q] += h;
                        a[p, q] = 0.0;
                        for (int j = 0; j < p; j++)
                            Rotate(a, s, tau, j, p, j, q);
                        for (int j = p + 1; j < q; j++)
                            Rotate(a, s, tau, p, j, j, q);
                        for (int j = q + 1; j < n; j++)
                            Rotate(a, s, tau, p, j, q, j);
                        for (int j = 0; j < n; j++)
                            Rotate(v, s, tau, j, p, j, q);
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                b[i] += z[i];
                d[i] = b[i];
                z[i] = 0.0;
            }
        }

        // Sort eigenvalues and eigenvectors in descending order
        for (int i = 0; i < n - 1; i++)
        {
            int maxIdx = i;
            for (int j = i + 1; j < n; j++)
                if (d[j] > d[maxIdx])
                    maxIdx = j;
            if (maxIdx != i)
            {
                // Swap eigenvalues
                double tmp = d[i];
                d[i] = d[maxIdx];
                d[maxIdx] = tmp;
                // Swap eigenvectors
                for (int k = 0; k < n; k++)
                {
                    tmp = v[k, i];
                    v[k, i] = v[k, maxIdx];
                    v[k, maxIdx] = tmp;
                }
            }
        }
    }

    private static void Rotate(double[,] a, double s, double tau, int i, int j, int k, int l)
    {
        double g = a[i, j];
        double h = a[k, l];
        a[i, j] = g - s * (h + g * tau);
        a[k, l] = h + s * (g - h * tau);
    }
}
*/
    public static class TransformedBoundingBox
   {
       public static Transform3D ComputeBestFitBoundingBox(this IEnumerable<Vector3D> points)
           => ComputeBestFitBoundingBox(points.ToArray());
   
       public static Transform3D ComputeBestFitBoundingBox(this Vector3D[] points)
       {
           // Compute the mean of the points
           var mean = Vector3D.Default;
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
        var rotation =  new Matrix4x4(
               (u.X, v.X, w.X, 0),
               (u.Y, v.Y, w.Y, 0),
               (u.Z, v.Z, w.Z, 0),
               (0, 0, 0, 1));
   
           // Compute inverse rotation (transpose of the rotation matrix)
           var invRotation = rotation.Transpose;
   
           // Transform points to rotated space
           var rotatedPoints = new Vector3D[points.Length];
           var min = Vector3D.MaxValue;
           var max = Vector3D.MinValue;
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
           var rotationQuat = rotation.QuaternionFromRotationMatrix;
   
           // Assemble the TRS structure
           return new Transform3D(obbCenter, rotationQuat, scale);
       }

    // Helper function to compute eigenvalues and eigenvectors of a symmetric 3x3 matrix
    public static void EigenDecompositionSymmetric3x3(
         float m00, float m01, float m02,
         float m11, float m12, float m22,
         out Vector3D[] eigenvectors,
         out float[] eigenvalues)
    {
        // Compute the eigenvalues using the analytical solution
        float p1 = m01 * m01 + m02 * m02 + m12 * m12;
        if (p1 == 0)
        {
            // The matrix is diagonal.
            eigenvalues = new float[] { m00, m11, m22 };
            eigenvectors = new Vector3D[]
            {
                new Vector3D(1, 0, 0),
                new Vector3D(0, 1, 0),
                new Vector3D(0, 0, 1)
            };
            return;
        }

        float q = (m00 + m11 + m22) / 3f;
        float p2 = (m00 - q) * (m00 - q) + (m11 - q) * (m11 - q) + (m22 - q) * (m22 - q) + 2f * p1;
        float p = (float)Math.Sqrt(p2 / 6f);

        // Compute the B matrix
        float invP = 1f / p;
        float b00 = invP * (m00 - q);
        float b01 = invP * m01;
        float b02 = invP * m02;
        float b11 = invP * (m11 - q);
        float b12 = invP * m12;
        float b22 = invP * (m22 - q);

        // Compute the determinant of B
        float detB = b00 * b11 * b22 + 2f * b01 * b12 * b02 - b02 * b02 * b11 - b01 * b01 * b22 - b00 * b12 * b12;
        float r = detB / 2f;

        // Compute the angle phi
        float phi;
        const float pi = (float)Math.PI;
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
        float eig1 = q + 2f * p * (float)Math.Cos(phi);
        float eig2 = q + 2f * p * (float)Math.Cos(phi + (2f * pi / 3f));
        float eig3 = q + 2f * p * (float)Math.Cos(phi + (4f * pi / 3f));

        eigenvalues = new float[] { eig1, eig2, eig3 };

        // Compute the eigenvectors
        eigenvectors = new Vector3D[3];
        eigenvectors[0] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig1);
        eigenvectors[1] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig2);
        eigenvectors[2] = EigenVectorSymmetric3x3(m00, m01, m02, m11, m12, m22, eig3);
    }

    // Helper function to compute an eigenvector for a given eigenvalue
    public static Vector3D EigenVectorSymmetric3x3(
           float m00, float m01, float m02,
           float m11, float m12, float m22,
           float eigenvalue)
       {
           // Adjust the matrix by subtracting the eigenvalue
           var a00 = m00 - eigenvalue;
           var a11 = m11 - eigenvalue;
           var a22 = m22 - eigenvalue;
   
           // Set up the equations
           var row0 = new Vector3D(a00, m01, m02);
           var row1 = new Vector3D(m01, a11, m12);
           var row2 = new Vector3D(m02, m12, a22);
   
           // Use cross products to find the eigenvector
           var v1 = row1.Cross(row0);
           var v2 = row2.Cross(row0);
           var v3 = row2.Cross(row1);
   
           // Select the vector with the largest magnitude
           float len1 = v1.LengthSquared;
           float len2 = v2.LengthSquared;
           float len3 = v3.LengthSquared;
   
           Vector3D v;
           if (len1 >= len2 && len1 >= len3)
           {
               v = v1;
           }
           else if (len2 >= len1 && len2 >= len3)
           {
               v = v2;
           }
           else
           {
               v = v3;
           }
   
           // Normalize the eigenvector
           return v.Normalize;
       }
   }