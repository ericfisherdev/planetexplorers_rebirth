// Stub: Pathfinding.Path, ABPath, NNConstraint, PathNNConstraint, GraphUpdateObject
// Path.vectorPath is the primary game code access pattern (List<Vector3> of waypoints).

using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// Base path class. Game code checks .error, reads .vectorPath for waypoints.
    /// </summary>
    public class Path
    {
        public List<Vector3> vectorPath = new List<Vector3>();
        public bool error;
        public string errorLog = "";

        public void Claim(object claimer) { }
        public void Release(object claimer) { }

        public float GetTotalLength()
        {
            return 0f;
        }
    }

    /// <summary>
    /// A-to-B path. Used via ABPath.Construct(start, end, callback).
    /// </summary>
    public class ABPath : Path
    {
        public Vector3 startPoint;
        public Vector3 endPoint;

        public static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback)
        {
            return new ABPath { startPoint = start, endPoint = end };
        }
    }

    /// <summary>
    /// Constraint for nearest-node queries.
    /// </summary>
    public class NNConstraint
    {
        public static readonly NNConstraint Default = new NNConstraint();
        public static readonly NNConstraint None = new NNConstraint();
    }

    /// <summary>
    /// Path-specific nearest-node constraint.
    /// </summary>
    public class PathNNConstraint : NNConstraint
    {
        public new static readonly PathNNConstraint Default = new PathNNConstraint();
    }

    /// <summary>
    /// Describes a graph update for dynamic obstacles. Constructed with a Bounds.
    /// </summary>
    public class GraphUpdateObject
    {
        public Bounds bounds;

        public GraphUpdateObject(Bounds bounds)
        {
            this.bounds = bounds;
        }
    }
}
