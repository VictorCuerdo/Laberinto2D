using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Cell ObstaclePrefab;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject GoalPrefab;
    [SerializeField] private int ObstacleNumber;
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    [SerializeField] private GameObject BorderPrefab;

    private GameObject padre;
    [HideInInspector]
    public Grid grid;
    private Player player;

    private void Awake()
    {
        Instance = this;
        Width = PlayerPrefs.GetInt("size");
        Height = PlayerPrefs.GetInt("size");
        ObstacleNumber = PlayerPrefs.GetInt("obstacles");
    }

    private void Start()
    {
        init(PlayerPrefs.GetInt("level"));
    }
    public Vector2 nextStep(int x, int y, int x2, int y2)
    {
        Cell nextpos = PathManager.Instance.FindPath(grid, x, y, x2, y2)[1];
        return new Vector2(nextpos.x, nextpos.y);
    }
    public void init(int enemyNumber)
    {
        padre = new GameObject("Board");
        grid = new Grid(Width, Height, 1, CellPrefab, ObstaclePrefab, ObstacleNumber, padre);
        while (PathManager.Instance.FindPath(grid, 0, 0, Width - 1, Height - 1) == null)
        {
            Destroy(padre);
            padre = new GameObject("Board");
            grid = new Grid(Width, Height, 1, CellPrefab, ObstaclePrefab, ObstacleNumber, padre);
        }
        for (int i = 0; i < Width; i++)
        {
            Instantiate(BorderPrefab, new Vector3(i, -1, 0), Quaternion.identity, padre.transform);
            Instantiate(BorderPrefab, new Vector3(i, Height, 0), Quaternion.identity, padre.transform);
        }
        for (int i = 0; i < Height; i++)
        {
            Instantiate(BorderPrefab, new Vector3(-1, i, 0), Quaternion.identity, padre.transform);
            Instantiate(BorderPrefab, new Vector3(Width, i, 0), Quaternion.identity, padre.transform);
        }
        for (int i = 0; i < enemyNumber; i++)
        {
            int x = Random.Range(3, Width);
            int y = Random.Range(3, Height);
            if (PathManager.Instance.FindPath(grid, x, y, Width - 1, Height - 1) != null && grid.GetGridObject(x, y).isWalkable)
            {
                Instantiate(EnemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
        player = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
        Instantiate(GoalPrefab, new Vector2(Width-1, Height-1), Quaternion.identity);
    }
}
