using System.Collections.Generic;

namespace GraphImpl.Unweighted
{
    public class AdjacentNodes
    {
        private readonly List<Vertex> _adjacencies;
        private int _newIndex;
        private Coordinate _sourceCoordinate;
        private int _sourceIndex;

        public AdjacentNodes(int sourceIndex)
        {
            _adjacencies = new List<Vertex>();
            _sourceIndex = sourceIndex;
        }

        public void addAdjacentVertex(Vertex vertex)
        {
            _adjacencies.Add(vertex);
        }

        public List<Vertex> GetAdjacencies()
        {
            return _adjacencies;
        }

        public Coordinate GetSourceCoordinates()
        {
            return _sourceCoordinate;
        }
    }
}