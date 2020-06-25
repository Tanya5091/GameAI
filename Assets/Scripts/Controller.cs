using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public int playerNo; 
	public GameObject[] indicators;
	public Text text;
	public Sprite[] icons;
	public Button[] cells;
	public int[] grid;

	// Start is called before the first frame update
	void Start()
	{
		Ready();
	}

	public void Ready()
	{
		text.text="";
		playerNo = 0;
		indicators[0].GetComponent<Image>().color = Color.red;
		indicators[1].GetComponent<Image>().color = Color.black;
		for (int i = 0; i < cells.Length; i++)
		{
			cells[i].interactable = true;
			cells[i].GetComponent<Image>().sprite = null;
			grid[i] = -5;
		}
	}

	public void ButtonClicked(int no)
	{
		bool cont = MakeChoice(no);
		if (cont)
		{
			MakeChoice(ChooseMove());
		}
	}

	bool MakeChoice(int no)
	{
		grid[no] = playerNo + 1;
		cells[no].image.sprite = icons[playerNo];
		cells[no].interactable = false;
		int vic = Victory();
		indicators[playerNo].GetComponent<Image>().color = Color.black;
		playerNo = (playerNo + 1) % 2;
		indicators[playerNo].GetComponent<Image>().color = Color.red;
		if (vic >= 0)
		{
			setText(vic);
		}
		return vic < 0;
	}

	void setText(int winner)
	{
		if (winner == 2)
		{
			text.text = "It`s a tie";
		}
		else if (winner == 1)
		{
			text.text = "O won the game";
		}
		else
		{
			text.text = "X won the game";
		}

		for (int i = 0; i < cells.Length; i++)
		{
			cells[i].interactable = false;
		}
	}
	int Victory()
	{
		if (grid[0] > 0 && grid[0] == grid[1] && grid[0] == grid[2])
		{
			return grid[0] - 1;
		}
		if (grid[3] > 0 && grid[3] == grid[4] && grid[3] == grid[5])
		{
			return grid[3] - 1;
		}
		if (grid[6] > 0 && grid[6] == grid[7] && grid[6] == grid[8])
		{
			return grid[6] - 1;
		}
		if (grid[0] > 0 && grid[0] == grid[4] && grid[0] == grid[8])
		{
			return grid[0] - 1;
		}
		if (grid[6] > 0 && grid[6] == grid[4] && grid[6] == grid[2])
		{
			return grid[6] - 1;
		}
		if (grid[0] > 0 && grid[0] == grid[3] && grid[0] == grid[6])
		{
			return grid[0] - 1;
		}
		if (grid[1] > 0 && grid[1] == grid[4] && grid[1] == grid[7])
		{
			return grid[1] - 1;
		}
		if (grid[2] > 0 && grid[2] == grid[5] && grid[2] == grid[8])
		{
			return grid[2] - 1;
		}

		for (int i = 0; i < 9; i++)
		{
			if (grid[i] < 0)
			{
				return -1;
			}
		}

		return 2;
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene("Start");
	}


	int ChooseMove()
	{
		int best = -10000;
		int cell = -1;
		for (int i = 0; i < 9; i++)
		{
			if (grid[i] < 0)
			{
				grid[i] = 2;
				int current = Minimax(grid, false);
				grid[i] = -100;
				if (current > best)
				{
					best = current;
					cell = i;
				}
			}
		}
		return cell;
	}


	int Minimax(int[] grid, bool computer)
	{
		int res = Victory();
		if (res == 0)
		{
			return -1;
		}

		if (res == 1)
		{
			return 2;
		}

		if (res == 2)
		{
			return 1;
		}
		int best;
		if (computer)
		{
			best = -10000;
			for (int i = 0; i < 9; i++)
			{
				if (grid[i] < 0)
				{
					grid[i] = 2;
					int current = Minimax(grid, false);
					grid[i] = -100;
					best = Math.Max(current, best);
				}
				
			}
		}
		else
		{
			best = 10000;
			for (int i = 0; i < 9; i++)
			{
				if (grid[i ] < 0)
				{
					grid[i ] = 1;
					int current = Minimax(grid, true);
					grid[i] = -100;
					best = Math.Min(current, best);
				}
			}
		}
		return best;
	}

}
