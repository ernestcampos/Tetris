using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;

    // Use this for initialization
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Modify position
            transform.position += new Vector3(-1, 0, 0);

            //See if valid.
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. Revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Modify Position
            transform.position += new Vector3(1, 0, 0);

            //See if valid.
            if (isValidGridPos())
                updateGrid();
            else
            transform.position += new Vector3(-1, 0, 0);
        }

        //rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            //See if valid.
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, 0, 90);
        }

        // Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }


    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 vector = Grid.roundVec2(child.position);

            // Not insideBorder?
            if (!Grid.insideBorder(vector))
                return false;

            // Block in grid cell (and not part of same grou)?
            if (Grid.grid[(int)vector.x, (int)vector.y] != null &&
                Grid.grid[(int)vector.x, (int)vector.y].parent != transform)
                return false;
        }
        return true;

    }

    void updateGrid()
    {
        for (int column = 0; column < Grid.height; ++column)
        {
            for (int row = 0; row < Grid.width; ++row)
            {
                if (Grid.grid[row, column] != null)
                {
                    if (Grid.grid[row, column].parent == transform)
                    {
                        Grid.grid[row, column] = null;
                    }
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 vector = Grid.roundVec2(child.position);
            Grid.grid[(int)vector.x, (int)vector.y] = child;
        }
    }

}