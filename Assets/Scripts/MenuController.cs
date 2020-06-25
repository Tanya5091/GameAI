using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartTTT()
    {
	    SceneManager.LoadScene("TicTacToe");
    }
    public void StartMaze()
    {
	    SceneManager.LoadScene("MazeScene");
    }
}
