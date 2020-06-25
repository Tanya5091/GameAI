using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


namespace Assets.Scripts
{

	public class PathFinder 
	{
		int Heuristic(Node current, Node target)
		{
			return Mathf.Abs(current.xpos - target.xpos) + Mathf.Abs(current.ypos - target.ypos);
		}


		public List<Node> Find(int sx, int sy, int tx, int ty, Node[,] grid)
		{
			Node start = grid[sx, sy];
			Node target = grid[tx, ty];

			MinHeap open = new MinHeap(100);
			open.Insert(start);
			HashSet<Node> closed = new HashSet<Node>();

			while (!open.isEmpty)
			{
				Node current = open.Pop();
				closed.Add(current);
				if (current == target)
				{
					List<Node> res = new List<Node>();
					Node curr = current;
					while (curr != start)
					{
						res.Add(curr);
						curr = curr.PrevNode;
					}
					res.Add(curr);
					res.Reverse();
					return res;
				}
				else
				{
					foreach (Node n in Neighbors(grid,current))
					{
						if (n.isPath && !closed.Contains(n))
						{
						
							int newg = current.gVal + 1;
							if (!open.Contains(n))
							{
								n.gVal = newg;
								n.hVal = Heuristic(n, target);
								n.PrevNode = current;
								open.Insert(n);
							}
							else if (newg < n.gVal)
							{
								n.gVal = newg;
								n.PrevNode = current;
								open.UpdateNode(n);
							}
						}
					}
				}
			}
			return new List<Node>();
		}

		public List<Node> Neighbors(Node[,] grid_, Node current)
		{
			List<Node> res = new List<Node>();
			int x = current.xpos;
			int y = current.ypos;
			if (x - 1 >= 0)
			{
				res.Add(grid_[x-1,y]);
			}
			if (x + 1 < 10)
			{
				res.Add(grid_[x + 1, y]);
			}
			if (y - 1 >= 0)
			{
				res.Add(grid_[x, y-1]);
			}
			if (y + 1 <10)
			{
				res.Add(grid_[x , y+1]);
			}
			return res;
		}

	}
}