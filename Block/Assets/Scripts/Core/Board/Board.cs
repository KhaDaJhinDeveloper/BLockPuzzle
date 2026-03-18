using System.Collections.Generic;
using UnityEngine;

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
    private List<Vector2Int> hoverLinesFull = new();

    private int clearScore = 0;
    private ScoreManager scoreManager;
    void Start()
    {
        Initialize();
        this.scoreManager = GameObject.FindFirstObjectByType<ScoreManager>();
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
#region HoverDrag
    public void HoverBoard(Vector2Int point, int polyominoIndex)
    {
        int[,] polyomino = Polyominos.Get(polyominoIndex);
        int polyominoRow = polyomino.GetLength(0);
        int polyominoCol = polyomino.GetLength(1);
        UnHover();
        GethoverPoint(point, polyominoRow, polyominoCol, polyomino);
        if (hoverPoints.Count > 0)
        {
            Hover();
            HoverFullLines();
        }
    }
    private void GethoverPoint(Vector2Int point, int polyominRows, int polyomiColums, int[,] polyominos)
    {
        for (int i = 0; i < polyominRows; i++)
        {
            for (int j = 0; j < polyomiColums; j++)
            {
                if (polyominos[i, j] > 0)
                {
                    Vector2Int hoverpoint = point + new Vector2Int(j , -i +1);
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
        //HoverNormal
        foreach (var hoverPoint in this.hoverPoints)
        {
            this.data[hoverPoint.y, hoverPoint.x] = 0;
            this.cells[hoverPoint.x, hoverPoint.y].Hide();
        }
        this.hoverPoints.Clear();
        //HoverWhenFull
        foreach (var hoverPoint in this.hoverLinesFull)
        {
            if(data[hoverPoint.y, hoverPoint.x] == 2)
                this.cells[hoverPoint.x, hoverPoint.y].Normal();
            else this.cells[hoverPoint.x, hoverPoint.y].Hide();
        }
        this.hoverLinesFull.Clear();
    }
#endregion
#region Placed_Block
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
    private void Place(Vector2Int point)
    {
        List<Vector2Int> placedPoint = new (this.hoverPoints);
        foreach (var hoverPoint in this.hoverPoints)
        {
            this.data[hoverPoint.y, hoverPoint.x] = 2;
            this.cells[hoverPoint.x, hoverPoint.y].Normal();
        }
        CheckAndClear(placedPoint);
        this.hoverPoints.Clear();
        //return placedPoint;
    }
#endregion
#region ClearRowCol_Logic
    private void CheckAndClear(List<Vector2Int> placedPoint)
    {
        this.fullLineRows.Clear();
        this.fullLineColumns.Clear();

        foreach (Vector2Int point in placedPoint)
        {
            this.fullLineRows.Add(point.y);
            this.fullLineColumns.Add(point.x);
        }
        List<int> rowsToClear = new();
        List<int> colsToClear = new();

        foreach (int row in this.fullLineRows)
        {
            if (IsRowFull(row))
            {
                rowsToClear.Add(row);
                this.clearScore++;
            }

        }
        foreach (int col in this.fullLineColumns)
        {
            if (IsColumnFull(col))
            {
                colsToClear.Add(col);
                this.clearScore++;
            }
        }
        foreach (int row in rowsToClear) ClearRow(row);
        foreach (int col in colsToClear) ClearColumn(col);
        this.scoreManager.IncreaseScore(ScoreAmplification(clearScore));
        clearScore = 0;
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
#endregion
#region HoverWhenFull
    private void HoverFullLines()
    {
        HashSet<int> rowsToCheck = new();
        HashSet<int> colsToCheck = new();
        foreach(Vector2Int point in this.hoverPoints)
        {
            rowsToCheck.Add(point.y);
            colsToCheck.Add(point.x);
        }
        foreach (int row in rowsToCheck)
        {
            if(IsRowFullWithHover(row))
            {
                for (int col = 0; col < Size; col++)
                    this.hoverLinesFull.Add(new Vector2Int(col, row));
            }
        }
        foreach (int col in colsToCheck)
        {
            if (IsColumnFullWithHover(col))
            {
                for (int row = 0; row < Size; row++)
                    this.hoverLinesFull.Add(new Vector2Int(col, row));
            }
        }
        foreach (var p in this.hoverLinesFull)
            cells[p.x, p.y].Hover();
    }
    private bool IsRowFullWithHover(int row)
    {
        for (int col = 0; col < Size; col++)
        {
            if (this.data[row, col] == 0) return false;
        }
        return true;
    }
    private bool IsColumnFullWithHover(int col)
    {
        for (int row = 0; row < Size; row++)
        {
            if (this.data[row, col] == 0) return false;
        }
        return true;
    }
#endregion
#region CheckGameOver
    public bool CanPlace(int polyominosIndex)
    {
        int[,] polyominos = Polyominos.Get(polyominosIndex);
        int row = polyominos.GetLength(0);
        int col = polyominos.GetLength(1);
        List<Vector2Int> result = new();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if(TryGetPoints(new Vector2Int(i, j), row, col, polyominos, result))
                    return true;
            }
        }
        return false;
    }
    private bool TryGetPoints(Vector2Int point ,int polyominosRow, int polyominosColumn, int[,] polyominos , List<Vector2Int> result)
    {
        for (int i = 0; i < polyominosRow; i++)
        {
            for (int j = 0; j < polyominosColumn; j++)
            {
                if (polyominos[i, j] > 0)
                {
                    Vector2Int hoverpoint = point + new Vector2Int(j, -i + 1);
                    if(!IsValidPoint(hoverpoint)) return false;
                    result.Add(hoverpoint);
                }
            }
        }
        return result.Count > 0;
    }
    #endregion
#region Score
    private int ScoreAmplification(int score)
    {
        switch (score)
        {
            case 0:
                return 0;
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 20;
            case 4:
                return 30;
            case 5:
                return 50;
            case 6:
                return 100;
            default: return 0;
        }
    }    
#endregion
    public void Retry()
    {
        this.hoverPoints.Clear();
        this.hoverLinesFull.Clear();
        this.fullLineColumns.Clear();
        this.fullLineRows.Clear();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                this.data[j, i] = 0;
                this.cells[i, j].Hide();
            }
        }
    }
}
