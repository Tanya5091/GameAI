using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MazeController : MonoBehaviour
{
	private int determined;
	public GameObject button;
	public Button finder;
	public Text text;
	public Image GridLayoutGroup;
	private int startx;
	private int starty;
	private int targetx;
	private int targety;
	public Button[,] cells;

    // Start is called before the first frame update
    void Start()
    {
	   ButtonSetup();
    }

    void ButtonSetup()
    {
	    finder.interactable = false;
	    int determined = 0;
		cells= new Button[10,10];
	    for (int i = 0; i < 10; i++)
	    {
		    for (int j = 0; j < 10; j++)
		    {
			    int i1 = i;
			    int j1 = j;
				cells[i, j] = Instantiate(button).GetComponent<Button>();
			    cells[i,j].onClick.AddListener(delegate { ButtonClickHandler(i1, j1); });
			    cells[i,j].interactable = true;
			    cells[i,j].transform.SetParent(GridLayoutGroup.transform);

		    }
	    }
	}

    public bool isPath(Button b)
    {
	    return b.GetComponent<Image>().color != Color.black;
    }

    public void FindPath()
    {
		PathFinder pf = new PathFinder();
		Node[,] grid = new Node[10,10];
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				grid[i, j] = new Node(i, j, isPath(cells[i, j]));
			}
		}

		List<Node> path = pf.Find(startx, starty, targetx, targety, grid);

		if (path.Count == 0)
		{
			text.text = "No Path";
		}
		else
		{
			path.RemoveAt(0);
			path.RemoveAt(path.Count-1);
			foreach (Node n in path)
			{
				cells[n.xpos, n.ypos].image.color = Color.green;
			}
		}
    }
    public void Restart()
    {
	    SceneManager.LoadScene("MazeScene");
    }
	public void GoToMenu()
	{
		SceneManager.LoadScene("Start");
	}

	private void ButtonClickHandler(int i, int j)
    {
	    if (determined == 0)
	    {
		    startx = i;
		    starty = j;
		    cells[i, j].GetComponent<Image>().color = Color.cyan;
		    cells[i, j].interactable = false;
			cells[i, j].GetComponentInChildren<Text>().text = "Start";
		    determined++;
	    }
		else if (determined == 1)
	    {
			targetx = i;
		    targety = j;

		    cells[i, j].GetComponent<Image>().color = Color.yellow;
			cells[i, j].interactable = false;
			cells[i, j].GetComponentInChildren<Text>().text = "Target";
		    determined++;
		    finder.interactable = true;
	    }
	    else
	    {
			cells[i, j].GetComponent<Image>().color = Color.black;
			cells[i, j].interactable = false;
		}
	}

  
}
