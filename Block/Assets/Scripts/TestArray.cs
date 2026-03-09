using UnityEngine;

public class TestArray : MonoBehaviour
{
    private int[][,] polyominos = new int[][,]
    {
        new int[,]
        {
            //{1, 0 },
            {1, 0 },
            {1, 0 },
            {1, 0 }
        }
    };
    private int[,] polyomino;
    public int[,] Get(int index)
    {
        return polyominos[index];
    }
    public int Length()
    {
        return polyominos.Length;
    }
    private void Start()
    {
        polyomino = Get(0);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log(polyomino.GetLength(0));
        }    
            
        if (Input.GetKey(KeyCode.B))
        {
            Debug.Log(polyomino.GetLength(1));
        }   
    }
}
