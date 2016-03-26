using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Vector2 roundVec2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x),
            Mathf.Round(vector.y));
    }

    public static bool insideBorder(Vector2 position)
    {
        return ((int)position.x >= 0 &&
            (int)position.x < width &&
            (int)position.y >= 0);
    }

    public static void deleteRow(int row)
    {
        for (int column = 0; column < width; ++column)
        {
            Destroy(grid[column, row].gameObject);
            grid[column, row] = null;
        }
    }

    public static void decreaseRow(int row)
    {
        for (int column = 0; column < width; ++column)
        {
            if (grid[column, row] != null)
            {
                // Move one towards bottom
                grid[column, row - 1] = grid[column, row];
                grid[column, row] = null;

                // Update Block position
                grid[column, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowsAbove(int startRow)
    {
        for (int row = startRow; row < height; ++row)
            decreaseRow(row);
    }

    public static bool isRowFull(int row)
    {
        for (int column = 0; column < width; ++column)
        {
            if (grid[column, row] == null)
            {
                return false;
            }
        }
            return true;
    }

    public static void deleteFullRows()
    {
        for (int row = 0; row < height; ++row)
        {
            if (isRowFull(row))
            {
                deleteRow(row);
                decreaseRowsAbove(row + 1);
                --row;
            }
        }

    }
}
