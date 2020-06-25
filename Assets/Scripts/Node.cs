using System;
namespace Assets.Scripts
{
	public class Node : IComparable<Node>
	{
		public int hVal;
		public int gVal;
		public bool isPath;
		public int xpos;
		public int ypos;
		public Node PrevNode;
		int index;
		public int fVal
		{
			get
			{
				return hVal + gVal;
			}
		}

		public int Index
		{
			get;
			set;
		}

		public Node(int x, int y, bool passable)
		{
			isPath = passable;
			xpos = x;
			ypos = y;
			}

		public int CompareTo(Node other)
		{
			if (this.fVal == other.fVal)
			{
				return other.hVal.CompareTo(this.hVal);
			}
			else
				return other.fVal.CompareTo(this.fVal);
		}
	}
}
