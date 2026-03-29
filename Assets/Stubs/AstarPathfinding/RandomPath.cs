// Stub: Pathfinding.RandomPath
// 6 RandomPath.Construct references. Generates a random path from a start position.

using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// Random wandering path. Game code calls RandomPath.Construct(start, length, callback).
    /// Used by NpcMgr for NPC idle wandering behavior.
    /// </summary>
    public class RandomPath : ABPath
    {
        public Vector3 aim;
        public int spread;

        public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback)
        {
            return new RandomPath { startPoint = start };
        }
    }
}
