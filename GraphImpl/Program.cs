using System.Collections.Generic;
using GraphImpl.Unweighted;

namespace GraphImpl
{
    public class Program
    {
        //  const int gridXSize = 40;
        //  const int gridYSize = 18;
        private const int gridXSize = 4;
        private const int gridYSize = 4;
        private const string wallMarker = "X";
        private const string startMarker = "S";
        private const string destinationMarker = "D";
        private const string emptyMarker = ".";
        private const string validMarker = "Valid";
        private const string visitedMarker = "V";
        private const string inValidMarker = "B";

        public static Coordinate findCoordinate(int input, int gridXSize, int gridYSize)
        {
            return new Coordinate(input % gridXSize, input / gridXSize);
        }

        public static int findIndexByCoordinate(Coordinate input, int gridXSize, int gridYSize)
        {
            return input.x + gridXSize * input.y;
        }

        public static string checkIfCoordinatesAreValid(Dictionary<int, string> maze, int x, int y)
        {
            var gridSize = maze.Count;
            var myCoordinate = new Coordinate(x, y);
            var dft = myCoordinate.x;
            var dfl = myCoordinate.y;


            var myIndex = findIndexByCoordinate(myCoordinate, gridXSize, gridYSize);

            if (myCoordinate.x < 0 ||
                myCoordinate.x >= gridXSize ||
                myCoordinate.y < 0 ||
                myCoordinate.y >= gridYSize)
                // location is not on the grid--return false
                return inValidMarker;
            if (maze[myIndex] == destinationMarker)
                return destinationMarker;
            if (maze[myIndex] != emptyMarker)
                // location is either an obstacle or has been visited
                return wallMarker;
            return validMarker;
        }

        public static void Main(string[] args)
        {
            var startingPosition = 0;
            var destinationPosition = 12;
            var wall1 = 4;
            var wall2 = 5;
            var wall3 = 6;
            var wall4 = 13;
            var wall5 = 14;
            var wall6 = 15;

            var maze = new Dictionary<int, string>();

            // populate the 2d grid with ".", "W", "S", "D"

            // 1 - populate with "." (empty)
            var counter = 0;
            for (var x = 0; x < gridXSize; x++)
            for (var y = 0; y < gridYSize; y++)
            {
                maze.Add(counter, emptyMarker);
                counter += 1;
            }

            // add walls and stuff...

            // insert "S" as a marker for a starting position
            if (maze.ContainsKey(startingPosition)) maze[startingPosition] = startMarker;

            // insert "D" as a marker for a destination position
            if (maze.ContainsKey(destinationPosition)) maze[destinationPosition] = destinationMarker;

            // insert "X" as a marker for a wall
            maze[wall1] = wallMarker;
            maze[wall2] = wallMarker;
            maze[wall3] = wallMarker;
            maze[wall4] = wallMarker;
            maze[wall5] = wallMarker;
            maze[wall6] = wallMarker;

            // the "maze" has now been completed

            // go through the maze, starting at top row/top left aka 0.0 and start adding vertices
            // a vertex is to be added if its index translated into coordinates is in the grid (valid)
            // and if it is not a wall
            // then its neighbours need to be evaluated and added as well

            var pathList = new List<string>();
            var q = new Queue<Vertex>();

            var myIndex = 0;
            var myCoordinates = findCoordinate(myIndex, gridXSize, gridYSize);
            var originatingVertex = new Vertex(myCoordinates);
            var finalListOfVertices = new List<Vertex>();

            q.Enqueue(originatingVertex);


            while (q.Count > 0)
            {
                var localVertexFromQ = q.Dequeue();

                myCoordinates = findCoordinate(localVertexFromQ.GetIndex(gridXSize), gridXSize, gridYSize);

                var adjNodes = new AdjacentNodes(localVertexFromQ.GetIndex(gridXSize));

                // Explore North
                var myCoordinate = localVertexFromQ.getCoordinates();
                var northernCoordinateY = myCoordinate.y - 1;
                var northernCoordinateX = myCoordinate.x;

                var isNorthernValid = checkIfCoordinatesAreValid(maze, northernCoordinateX, northernCoordinateY);

                var adjacentVertex = new Vertex(new Coordinate(northernCoordinateX, northernCoordinateY));

                switch (isNorthernValid)
                {
                    case "B":

                        break;
                    case "Valid":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        pathList.Add("N");
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        break;
                    case "X":
                        // do nothing, its a wall...
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "S":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "D":
                        // need to return the path taken
                        pathList.Add("N");
                        break;

                    case ".":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        pathList.Add("N");
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        break;

                    case "V":
                        // do nothing, it has been visited already...
                        break;
                }

                // Explore West
                var westernCoordinateX = myCoordinates.x - 1;
                var westernCoordinateY = myCoordinates.y;
                var isWesternValid = checkIfCoordinatesAreValid(maze, westernCoordinateX, westernCoordinateY);

                adjacentVertex = new Vertex(new Coordinate(westernCoordinateX, westernCoordinateY));

                switch (isWesternValid)
                {
                    case "B":

                        break;
                    case "Valid":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);
                        pathList.Add("W");

                        break;
                    case "X":
                        // do nothing, its a wall...
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "S":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "D":
                        // need to return the path taken
                        pathList.Add("W");
                        break;

                    case ".":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        pathList.Add("W");
                        break;
                    case "V":

                        break;
                }


                // Explore South
                var southCoordinateY = myCoordinates.y + 1;
                var southCoordinateX = myCoordinates.x;
                var issouthValid = checkIfCoordinatesAreValid(maze, southCoordinateX, southCoordinateY);

                adjacentVertex = new Vertex(new Coordinate(southCoordinateX, southCoordinateY));

                switch (issouthValid)
                {
                    case "B":

                        break;
                    case "Valid":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        pathList.Add("S");
                        break;
                    case "X":
                        // do nothing, its a wall...
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "S":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "D":
                        // need to return the path taken
                        pathList.Add("S");
                        break;

                    case ".":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        pathList.Add("S");
                        break;
                    case "V":

                        break;
                }


                // Explore East
                var easternCoordinateX = myCoordinates.x + 1;
                var easternCoordinateY = myCoordinates.y;
                var isEasternValid = checkIfCoordinatesAreValid(maze, easternCoordinateX, easternCoordinateY);

                adjacentVertex = new Vertex(new Coordinate(easternCoordinateX, easternCoordinateY));

                switch (isEasternValid)
                {
                    case "B":

                        break;
                    case "Valid":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        pathList.Add("E");
                        break;
                    case "X":
                        // do nothing, its a wall...
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "S":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        break;
                    case "D":
                        // need to return the path taken
                        pathList.Add("E");
                        break;

                    case ".":
                        maze[adjacentVertex.GetIndex(gridXSize)] = visitedMarker;
                        q.Enqueue(adjacentVertex);
                        adjNodes.addAdjacentVertex(adjacentVertex);

                        pathList.Add("E");
                        break;
                    case "V":

                        break;
                }

                localVertexFromQ.addAdjacentNodes(adjNodes);
                finalListOfVertices.Add(localVertexFromQ);

                var r = pathList.ToArray();
            }
        }
    }
}