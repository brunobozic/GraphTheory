using System.Collections.Generic;

namespace GraphImpl.Unweighted
{
    public class Vertex
    {
        private readonly List<string> _traversedPath = new List<string>();
        private Coordinate _coordinate;
        private int _index;
        private AdjacentNodes _nodes;
        private bool _visited;

        public Vertex(Coordinate coordinate)
        {
            _coordinate = coordinate;
        }

        public Vertex(int index)
        {
            _index = index;
        }

        public void addAdjacentNodes(AdjacentNodes nodes)
        {
            _nodes = nodes;
        }

        public void setIndex(int index)
        {
            _index = index;
        }

        public bool GetVisited()
        {
            return _visited;
        }

        public void setVisited(bool status)
        {
            _visited = status;
        }

        public void setCoordinate(Coordinate coordinate)
        {
            _coordinate = coordinate;
        }

        public AdjacentNodes GetAdjacentNodes()
        {
            return _nodes;
        }

        public Coordinate getCoordinates()
        {
            return _coordinate;
        }

        public int GetIndex(int gridXSize)
        {
            return _coordinate.x + gridXSize * _coordinate.y;
        }

        public void AddTraversedPath(string pathToAppend)
        {
            _traversedPath.Add(pathToAppend);
        }

        public List<string> GetTraversedPath(string pathToAppend)
        {
            return _traversedPath;
        }
    }
}