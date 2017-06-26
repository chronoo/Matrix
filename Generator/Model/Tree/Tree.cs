using Generator.View;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Model
{
	public class Tree
	{
		public BasicNode root { get; set; }
		private bool isQuad { get; set; }
		public List<Node> nodes { get; set; }
		public MersenneTwister random { get; set; }
		public bool numberExist { get; set; }
		public bool quadExist { get; set; }
		public bool vectorExist { get; set; }
		public bool zeroDet { get; set; }
		public QuadMatrix qMax { get; set; }
		public Number numb { get; set; }
		private int _mSize;
		private int _nSize;
		public Tree()
		{
			random = new MersenneTwister();
			root = new GenericMatrix();
			nodes = new List<Node>();
			nodes.Add(root);
		}
		public string GetFormulaTeX()
		{
			return ToTeX.PrintTree(this).Replace("+-", "-");
		}
		public string GetOutNatrix()
		{
			return string.Format(@"${0}={1}$", root.label, root.matrix.ToTEX("N3"));
		}
		public bool Build(int matrixCount, ElementType elementType)
		{
			int vectorCoef = 0;
			int quadCoef = 0;
			if (vectorExist)
			{
				vectorCoef = 1;
			}
			if (quadExist)
			{
				quadCoef = 1;
			}
			int tempMatrixCount = matrixCount - vectorCoef - quadCoef;
			while (nodes.Count < tempMatrixCount)
			{
				NodeOpen(nodes[random.Next(0, nodes.Count)] as BasicNode);
			};
			AddToRoot();
			if (random.NextDouble() > 0.6)
			{
				if (numberExist)
				{
					AddNumber();
				}
				if (quadExist)
				{
					AddQuad();
				}
				if (vectorExist)
				{
					AddVectors();
				}
				if (quadExist)
				{
					nodes.Add(qMax);
				}
			}
			else
			{
				if (quadExist)
				{
					AddQuad();
				}
				if (vectorExist)
				{
					AddVectors();
				}
				if (quadExist)
				{
					nodes.Add(qMax);
				}
				if (numberExist)
				{
					AddNumber();
				}
			}
			SetName();
			SetSize(root, _mSize, _nSize);
			FillMatrix(elementType);
			if (root is OperationNode)
			{
				Calc(root as OperationNode);
			}
			if (root.matrix.Field.Exists(ValueConstraint, Zeros.AllowSkip))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void SetSize(Node nod, int m, int n)
		{
			if (nod is OperationNode)
			{
				OperationNode node = nod as OperationNode;
				node.mSize = m;
				node.nSize = n;
				switch (node.operation)
				{
					case OperationEnum.mult:
						{
							BynaryOperation op = node as BynaryOperation;
							if (op.firstChild is BasicNode && op.secondChild is BasicNode)
							{
								BasicNode child1 = op.firstChild as BasicNode;
								BasicNode child2 = op.secondChild as BasicNode;

								int x = 1;

								if (child1 is Row || child2 is Column || child2 is Row || child1 is Column)
								{
									x = 1;
								}
								else
								{
									if (child1 is QuadMatrix || child2 is QuadMatrix)
									{
										if (child1 is QuadMatrix)
											x = n;
										else
											x = m;
									}else
										x = GetMaxSize(node);
								}
								

								child1.mSize = m;
								child1.nSize = x;

								child2.mSize = x;
								child2.nSize = n;

								SetSize(child1, child1.mSize, child1.nSize);
								SetSize(child2, child2.mSize, child2.nSize);
							}
							else
							{
								if (op.firstChild is BasicNode)
								{
									BasicNode child = op.firstChild as BasicNode;
									child.mSize = m;
									child.nSize = n;
									SetSize(child, child.mSize, child.nSize);									
								}
								else
								{
									BasicNode child = op.secondChild as BasicNode;
									child.mSize = m;
									child.nSize = n;
									SetSize(child, child.mSize, child.nSize);
								}
							}
							break;
						}
					case OperationEnum.sum:
						{
							BynaryOperation op = node as BynaryOperation;
							if (op.firstChild is BasicNode)
							{
								BasicNode child = op.firstChild as BasicNode;
								child.mSize = m;
								child.nSize = n;
								SetSize(child, child.mSize, child.nSize);
							}
							if (op.secondChild is BasicNode)
							{
								BasicNode child = op.secondChild as BasicNode;
								child.mSize = m;
								child.nSize = n;
								SetSize(child, child.mSize, child.nSize);
							}
							break;
						}
					case OperationEnum.transe:
						{
							UnaryOperation op = node as UnaryOperation;
							if (op.child is BasicNode)
							{
								BasicNode child = op.child as BasicNode;
								child.mSize = n;
								child.nSize = m;
								SetSize(child, child.mSize, child.nSize);
							}
							break;
						}
					case OperationEnum.inverse:
						{
							UnaryOperation op = node as UnaryOperation;
							if (op.child is BasicNode)
							{
								BasicNode child = op.child as BasicNode;
								child.mSize = m;
								child.nSize = n;
								SetSize(child, child.mSize, child.nSize);
							}
							break;
						}
				}
			}			
		}
		public Tree (GenericMatrix matrix)
		{
			random = new MersenneTwister();
			nodes = new List<Node>();
			root = matrix;
			isQuad = matrix.isQuad();
			_mSize = matrix.mSize;
			_nSize = matrix.nSize;
			nodes.Add(root);
		}
		public void AddNumber()
		{
			BasicNode node = nodes[random.Next(0, nodes.Count)] as BasicNode;
			bool isRoot = false;
			if (node == root)
				isRoot = true;
			nodes.Remove(node);			
			BynaryOperation op = OperationCreator.CreateBinary(node,false);
			node.parent = op;
			nodes.Add(node as BasicNode);
			if (isRoot)
				root = op;
			if (random.NextBoolean())
			{
				op.secondChild = node;
				op.firstChild = new Number();
				numb = op.firstChild as Number;
			}
			else
			{
				op.firstChild = node;
				op.secondChild = new Number();
				numb = op.secondChild as Number;
			}
			op.firstChild.parent = op;
			op.secondChild.parent = op;
		}
		public void AddVectors()
		{
			BasicNode node = nodes[random.Next(0, nodes.Count)] as BasicNode;
			bool isRoot = false;
			if (node == root)
			{
				isRoot = true;
			}
			nodes.Remove(node);
			BynaryOperation op = OperationCreator.CreateBinary(node, true);
			if (isRoot)
			{
				root = op;
			}
			op.firstChild = new Column();
			op.secondChild = new Row();

			op.firstChild.parent = op;
			op.secondChild.parent = op;
			
			nodes.Add(op.firstChild);
			nodes.Add(op.secondChild);
			op.brackCheck();
		}
		public void AddQuad()
		{
			BasicNode node = nodes[random.Next(0, nodes.Count)] as BasicNode;
			bool isRoot = false;
			if (node == root)
				isRoot = true;
			nodes.Remove(node);
			BynaryOperation op = OperationCreator.CreateBinary(node, true);
			//nodes.AddRange(op.children);
			if (isRoot)
				root = op;
			if (random.NextBoolean())
			{
				op.firstChild = new QuadMatrix();
				qMax = op.firstChild as QuadMatrix;
				op.secondChild = new GenericMatrix();
				nodes.Add(op.secondChild as BasicNode);
			}else
			{
				op.firstChild = new GenericMatrix();
				op.secondChild = new QuadMatrix();
				qMax = op.secondChild as QuadMatrix;
				nodes.Add(op.firstChild as BasicNode);
			}
			op.firstChild.parent = op;
			op.secondChild.parent = op;
			op.brackCheck();
		}
		public void AddToRoot()
		{
            UnaryOperation replacement = OperationCreator.CreateOperation(RandDecide.Decide(isQuad, 0, 0, 1, 4)) as UnaryOperation;
            try
            {
                if (random.NextDouble() > 0.6)
                {
                    if (root is BynaryOperation)
                    {
                        replacement = OperationCreator.CreateOperation(RandDecide.Decide(isQuad, 0, 0, 1, 4)) as UnaryOperation;
                    }
                    else if (root is UnaryOperation)
                    {
                        if ((root as UnaryOperation).operation == OperationEnum.transe)
                        {
                            replacement = OperationCreator.CreateOperation(RandDecide.Decide(isQuad, 0, 0, 0, 1)) as UnaryOperation;
                        }
                        else
                        {
                            replacement = OperationCreator.CreateOperation(RandDecide.Decide(isQuad, 0, 0, 1, 0)) as UnaryOperation;
                        }
                    }
                    replacement.child = root;
                    if (root is OperationNode)
                        (root as OperationNode).isBracket = true;
                    root.parent = replacement;
                    root = replacement;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
		public void NodeOpen(BasicNode node)
		{
			bool isRoot = false;
			if (node == root)
				isRoot = true;
			nodes.Remove(node);
			node = OperationCreator.Create(node, OperationEnum.any);
			if(node is BynaryOperation)
			{
				nodes.Add((node as BynaryOperation).firstChild);
				nodes.Add((node as BynaryOperation).secondChild);
			}else
			{
				nodes.Add((node as UnaryOperation).child);
			}
			if (isRoot)
				root = node;
		}
		public List<Node> AllType<T>()
		{
			List<Node> N = new List<Node>();
			foreach (Node nod in nodes)
			{
				if (nod is T)
					N.Add(nod);
			}
			return N;
		}
		public void SetName()
		{
			string Labels = "ABCDEFGHIGKLMNOPQRSTUXYWZ";			
			foreach (BasicNode N in nodes)
			{
				N.label = Labels[nodes.IndexOf(N)%Labels.Count()];
			}
			root.label = Labels[nodes.Count % Labels.Count()];

			if (numberExist)
			{
				numb.numb = random.Next(1, 10);
				if (random.NextBoolean())
					numb.numb *= -1;
			}

		}
		public void FillMatrix(ElementType elementType)
		{
            int minValue = (int)MatrixElenemtRange.min;
            int maxValue = (int)MatrixElenemtRange.max;
            if (elementType == ElementType.any)
                minValue = -maxValue;
            foreach (BasicNode N in nodes)
			{
                N.matrix=new MatrixGenerator.Matrix(N.mSize,N.nSize, minValue, maxValue, false,false,N.label.ToString());
				for (int i = 0; i < N.mSize; i++)
					for (int j = 0; j < N.nSize; j++)
						if (N.matrix.Field[i, j] == 0)
						{
							N.matrix.Field[i, j] = random.Next(1, 10);
							if (random.NextBoolean())
								N.matrix.Field[i, j] *= -1;
						}				
			}
			if (zeroDet)
            {
                qMax.matrix.MakeZeroDeterminant();//дописать
            }
				
		}
		public void Calc(OperationNode node)
		{
			if(node is BynaryOperation)
			{
				BynaryOperation bin = node as BynaryOperation;
				bin.matrix = new MatrixGenerator.Matrix();
				if (bin.firstChild is OperationNode)
				{
					Calc(bin.firstChild as OperationNode);
				}
				if (bin.secondChild is OperationNode)
				{
					Calc(bin.secondChild as OperationNode);
				}
				if(bin.firstChild is Number || bin.secondChild is Number)
				{
					if (bin.firstChild is Number)
					{
						if (bin is MultOperation)
							bin.matrix.Field = (bin.secondChild as BasicNode).matrix.Field.Multiply((bin.firstChild as Number).numb);
						else
							bin.matrix.Field = (bin.secondChild as BasicNode).matrix.Field.Add((bin.firstChild as Number).numb);
					}
					else
					{
						if (bin is MultOperation)
							bin.matrix.Field = (bin.firstChild as BasicNode).matrix.Field.Multiply((bin.secondChild as Number).numb);
						else
							bin.matrix.Field = (bin.firstChild as BasicNode).matrix.Field.Add((bin.secondChild as Number).numb);
					}
				}else
				{
					if (bin is MultOperation)
						bin.matrix.Field = (bin.firstChild as BasicNode).matrix.Field.Multiply((bin.secondChild as BasicNode).matrix.Field);
					else
						bin.matrix.Field = (bin.firstChild as BasicNode).matrix.Field.Add((bin.secondChild as BasicNode).matrix.Field);
				}
			}else
			{
				UnaryOperation un = node as UnaryOperation;
				un.matrix = new MatrixGenerator.Matrix();
				if (un.child is OperationNode)
				{
					Calc(un.child as OperationNode);
				}
				if(un is TranseOperation)
					un.matrix.Field = (un.child as BasicNode).matrix.Field.Transpose();
				else
					un.matrix.Field = (un.child as BasicNode).matrix.Field.Inverse();
			}
		}
		public int GetMaxSize(BasicNode N)
		{
			int c = 0;
			if(random.NextBoolean())
			{
				c = random.Next(Math.Min(N.nSize, N.mSize) - 1, Math.Max(N.nSize, N.mSize) + 2);
			}
			else
			{
				c = random.Next(Math.Min(N.nSize, N.mSize)-1, Math.Max(N.nSize, N.mSize)+1);
			}
			if (c == 0)
			{
				if (random.NextBoolean())
					c = N.nSize;
				else
					c = N.mSize;
			}
			return c;
		}
		public static bool ValueConstraint(double value)
		{
			if (Math.Abs(value) > 1000 || Math.Abs(value) < 0.001)
				return true;
			else return
					false;
		}
	}
}