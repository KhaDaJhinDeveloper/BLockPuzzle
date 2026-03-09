using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Board : MonoBehaviour
{
    [SerializeField] private Cell cellPrefabs;
    [SerializeField] private Transform cellsTransform;
    public const int Size = 8;
    private Cell[,] cells = new Cell[Size, Size];
    public int[,] data = new int[Size, Size]; 
    private List<Vector2Int> hoverPoints = new ();

    private HashSet<int> fullLineColumns = new ();
    private HashSet<int> fullLineRows = new ();
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        for (int i = 0; i < Size; i++)
        {
            for(int j = 0;  j < Size; j++)
            {
                this.cells[i, j] = Instantiate(this.cellPrefabs, this.cellsTransform);
                this.cells[i, j].transform.position = new(i , j , 0.0f);
                this.cells[i, j].Hide();
            }
        }             
    }
    public void HoverBoard(Vector2Int point, int polyominoIndex)
    {
        int[,] polyomino = Polyominos.Get(polyominoIndex);
        int polyominoRow = polyomino.GetLength(0);
        int polyominoCol = polyomino.GetLength(1);
        UnHover();
        GethoverPoint(point, polyominoRow, polyominoCol, polyomino);
        if (hoverPoints.Count > 0) Hover();
    }
    private void GethoverPoint(Vector2Int point, int polyominRows, int polyomiColums, int[,] polyominos)
    {
        for (int i = 0; i < polyominRows; i++)
        {
            for (int j = 0; j < polyomiColums; j++)
            {
                if (polyominos[i, j] > 0)
                {
                    Vector2Int hoverpoint = point + new Vector2Int( j, -i + 3);
                    if (!IsValidPoint(hoverpoint))
                    {
                        this.hoverPoints.Clear();
                        return;
                    }
                    this.hoverPoints.Add(hoverpoint);
                }
            }
        }
    }
    private bool IsValidPoint(Vector2Int point)
    {
        if (point.x < 0 || Size <= point.x) return false;
        if (point.y < 0 || Size <= point.y) return false;
        if (this.data[point.y, point.x] > 0) return false;
        return true;
    }
    private void Hover()
    {
        foreach (var hoverPoint in this.hoverPoints)
        {
            this.data[hoverPoint.y, hoverPoint.x] = 1;
            this.cells[hoverPoint.x, hoverPoint.y].Hover();
        }
    }
    private void UnHover()
    {
        foreach (var hoverPoint in this.hoverPoints)
        {
            this.data[hoverPoint.y, hoverPoint.x] = 0;
            this.cells[hoverPoint.x, hoverPoint.y].Hide();
        }
        this.hoverPoints.Clear();
    }
    public bool IsPlace(Vector2Int point, int polyominoIndex )
    {
        int[,] polyomino = Polyominos.Get(polyominoIndex);
        int polyominoRow = polyomino.GetLength(0);
        int polyominoCol = polyomino.GetLength(1);
        UnHover();
        GethoverPoint(point, polyominoRow, polyominoCol, polyomino);
        if (hoverPoints.Count > 0)
        {
            Place(point);
            return true;
        }
        return false;
    }
    private List<Vector2Int> Place(Vector2Int point)
    {
        List<Vector2Int> placedPoint = new (this.hoverPoints);
        foreach (var hoverPoint in this.hoverPoints)
        {
            this.data[hoverPoint.y, hoverPoint.x] = 2;
            this.cells[hoverPoint.x, hoverPoint.y].Normal();
        }
        CheckAndClear(placedPoint);
        this.hoverPoints.Clear();
        return placedPoint;
    }
    private void CheckAndClear(List<Vector2Int> placedPoint)
    {
        foreach(Vector2Int point in placedPoint)
        {
            this.fullLineRows.Add(point.x);
            this.fullLineColumns.Add(point.y);
        } 
        foreach(int row in this.fullLineRows)
        {
            if(IsRowFull(row))
            {
                ClearRow(row);
            }
        }
        foreach (int col in this.fullLineColumns)
        {
            if (IsColumnFull(col))
            {
                ClearColumn(col);
            }
        }
    }
    private bool IsRowFull(int row)
    {
        for (int col = 0; col < Size; col++)
        {
            if (this.data[row, col] != 2) return false;
        }
        return true;
    }    
    private bool IsColumnFull(int col)
    {
        for (int row = 0; row < Size; row++)
        {
            if (this.data[row, col] != 2) return false;
        }
        return true;
    }
    private void ClearRow(int row)
    {
        for (int col = 0; col < Size; col++)
        {
            this.data[row, col] = 0;
            this.cells[col, row].Hide();
        }
    }
    private void ClearColumn(int col)
    {
        for (int row = 0; row < Size; row++)
        {
            this.data[row, col] = 0;
            this.cells[col, row].Hide();
        }
    }
}
