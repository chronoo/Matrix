using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixGenerator;

namespace Generator.Model
{
	public abstract class Node
	{
		public OperationNode parent { get; set; }
	}
	public class Number : Node
	{
		public double numb { get; set; }
	}
	public abstract class OperationNode : BasicNode
	{
		public bool isBracket { get; set; }		
		public OperationEnum operation { get; set; }
		public void brackCheck()
		{
			this.isBracket = false;
			if (this is BynaryOperation)
			{
				if (this.parent is BynaryOperation)
					if (this.operation == OperationEnum.sum && parent.operation == OperationEnum.mult)
						this.isBracket = true;
				if (this.parent is UnaryOperation)
					this.isBracket = true;
			}
		}

		public void Calculate()
		{
			throw new System.NotImplementedException();
		}
	}
	public abstract class BynaryOperation : OperationNode
	{
		public Node firstChild { get; set; }

		public Node secondChild { get; set; }
		public BynaryOperation()
		{
			GenericMatrix firstChild = new GenericMatrix();
			firstChild.parent = this;
			GenericMatrix secondChild = new GenericMatrix();
			secondChild.parent = this;
			this.firstChild = firstChild;
			this.secondChild = secondChild;
		}
	}
	public class MultOperation : BynaryOperation
	{
		public MultOperation() : base() {
			operation = OperationEnum.mult;
		}	
	}
	public class SumOperation : BynaryOperation {
		public SumOperation() : base() {
			operation = OperationEnum.sum;
		}
	}
	public abstract class UnaryOperation: OperationNode
	{
		public Node child { get; set; }
		public UnaryOperation()
		{
			GenericMatrix child = new GenericMatrix();			
			child.parent = this;
			this.child = child;
		}
	}
	public class TranseOperation : UnaryOperation
	{
		public TranseOperation() : base(){
			this.operation = OperationEnum.transe;
		}
	}
	public class InvertOperation : UnaryOperation {
		public InvertOperation() : base() {
			operation = OperationEnum.inverse;
		}
	}
	public abstract class BasicNode : Node
	{
		public char label;
		public int nSize { get; set; }
		public int mSize { get; set; }
		public Matrix matrix { get; set; }

		public bool isQuad()
		{
			if (nSize == mSize)
				return true;
			else
				return false;
		}
	}
	public static class OperationCreator
	{
		public static OperationNode Create(BasicNode node, OperationEnum op = OperationEnum.any)
		{
			OperationNode replacement = null;
			if (node.parent == null)
			{
				if (op == OperationEnum.any)
					replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 4, 4, 2, 0));
			}
			else
			{				
				switch ((node.parent as OperationNode).operation)
				{
					case OperationEnum.mult:
						replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 0, 5, 1, 0));
						break;
					case OperationEnum.sum:
						replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 5, 0, 1, 0));
						break;
					case OperationEnum.transe:
						replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 6, 4, 0, 0));
						break;
					case OperationEnum.inverse:
						replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 6, 4, 2, 0));
						break;
				}
				if (node.parent is UnaryOperation)
				{
					UnaryOperation parent = (node.parent as UnaryOperation);
					parent.child = replacement;
				}
				else if (node.parent is BynaryOperation)
				{
					BynaryOperation parent = (node.parent as BynaryOperation);
					if (node == parent.firstChild)
						parent.firstChild = replacement;
					else
						parent.secondChild = replacement;
				}
				replacement.parent = node.parent;
			}
			replacement.brackCheck();
			return replacement;
		}
		public static BynaryOperation CreateBinary(BasicNode node, bool onlyMult)
		{
			OperationNode replacement = null;
			if (node.parent == null)
			{
				if(onlyMult)
					replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 1, 0, 0, 0));
				else
					replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 4, 4, 0, 0));
			}
			else
			{
				if (onlyMult)
					replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 1, 0, 0, 0));
				else
				{
					switch ((node.parent as OperationNode).operation)
					{
						case OperationEnum.mult:
							replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 0, 5, 0, 0));
							break;
						case OperationEnum.sum:
							replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 5, 0, 0, 0));
							break;
						case OperationEnum.transe:
							replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 6, 4, 0, 0));
							break;
						case OperationEnum.inverse:
							replacement = CreateOperation(RandDecide.Decide(node.isQuad(), 6, 4, 0, 0));
							break;
					}
				}
				if (node.parent is UnaryOperation)
				{
					UnaryOperation parent = (node.parent as UnaryOperation);
					parent.child = replacement;
				}
				else if (node.parent is BynaryOperation)
				{
					BynaryOperation parent = (node.parent as BynaryOperation);
					if (node == parent.firstChild)
						parent.firstChild = replacement;
					else
						parent.secondChild = replacement;
				}
				replacement.parent = node.parent;
			}
			replacement.brackCheck();
			return replacement as BynaryOperation;
		}
		public static OperationNode CreateOperation(OperationEnum operation)
		{
			OperationNode node = null;
			switch (operation)
			{
				case OperationEnum.mult:
					node = new MultOperation();
					break;
				case OperationEnum.sum:
					node = new SumOperation();
					break;
				case OperationEnum.transe:
					node = new TranseOperation();
					break;
				case OperationEnum.inverse:
					node = new InvertOperation();
					break;
				case OperationEnum.error:
					break;
			}
			return node;
		}
	} 
	public abstract class BasicMatrixNode : BasicNode
	{
		public bool isClosed { get; set; }
	}
	public class QuadMatrix : BasicMatrixNode { }
	public class GenericMatrix : BasicMatrixNode { }
	public abstract class Vector : BasicMatrixNode { }
	public class Row : Vector { }
	public class Column : Vector { }
}