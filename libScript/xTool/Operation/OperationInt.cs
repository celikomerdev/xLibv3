#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolOperation
{
	public class OperationInt : BaseInitM
	{
		protected override void OnInit(bool init)
		{
			if(!init) return;
			operationAction = CreateOperation();
		}
		
		#region Property
		[SerializeField]private int left = 0;
		public int Left
		{
			get
			{
				return left;
			}
			set
			{
				if(left == value) return;
				left = value;
				operationAction();
			}
		}
		
		[SerializeField]private int right = 0;
		public int Right
		{
			get
			{
				return right;
			}
			set
			{
				if(right == value) return;
				right = value;
				operationAction();
			}
		}
		
		[SerializeField]private string operation = "";
		public string Operation
		{
			get
			{
				return operation;
			}
			set
			{
				if(operation == value) return;
				operation = value;
				operationAction = CreateOperation();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventInt")]
		[SerializeField]private EventInt eventResult = new EventInt();
		private int result = 0;
		private int Result
		{
			set
			{
				if(result == value) return;
				result = value;
				eventResult.Invoke(result);
			}
		}
		
		public void Recall()
		{
			eventResult.Invoke(result);
		}
		#endregion
		
		
		
		#region Operation
		private Action operationAction = delegate(){};
		private Action CreateOperation()
		{
			switch(operation)
			{
				case "+":
				case "left+right":
					return Op_Add;
				case "-":
				case "left-right":
					return Op_Substract;
				case "*":
				case "left*right":
					return Op_Multiply;
				case "/":
				case "left/right":
					return Op_Divide;
				case "100-left/right*100":
					return Op_Discount;
				default:
					return Op_Null;
			}
		}
		
		private void Op_Add()
		{
			Result = left+right;
		}
		
		private void Op_Substract()
		{
			Result = left-right;
		}
		
		private void Op_Multiply()
		{
			Result = left*right;
		}
		
		private void Op_Divide()
		{
			Result = left/right;
		}
		
		private void Op_Discount()
		{
			Result = 100-left/right*100;
		}
		
		private void Op_Null()
		{
			xDebug.LogTempFormat(this,this.name+":Op_Null");
		}
		#endregion
		
		
		#region Convert
		public void LongLeft(long value)
		{
			Left = (int)value;
		}
		
		public void LongRight(long value)
		{
			Right = (int)value;
		}
		#endregion
	}
}
#endif

// #if xLibv2
// using UnityEngine;
// using xLib.ToolEventClass;

// namespace xLib.ToolOperation
// {
// 	public class OperationInt : BaseM
// 	{
// 		#region Comparison
// 		[SerializeField]private int left;
// 		[SerializeField]private int right;
// 		public EventInt eventInt;
// 		#endregion
		
		
// 		#region Property
// 		public int Left
// 		{
// 			get
// 			{
// 				return this.left;
// 			}
// 			set
// 			{
// 				this.left = value;
// 			}
// 		}
		
// 		public int Right
// 		{
// 			get
// 			{
// 				return this.right;
// 			}
// 			set
// 			{
// 				this.right = value;
// 			}
// 		}
		
// 		public int LeftAdd
// 		{
// 			set
// 			{
// 				Left += value;
// 			}
// 		}
		
// 		public int RightAdd
// 		{
// 			set
// 			{
// 				Right += value;
// 			}
// 		}
// 		#endregion
		
		
// 		#region Compare
// 		public void Op_Add()
// 		{
// 			int result = left+right;
// 			eventInt.Invoke(result);
// 		}
		
// 		public void Op_Substract()
// 		{
// 			int result = left-right;
// 			eventInt.Invoke(result);
// 		}
		
// 		public void Op_Multiply()
// 		{
// 			int result = left*right;
// 			eventInt.Invoke(result);
// 		}
		
// 		public void Op_Divide()
// 		{
// 			int result = Mathf.RoundToInt(left/right);
// 			eventInt.Invoke(result);
// 		}
		
// 		public void Op_Repeat()
// 		{
// 			int result = Mathx.MathInt.Repeat(left,right);
// 			eventInt.Invoke(result);
// 		}
		
// 		public void Op_Greater()
// 		{
// 			int greater = Left;
// 			if(Right>greater) greater = Right;
// 			eventInt.Invoke(greater);
// 		}
// 		#endregion
		
		
// 		#region Overload
// 		public void Op_Add(int value)
// 		{
// 			Right = value;
// 			Op_Add();
// 		}
		
// 		public void Op_Substract(int value)
// 		{
// 			Right = value;
// 			Op_Substract();
// 		}
		
// 		public void Op_Multiply(int value)
// 		{
// 			Right = value;
// 			Op_Multiply();
// 		}
		
// 		public void Op_Divide(int value)
// 		{
// 			Right = value;
// 			Op_Divide();
// 		}
		
// 		public void Op_Repeat(int value)
// 		{
// 			Left = value;
// 			Op_Repeat();
// 		}
		
// 		public void Op_Greater(int value)
// 		{
// 			Right = value;
// 			Op_Greater();
// 		}
// 		#endregion
// 	}
// }
// #endif