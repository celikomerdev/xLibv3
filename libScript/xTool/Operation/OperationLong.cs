#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolOperation
{
	public class OperationLong : BaseInitM
	{
		protected override void OnInit(bool init)
		{
			if(!init) return;
			operationAction = CreateOperation();
			operationAction();
		}
		
		#region Property
		[SerializeField]private long left;
		public long Left
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
		
		[SerializeField]private long right;
		public long Right
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
		
		[SerializeField]private string operation;
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
		
		[SerializeField]private EventLong eventResult = new EventLong();
		private long result = 0;
		private long Result
		{
			set
			{
				if(result == value) return;
				result = value;
				eventResult.Invoke(result);
			}
		}
		#endregion
		
		
		
		#region Operation
		private Action operationAction = delegate{};
		private Action CreateOperation()
		{
			switch(operation)
			{
				case "+":	return Op_Add;
				case "-":	return Op_Substract;
				case "*":	return Op_Multiply;
				case "/":	return Op_Divide;
				default:	return Op_Null;
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
		
		private void Op_Null()
		{
			xLogger.LogFormat(this,this.name+":Op_Null");
		}
		#endregion
	}
}
#endif