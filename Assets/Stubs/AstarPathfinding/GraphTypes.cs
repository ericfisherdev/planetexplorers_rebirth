// Stub: Pathfinding graph infrastructure types
// NavGraph, GridGraph, LayerGridGraph, GraphNode, NNInfo

using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// Base graph type. AstarPath.graphs is an array of NavGraph.
    /// </summary>
    public class NavGraph
    {
        public GraphNode[] nodes = new GraphNode[0];
    }

    /// <summary>
    /// Grid-based navigation graph with width, depth, nodeSize, center.
    /// </summary>
    public class GridGraph : NavGraph
    {
        public int width;
        public int depth;
        public float nodeSize = 1f;
        public Vector3 center;

        public void GenerateMatrix() { }
        public void UpdateSizeFromWidthDepth() { }
    }

    /// <summary>
    /// Layered grid graph supporting multiple walkable layers (caves, bridges).
    /// Game code accesses center, width, depth, GenerateMatrix, UpdateSizeFromWidthDepth.
    /// </summary>
    public class LayerGridGraph : GridGraph
    {
    }

    /// <summary>
    /// Single node in a navigation graph.
    /// </summary>
    public class GraphNode
    {
        public Vector3 position;
        public bool Walkable;
        public uint Tag;
        public uint Penalty;
    }

    /// <summary>
    /// Result of a nearest-node query. Contains the node and position info.
    /// </summary>
    public struct NNInfo
    {
        public GraphNode node;
        public Vector3 position;
        public Vector3 clampedPosition;
    }
}
