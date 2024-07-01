using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    Point [] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1,0),
        new Point(-1,0)
    };

    Color selectedTileColor = new Color(0, 1,1,1);
    Color defaultTileColor = new Color(1,1,1,1);
    public Dictionary<Point, Tile2> tiles = new Dictionary<Point, Tile2>();

    public void Load (LevelData data)
    {
        for (int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;

            Tile2 t = instance.GetComponent<Tile2>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }
    public List<Tile2> Search (Tile2 start, Func<Tile2, Tile2, bool> AddTile)
    {
        List<Tile2> retValue = new List<Tile2>();
        retValue.Add(start);
        ClearSearch();
        Queue<Tile2> checkNext = new Queue<Tile2>();
        Queue<Tile2> checkNow = new Queue<Tile2>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            Tile2 t = checkNow.Dequeue();
            for (int i = 0; i < 4; ++i)
            {
                Tile2 next = GetTile(t.pos + dirs[i]);
                if (next == null || next.distance <= t.distance + 1)
                    continue;
                if(AddTile(t, next))
                {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
                if (checkNow.Count == 0)
                    SwapReference(ref checkNow, ref checkNext);
                
            }
        }

        return retValue;
    }

    void SwapReference(ref Queue<Tile2> a, ref Queue<Tile2> b)
    {
        Queue<Tile2> temp = a;
        a = b;
        b = temp;
    }

    public Tile2 GetTile(Point p)
    {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }

    void ClearSearch ()
    {
        foreach (Tile2 t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    public void SelectTiles (List<Tile2> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", selectedTileColor);
    }

    public void DeSelectTiles (List<Tile2> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", defaultTileColor);
    }

}
