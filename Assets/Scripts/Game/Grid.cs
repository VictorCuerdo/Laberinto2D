using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    private int obstacleNumber;
    private int width;
    private int height;
    private int cellSize;
    private Cell cellPrefab;
    private Cell obstaclePrefab;
    private Cell[,] gridArray;
    private GameObject padre;


    public Grid(int width, int height, int cellSize, Cell cellPrefab, Cell obstaclePrefab, int obstacleNumber, GameObject padre)
    {

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;
        this.obstaclePrefab = obstaclePrefab;
        this.obstacleNumber = obstacleNumber;
        this.padre = padre;
        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];
        for (int i = 0; i < obstacleNumber; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (gridArray[x, y] == null && !(x==0 && y==0))
            {
                var p = new Vector2(x, y) * cellSize;
                cell = Instantiate(obstaclePrefab, p, Quaternion.identity);
                cell.transform.SetParent(padre.transform);
                cell.Init(this, (int)p.x, (int)p.y, true);
                cell.SetWalkable(false);
                gridArray[x, y] = cell;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i, j) * cellSize;
                
                if (gridArray[i, j] == null) {
                    cell = Instantiate(cellPrefab, p, Quaternion.identity);
                    cell.transform.SetParent(padre.transform);
                    cell.Init(this, (int)p.x, (int)p.y, true);
                    cell.SetWalkable(true);
                    gridArray[i, j] = cell;
                }
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
        Camera.main.orthographicSize = ((Mathf.Max(height, width) / 2) + 3)/2;
    }

    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }



    public Cell GetGridObject(int x, int y)
    {
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }
}
