using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blocks;
    private Board board;
    private int blockCount = 0;
    void Start()
    {
        SetupBlocks();
        this.board = GameObject.FindFirstObjectByType<Board>();
    }
    public void SetupBlocks()
    {
        float blocksWith = (float)Board.Size/this.blocks.Length;
        float cellSizeScale = (float)Board.Size/(Block.Size * this.blocks.Length + this.blocks.Length + 1);
        for (int i = 0; i < this.blocks.Length; i++)
        {
            this.blocks[i].transform.position = new(blocksWith * (i + 0.5f), -0.25f - cellSizeScale * 4.0f, 0.0f);
            this.blocks[i].transform.localScale = new(cellSizeScale, cellSizeScale, cellSizeScale);
            this.blocks[i].Initialize();
        }
        Generate();
    }
    public void Generate()
    {
        for (int i = 0;i < this.blocks.Length;i++)
        {
            int index = Random.Range(0, Polyominos.Length());
            this.blocks[i].gameObject.SetActive(true);
            this.blocks[i].Show(index);
            this.blockCount++;
        }
    }
    public void Remove()
    {
        this.blockCount--;
        if (this.blockCount <= 0)
        {
            this.blockCount = 0;
            Generate();
        }    
    }
    public void CheckCanPlace()
    {
        int count = 0;
        foreach (var block in this.blocks)
        {
            if (this.board.CanPlace(block.PolyominoIndex) && block.gameObject.activeInHierarchy)
                count++;
        }
        //GAME OVER
        if (count == 0)
            EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWGAMEOVER);
    }   
    public void Retry()
    {
        this.blockCount = 0;
        Generate();
    }
}
