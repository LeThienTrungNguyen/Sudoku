using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject board;
    public GameObject pause;
    public GameObject lose;
    public GameObject victory;
    public GameObject chooseLevel;
    public GameObject chooseLevelStart;
    public void Pause()
    {
        print("Pause");
        Time.timeScale = 0;
        HideAllMenu();
        pause.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        HideAllMenu();
        pause.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        GetComponent<Grid>().ResetBoard();
        HideAllMenu();
    }
    Level level;
    public void NewGame()
    {
        HideAllMenu();
        chooseLevel.SetActive(true);
    }
    public void LevelChoose(string levelString)
    {
        switch (levelString)
        {
            case "Easy": this.level = Level.Easy; break;
            case "Medium": this.level = Level.Medium; break;
            case "Hard": this.level = Level.Hard; break;
            case "Expert": this.level = Level.Expert; break;
            case "Evil": this.level = Level.Evil; break;
        }

        HideAllMenu();

        GetComponent<Grid>().GenerationSolvedBoard();
        GetComponent<Grid>().level = this.level;
        GetComponent<Grid>().GenerationBoard(this.level);
        GetComponent<Grid>().CopyBoard(Grid.sudokuBoard,Grid.backupBoard);
        GetComponent<Grid>().ShowBoard();
        Time.timeScale = 1;



    }

    public void LevelChooseStart(string levelString)
    {
        switch (levelString)
        {
            case "Easy": this.level = Level.Easy; break;
            case "Medium": this.level = Level.Medium; break;
            case "Hard": this.level = Level.Hard; break;
            case "Expert": this.level = Level.Expert; break;
            case "Evil": this.level = Level.Evil; break;
        }

        Time.timeScale = 1;

        GetComponent<Grid>().level = this.level;
        HideAllMenu();
        board.SetActive(true);
        GetComponent<Grid>().GenerationBoard(this.level);
        GetComponent<Grid>().CopyBoard(Grid.sudokuBoard,Grid.backupBoard);
        GetComponent<Grid>().ShowBoard();
        //GetComponent<Grid>().level = this.level;
    }

    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Thoát editor
#else
                        Application.Quit(); // Thoát game
#endif
    }

    public void HideAllMenu()
    {
        pause.SetActive(false);
        lose.SetActive(false);
        victory.SetActive(false);
        chooseLevel.SetActive(false); chooseLevelStart.SetActive(false);
    }

    public void VictoryMenu()
    {
        HideAllMenu(); victory.SetActive(true);Time.timeScale = 0;
    }

    public void LoseMenu()
    {
        HideAllMenu(); lose.SetActive(true); Time.timeScale = 0;
    }
}
