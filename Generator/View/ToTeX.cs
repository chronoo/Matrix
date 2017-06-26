using Generator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.View
{
	public class ToTeX
	{
		public static string PrintTree(Tree _tree)
		{
			string rez = string.Format(@"${0}=", _tree.root.label);
			Print(_tree.root as BasicNode, ref rez);
			return rez + "$";
		}
		public static void Print(Node Na, ref string rez)
		{
			if (Na is OperationNode)
			{
				OperationNode N = Na as OperationNode;
				if (N.isBracket)
					rez += "(";
				if (N is UnaryOperation)
					rez += "{";
				if (N is BynaryOperation)
				{
					BynaryOperation op = N as BynaryOperation;
					if (op.firstChild is OperationNode)
						Print(op.firstChild as OperationNode, ref rez);
					else if (op.firstChild is BasicNode)
						rez += (op.firstChild as BasicNode).label;
					else if (op.firstChild is Number)
						rez += (op.firstChild as Number).numb;
				}
				if (N is UnaryOperation)
				{
					UnaryOperation op = N as UnaryOperation;
					if (op.child is OperationNode)
						Print(op.child as OperationNode, ref rez);
					else if(op.child is BasicNode)
						rez += (op.child as BasicNode).label;
					else if (op.child is Number)
						rez += (op.child as Number).numb;
				}
				switch (N.operation)
				{
					case OperationEnum.mult:
						rez += "*";
						break;
					case OperationEnum.sum:
						rez += "+";
						break;
					case OperationEnum.transe:
						rez += "^{T}";
						break;
					case OperationEnum.inverse:
						rez += "^{-1}";
						break;
					case OperationEnum.error:
						break;
				}
				if (N is BynaryOperation)
				{
					BynaryOperation op = N as BynaryOperation;
					if (op.secondChild is OperationNode)
						Print(op.secondChild as OperationNode, ref rez);
					else if (op.secondChild is BasicNode)
						rez += (op.secondChild as BasicNode).label;
					else if (op.secondChild is Number){
						if((op.secondChild as Number).numb<0 && op.operation== OperationEnum.mult)
						{
							rez += string.Format("({0})",(op.secondChild as Number).numb);
						}else
						{
							rez += (op.secondChild as Number).numb;
						}
					}
				}
				if (N is UnaryOperation)
					rez += "}";
				if (N.isBracket)
					rez += ")";
			}else
			{
				if (Na is BasicNode)
					rez += (Na as BasicNode).label;
				else if (Na is Number)
					rez += (Na as Number).numb;
			}
		}
	}
}
