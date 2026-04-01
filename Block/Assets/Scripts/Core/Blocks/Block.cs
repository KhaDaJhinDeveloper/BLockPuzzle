using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public const int Size = 5;
    [SerializeField] private Cell cellPrefab;
    private Cell[,] cells = new Cell[Size, Size];
    #region MousePoint    
    private Camera mainCamera;
    private Vector3 mousePosition = Vector3.positiveInfinity;
    private Vector3 currentMousePosition;
    private Vector3 inputPoint;
    #endregion

    private Vector3 pos;
    private Vector3 posscale;

    #region DragPoint   
    private Vector2 dragPoint;
    private Vector2Int currentDragPoint;
    private Vector3 center;
    #endregion
    #region HoverDragBoard
    private Board board;
    private int polyominoIndex;
    #endregion
    private Blocks blocks;

    public int PolyominoIndex { get => polyominoIndex;}

    private void Awake()
    {
        this.mainCamera = Camera.main;
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.blocks = GameObject.FindFirstObjectByType<Blocks>();
    }
    public void Initialize()
    {
        for (int i = 0; i < Size; i++)
        {
            for(int j = 0; j < Size; j++)
            {
                this.cells[i ,j] = Instantiate(this.cellPrefab, transform);
            }    
        }       
        this.pos = transform.position;
        this.posscale = transform.localScale;
    }
    public void Show(int polyomihnoIndex)
    {
        this.polyominoIndex = polyomihnoIndex;
        Hide();
        int[,] polyomino = Polyominos.Get(polyomihnoIndex);
        int polyominoRow = polyomino.GetLength(0);
        int polyominoCol = polyomino.GetLength(1);
        this.center = new Vector2(polyominoCol * 0.5f, polyominoRow  *0.5f);
        for (int i = 0;i < polyominoRow; i++)
        {
            for( int j = 0;j < polyominoCol; j++)
            {
                if (polyomino[i,j] > 0)
                {
                    this.cells[i, j].transform.localPosition = new(j - center.x, -(i - center.y + 1.0f) , 0);
                    this.cells[i, j].Normal();
                }    
            }    
        }            
    }    
    public void Hide()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                this.cells[i, j].Hide();
            }
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("mousedown");
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_BlockDown);
        transform.localPosition = this.pos + new Vector3 (0, 2.0f, 0);
        transform.localScale = Vector3.one;
        this.mousePosition = Input.mousePosition;
        this.inputPoint = this.mainCamera.ScreenToWorldPoint (this.mousePosition);
        this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
        this.dragPoint = this.currentDragPoint;
        this.board.HoverBoard(this.currentDragPoint, polyominoIndex);
    }
    private void OnMouseDrag()
    {
        Debug.Log("mousedrag");
        this.currentMousePosition = Input.mousePosition;
        if ( this.currentMousePosition != this.mousePosition )
        {
            this.mousePosition = this.currentMousePosition;
            var inputDelta = this.mainCamera.ScreenToWorldPoint(this.mousePosition) - this.inputPoint;
            transform.localPosition = this.pos + inputDelta * 1.2f + new Vector3(0, 2.0f, 0);
            this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
            if (this.currentDragPoint != this.dragPoint)
            {
                this.dragPoint = this.currentDragPoint;
                this.board.HoverBoard(this.currentDragPoint, polyominoIndex);
            }
        }
    }
    private void OnMouseUp()
    {
        Debug.Log("mouseup");
        this.mousePosition = Vector3.positiveInfinity;
        this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
        if (this.board.IsPlace(this.currentDragPoint, this.polyominoIndex))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_PlaceBlock);
            gameObject.SetActive(false);
            this.blocks.Remove();
            this.blocks.CheckCanPlace();
        }
        transform.position = this.pos;
        transform.localScale = this.posscale;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_BlockDown);
        transform.localPosition = this.pos + new Vector3(0, 2.0f, 0);
        transform.localScale = Vector3.one;
        this.mousePosition = eventData.position;
        this.inputPoint = this.mainCamera.ScreenToWorldPoint(this.mousePosition);
        this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
        this.dragPoint = this.currentDragPoint;
        this.board.HoverBoard(this.currentDragPoint, polyominoIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        this.currentMousePosition = eventData.position;
        if ( this.currentMousePosition != this.mousePosition )
        {
            this.mousePosition = this.currentMousePosition;
            var inputDelta = this.mainCamera.ScreenToWorldPoint(this.mousePosition) - this.inputPoint;
            transform.localPosition = this.pos + inputDelta * 1.2f + new Vector3(0, 2.0f, 0);
            this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
            if (this.currentDragPoint != this.dragPoint)
            {
                this.dragPoint = this.currentDragPoint;
                this.board.HoverBoard(this.currentDragPoint, polyominoIndex);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        this.mousePosition = Vector3.positiveInfinity;
        this.currentDragPoint = Vector2Int.RoundToInt(this.transform.position - this.center);
        if (this.board.IsPlace(this.currentDragPoint, this.polyominoIndex))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_PlaceBlock);
            gameObject.SetActive(false);
            this.blocks.Remove();
            this.blocks.CheckCanPlace();
        }
        transform.position = this.pos;
        transform.localScale = this.posscale;
    }
}
