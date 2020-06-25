using System;

namespace Assets.Scripts
{
	

	public class MinHeap
	{
		Node [] cells;
		int count;

		public MinHeap(int size)
		{
			cells = new Node[size];
		}
		public bool isEmpty
		{
			get
			{
				return count == 0;
			}
		}

		public void Insert(Node node)
		{
			node.Index = count;
			cells[count++] = node;
			FloatNode(node);
			
		}
		public Node Pop()
		{
			Node head = cells[0];
			cells[0] = cells[--count];
			cells[0].Index = 0;
			SinkNode(cells[0]);
			return head;
		}

		public bool Contains(Node node)
		{
			return cells[node.Index]==node;
		}
		

		void SinkNode(Node node)
		{
			while (true)
			{
				int left = node.Index * 2 + 1;
				int right = node.Index * 2 + 2;
				int newInd;
				if (right < count)
				{
					 newInd = cells[left].CompareTo(cells[right]) > 0 ? left:right;
				}
				else if (left<count)
				{
					newInd = left;
				}
				else
				{
					return;
				}

				if (node.CompareTo(cells[newInd]) < 0)
				{
					Node temp = cells[node.Index];
					cells[node.Index] = cells[newInd];
					cells[node.Index].Index = node.Index;
					cells[newInd] = temp;
					cells[newInd].Index = newInd;
				}
				else
				{
					return;
				}
			}
		}

		
		void FloatNode(Node node)
		{
			int newInd = (node.Index - 1) / 2;
			while (node.Index>0)
			{
				Node parent = cells[newInd];
				if (parent.CompareTo(node) < 0)
				{
						Node temp = cells[node.Index];
						cells[node.Index] = cells[newInd];
						cells[node.Index].Index = node.Index;
						cells[newInd] = temp;
						cells[newInd].Index = newInd;
						newInd= (node.Index - 1) / 2;
				}
				else
				{
					{
						break;
					}
				}
			}
		}

		public void UpdateNode(Node item)
		{
			FloatNode(item);
			SinkNode(item);
		}
	}



}
