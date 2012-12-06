﻿using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace DrawingSpace
{
    static class DrawingTools
    {
        /// <summary>
        /// Moves an object in the drawing.
        /// </summary>
        public static void Move(Entity entity, Point3d fromPoint, Point3d toPoint)
        {
            Vector3d moveVector = new Vector3d(toPoint.X - fromPoint.X, toPoint.Y - fromPoint.Y,
                toPoint.Z - fromPoint.Z);
            Matrix3d moveMatrix = Matrix3d.Displacement(moveVector);

            entity.TransformBy(moveMatrix);
        }

        public enum Axis { X, Y, Z }

        /// <summary>
        /// Rotates an object in the drawing.
        /// </summary>
        /// <param name="rotationAngle">Angle in decimal degrees.</param>
        /// <remarks>This method only performs a 2D rotation around the specified axis.</remarks>
        public static void Rotate(Entity entity, Point3d basePoint, double rotationAngle, Axis rotationAxis)
        {
            // Default case is rotation around de Z-axis.
            Vector3d rotateVector = new Vector3d(0, 0, 1);

            switch (rotationAxis)
            {
                case Axis.X:
                    rotateVector = new Vector3d(1, 0, 0);
                    break;
                case Axis.Y:
                    rotateVector = new Vector3d(0, 1, 0);
                    break;
            }

            Matrix3d rotateMatrix = Matrix3d.Rotation(rotationAngle * Math.PI / 180,
                rotateVector, basePoint);

            entity.TransformBy(rotateMatrix);
        }


        /// <summary>
        /// Scales an object in the drawing.
        /// </summary>
        public static void Scale(Entity entity, Point3d basePoint, double scale)
        {
            Matrix3d scaleMatrix = Matrix3d.Scaling(scale, basePoint);
            entity.TransformBy(scaleMatrix);
        }
    }
}