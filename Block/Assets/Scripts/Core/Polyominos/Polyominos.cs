public static class Polyominos
{
    private static int[][,] polyominos = new int[][,]
    {
        
        new int[,]
        {
            {0,0,0,0},
            {1,1,1,1}
        },
        new int[,]
        {
            {1,0},
            {1,0},
            {1,0},
            {1,0}
        },
        new int[,]
        {
            {1,1},
            {1,1}
        },
        new int[,]
        {
            {0,1},
            {1,1},
            {0,1}
        },
         new int[,]
        {
            {1,0},
            {1,1},
            {1,0}
        },
        new int[,]
        {
            {1,1,1},
            {0,1,0}
        },
        new int[,]
        {
            {0,1,0},
            {1,1,1}
        },
        new int[,]
        {
            {1,1,1},
            {1,0,0}
        },
        new int[,]
        {
            {0,0,1},
            {1,1,1}
        },
        new int[,]
        {
            {1,0,0},
            {1,1,1}
        },
        new int[,]
        {
            {1,1,1},
            {0,0,1}
        },
        new int[,]
        {
            {0,1,1},
            {1,1,0}
        },
        new int[,]
        {
            {1,1,0},
            {0,1,1}
        },
        new int[,]
        {
            {0,0},
            {0,1}
        }
    };
    public static int[,] Get(int index)
    {
        return polyominos[index];
    }
    public static int Length()
    {
        return polyominos.Length;
    }
}
