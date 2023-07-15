using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public Transform board;
    public Transform sudokuIndex;

    bool[] CantOverwriteIndexes = new bool[81];
    public static int[] sudokuBoard = new int[81];
    public static int[] solvedBoard;
    public int maxErrorCount = 3;
    public int currentErrorCount = 0;
    public static Index chosenIndex;
    public static int chosenIndexValue;

    public Level level;

    [SerializeField]
    GameObject victoryBanner;

    private void Awake()
    {
        solvedBoard = new int[81];
        for (int i = 0; i < 81; i++)
        {
            solvedBoard[i] = sudokuBoard[i];
        }

        for (int i = 0; i < 81; i++)
        {
            GameObject Index = Instantiate(sudokuIndex.gameObject, board);
        }

        SetIndexAll();
        GenerationSudoku();



    }

    public void SolveSudoku()
    {
        // create new array to copy from puzzle to temp
        int[] tempSolveSudoku = new int[81];
        // solve sudoku
        // 1. copy puzzle -> temp
        for(int i = 0; i < 81; i++)
        {
            tempSolveSudoku[i] = sudokuBoard[i];
        }
        // 
        for(int i = 0; i < 81; i++)
        {
            // check empty block
            if (tempSolveSudoku[i] != 0) continue;
            // 
        }
    }

    public void GenerationSudoku()
    {
        GenerationSolvedBoard();
        GenerationBoard(level);
        ShowBoard();
        CopyBoard(sudokuBoard,backupBoard);
    }
    private void Update()
    {
        UpdateErrorScore();
    }


    public void SetChosenIndexValue(int value)
    {
        chosenIndexValue = value;
        if (!CantOverwriteIndexes[chosenIndex.rowIndex * 9 + chosenIndex.cellIndex])
        {
            sudokuBoard[chosenIndex.rowIndex * 9 + chosenIndex.cellIndex] = chosenIndexValue;
            board.GetChild(chosenIndex.rowIndex * 9 + chosenIndex.cellIndex).GetComponentInChildren<Text>().text = value + "";
            int sudokuValue = sudokuBoard[chosenIndex.rowIndex * 9 + chosenIndex.cellIndex];
            int solvedValue = solvedBoard[chosenIndex.rowIndex * 9 + chosenIndex.cellIndex];

            if (sudokuValue != solvedValue)
            {
                currentErrorCount++;
                UpdateErrorScore();
                if (currentErrorCount >= maxErrorCount)
                {
                    currentErrorCount = 0;
                    GameObject GameController = GameObject.Find("GameController");
                    GameController.GetComponent<MenuController>().LoseMenu();
                }
            }
            board.GetChild(chosenIndex.rowIndex * 9 + chosenIndex.cellIndex).GetComponentInChildren<Text>().color = (sudokuValue == solvedValue) ? Color.blue : Color.red;
        }

        if (CompleteLevel())
        {
            print("You Win");

            if (victoryBanner)
            {
                victoryBanner.SetActive(true);
            }
        };
    }

    bool CompleteLevel()
    {
        bool complete = true;
        for (int i = 0; i < 81; i++)
        {
            if (sudokuBoard[i] != solvedBoard[i])
            {
                complete = false;
            }
        }

        return complete;
    }

    void SetIndexAll()
    {
        for (int i = 0; i < 81; i++)
        {
            board.GetChild(i).GetComponent<Index>().SetIndex(i % 9, i / 9);
        }
    }

    public void GenerationBoard(Level level)
    {
        for (int i = 0; i < 81; i++)
        {
            sudokuBoard[i] = 0;
            CantOverwriteIndexes[i] = false;
        }

        int count = -1;
        switch (level)
        {
            case Level.Easy:
                count = Random.Range(40, 45);
                break;
            case Level.Medium:
                count = Random.Range(35, 40);
                break;
            case Level.Hard:
                count = Random.Range(30, 35);
                break;
            case Level.Expert:
                count = Random.Range(25, 30);
                break;
            case Level.Evil:
                count = Random.Range(22, 25);
                break;
        }
        if (count == -1) return;
        List<int> indexes = new List<int>();
        for (int i = 0; i < count; i++)
        {
        CheckNotContainOldIndex:
            {
                int randomIndex = Random.Range(0, 81);
                if (!indexes.Contains(randomIndex))
                {
                    indexes.Add(randomIndex);
                    continue;
                }
                goto CheckNotContainOldIndex;
            }
        }

        foreach (int index in indexes)
        {
            sudokuBoard[index] = solvedBoard[index];
            board.GetChild(index).GetComponentInChildren<Text>().color = Color.black;
            CantOverwriteIndexes[index] = true;
        }
    }

    public void GenerationSolvedBoard()
    {
        for (int i = 0; i < 81; i++)
        {
            solvedBoard[i] = 0;
        }

        for (int i = 0; i < 81; i++)
        {
            int rant = Random.Range(1, 10);
            int count = 100;
            while (true)
            {
                rant = Random.Range(1, 10);
                if (SafeIndex(i, rant))
                {
                    solvedBoard[i] = rant;
                    break;
                }
                count--;
                if (count < 0)
                {
                    break;
                }
            }

        }

        if (FindEmptyCell() != -1)
        {
            GenerationSolvedBoard();
        }
    }

    public int RandomNumber()
    {
        return Random.Range(0, 10);
    }

    public bool SafeIndex(int index, int number)
    {
        if (solvedBoard[index] != 0)
            return true;

        int cellIndex = index % 9;
        int rowIndex = index / 9;
        int boxRowIndex = rowIndex / 3;
        int boxCellIndex = cellIndex / 3;
        int boxIndex = boxRowIndex * 3 + boxCellIndex;

        for (int i = 0; i < 9; i++)
        {
            if (solvedBoard[rowIndex * 9 + i] == number ||
                solvedBoard[i * 9 + cellIndex] == number ||
                solvedBoard[(boxRowIndex * 3 + i / 3) * 9 + (boxCellIndex * 3 + i % 3)] == number)
            {
                return false;
            }
        }

        return true;
    }

    public void ShowBoard()
    {
        for (int i = 0; i < 81; i++)
        {
            if (sudokuBoard[i] == 0)
            {
                board.GetChild(i).GetComponentInChildren<Text>().text = "";
            }
            else
            {
                board.GetChild(i).GetComponentInChildren<Text>().text = sudokuBoard[i] + "";
            }
        }
    }

    public int FindEmptyCell()
    {
        for (int i = 0; i < 81; i++)
        {
            if (solvedBoard[i] == 0)
            {
                return i;
            }
        }

        return -1;
    }

    

    public static int[] backupBoard = new int[81];

    public void ResetBoard()
    {
        currentErrorCount = 0;
        for (int i = 0; i < 81; i++)
        {
            sudokuBoard[i] = 0;
        }
        for (int i = 0; i < 81; i++)
        {
            sudokuBoard[i] = backupBoard[i];
        }
        ShowBoard();
    }

    public void CopyBoard(int[]fromBoard, int[]toBoard)
    {
        for (int i = 0; i < 81; i++)
        {
            toBoard[i] = fromBoard[i];
        }
    }

    public void UpdateErrorScore()
    {
        GameObject errorScore = GameObject.Find("ErrorCount");
        if (errorScore)
        {
            Debug.Log("error score found");
            errorScore.GetComponent<Text>().text = currentErrorCount + "/3";
        }
    }

    public List<List<int>> validAnswers;
    private void Start()
    {
        TempSudokuBoard = new int[81];
        validAnswers = new List<List<int>>();
        Solve();
        CopyBoard(sudokuBoard, TempSudokuBoard);
        
    }
    int[] TempSudokuBoard;

    public void RecursionSolve(int cellIndex)
    {
        if (cellIndex == 81)
        {
            
        }
    }

    public void Solve()
    {
        RecursionSolve(0);
        Debug.Log("Total valid solutions: " + validAnswers.Count);
    }




}

public enum Level
{
    Easy,   // 50 - 60 %
    Medium, // 40 - 50 %
    Hard,   // 30 - 40 %
    Expert, // 20 - 30 %
    Evil    // 10 - 20 %
}
