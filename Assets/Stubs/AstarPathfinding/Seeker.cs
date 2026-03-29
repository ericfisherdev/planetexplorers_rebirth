// Stub: Pathfinding.Seeker
// MonoBehaviour component required by PEPathfinder. Provides path request delegation.

using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// Delegate type for path completion callbacks.
    /// </summary>
    public delegate void OnPathDelegate(Path path);

    /// <summary>
    /// Seeker MonoBehaviour. Attached to pathfinding agents to request and manage paths.
    /// Game code uses pathCallback += OnPathComplete and StartPath(start, end).
    /// </summary>
    public class Seeker : MonoBehaviour
    {
        public OnPathDelegate pathCallback;

        public Path StartPath(Vector3 start, Vector3 end)
        {
            return new ABPath();
        }

        public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
        {
            return new ABPath();
        }

        public Path GetCurrentPath()
        {
            return null;
        }
    }

    /// <summary>
    /// Path modifier for smoothing waypoints. Required by PEPathfinder via [RequireComponent].
    /// </summary>
    public class SimpleSmoothModifier : MonoBehaviour
    {
    }
}
