using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatryksTilemap : MonoBehaviour
{
    public GameObject selectedUnit;
    public TileType[] tileTypes;

    public Unit[] units;

    int[,] tiles;
    Node[,] graph;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start()
    {
        
        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisual();
        
    }
    void GenerateMapData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for(int x = 0; x < mapSizeX; x++)
        {
            for(int y = 0; y < mapSizeX; y++)
            {
                tiles[x, y] = 0;
            }
        }

        for(int x = 3; x <= 5; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                tiles[x,y] = 1;
            }

        }

        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;

        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;
        }

        class Node {
            public List<Node> neighbours;

            public Node()
            {
                neighbours = new List<Node>(); 
            }
        }

        
        
        void GeneratePathfindingGraph()
        {
            graph = new Node[mapSizeX,mapSizeY];
            for(int x = 0; x < mapSizeX; x++)
        {
            for(int y = 0; y < mapSizeX; y++)
            {
                if(x > 0)
                graph[x,y].neighbours.Add(graph[x-1,y]);
                if(x < mapSizeX-1)
                graph[x,y].neighbours.Add(graph[x+1,y]);
                if(y > 0)
                graph[x,y].neighbours.Add(graph[x,y-1]);
                if(y < mapSizeY-1)
                graph[x,y].neighbours.Add(graph[x,y+1]);

            }
        }
            
        }
    void GenerateMapVisual()
    {
        for(int x = 0; x < mapSizeX; x++)
        {
            for(int y = 0; y < mapSizeX; y++)
            {
                TileType tt = tileTypes[ tiles[x, y]];

               GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

               ClickableTile ct = go.GetComponent<ClickableTile>();
               ct.tileX = x;
               ct.tileY = y;

               ct.map = this;

            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y,0);
    }

    public void MoveSelectedUnitTo(int x, int y)
    {
        selectedUnit.GetComponent<Unit>().tileX = x;
        selectedUnit.GetComponent<Unit>().tileY = y;
        selectedUnit.transform.position = TileCoordToWorldCoord(x, y);
    }
}