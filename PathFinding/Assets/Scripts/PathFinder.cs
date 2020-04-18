using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder
{
    public static TilePath DiscoverPath(Tilemap map, Vector3Int start, Vector3Int end)
    {
        //you will return this path to the user.  It should be the shortest path between
        //the start and end vertices 
        TilePath discoveredPath = new TilePath();

        //TileFactory is how you get information on tiles that exist at a particular vector's
        //coordinates
        TileFactory tileFactory = TileFactory.GetInstance();

        //This is the priority queue of paths that you will use in your implementation of
        //Dijkstra's algorithm
        PriortyQueue<TilePath> pathQueue = new PriortyQueue<TilePath>();

        //You can slightly speed up your algorithm by remembering previously visited tiles.
        //This isn't strictly necessary.
        Dictionary<Vector3Int, int> discoveredTiles = new Dictionary<Vector3Int, int>();

        //quick sanity check
        if(map == null || start == null || end == null)
        {
            return discoveredPath;
        }

        //This is how you get tile information for a particular map location
        //This gets the Unity tile, which contains a coordinate (.Position)
        var startingMapLocation = map.GetTile(start);

        //And this converts the Unity tile into an object model that tracks the
        //cost to visit the tile.
        var startingTile = tileFactory.GetTile(startingMapLocation.name);
        startingTile.Position = start;

        //Any discovered path must start at the origin!
        discoveredPath.AddTileToPath(startingTile);

        //This adds the starting tile to the PQ and we start off from there...
        pathQueue.Enqueue(discoveredPath);
        bool found = false;
        while(found == false && pathQueue.IsEmpty() == false)
        {
            //TODO: Implement Dijkstra's algorithm!
            var path_ = new TilePath (pathQueue.Dequeue());
            Tile recent = new Tile(path_.GetMostRecentTile());
            if (recent.Position == end)
            {
                return path_;
            }
            else
            {
                TilePath tempL = new TilePath(path_);
                Tile nextTile = new Tile(recent);
                TileBase up = map.GetTile(new Vector3Int(recent.Position.x, recent.Position.y + 1, recent.Position.z));
                Tile uptemp = new Tile(tileFactory.GetTile(up.name));
                TileBase down = map.GetTile(new Vector3Int(recent.Position.x, recent.Position.y - 1, recent.Position.z));
                Tile downtemp = new Tile(tileFactory.GetTile(down.name));
                TileBase left = map.GetTile(new Vector3Int(recent.Position.x - 1, recent.Position.y, recent.Position.z));
                Tile lefttemp = new Tile(tileFactory.GetTile(left.name));
                TileBase right = map.GetTile(new Vector3Int(recent.Position.x + 1, recent.Position.y, recent.Position.z));
                Tile righttemp = new Tile(tileFactory.GetTile(right.name));





                if (up != null)
                {
                    Tile upT = new Tile();
                    upT.Position = new Vector3Int(recent.Position.x, recent.Position.y + 1, recent.Position.z);
                    upT.Weight = uptemp.Weight;
                    upT.Name = uptemp.Name;
                    tempL.AddTileToPath(upT);
                    if(upT.Position == end)
                    {
                        return tempL;
                    }
                    pathQueue.Enqueue(tempL);
                    tempL = new TilePath(path_);
                }
                if (down != null)
                {
                    Tile downT = new Tile();
                    downT.Position = new Vector3Int(recent.Position.x, recent.Position.y - 1, recent.Position.z);
                    downT.Weight = downtemp.Weight;
                    downT.Name = downtemp.Name;
                    tempL.AddTileToPath(downT);
                    if (downT.Position == end)
                    {
                        return tempL;
                    }
                    pathQueue.Enqueue(tempL);
                    tempL = new TilePath(path_);
                }
                if (left != null)
                {
                    Tile leftT = new Tile();
                    leftT.Position = new Vector3Int(recent.Position.x - 1, recent.Position.y, recent.Position.z);
                    leftT.Weight = lefttemp.Weight;
                    leftT.Name = lefttemp.Name;
                    tempL.AddTileToPath(leftT);
                    if (leftT.Position == end)
                    {
                        return tempL;
                    }
                    pathQueue.Enqueue(tempL);
                    tempL = new TilePath(path_);
                }
                if (right != null)
                {
                    Tile rightT = new Tile();
                    rightT.Position = new Vector3Int(recent.Position.x + 1, recent.Position.y, recent.Position.z);
                    rightT.Weight = righttemp.Weight;
                    rightT.Name = righttemp.Name;
                    tempL.AddTileToPath(rightT);
                    if (rightT.Position == end)
                    {
                        return tempL;
                    }
                    pathQueue.Enqueue(tempL);
                }
            }

            //This line ensures that we don't get an infinite loop in Unity.
            //You will need to remove it in order for your pathfinding algorithm to work.
        }
        return discoveredPath;
    }
}
