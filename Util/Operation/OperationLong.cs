#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolOperation
{
	public class OperationLong : BaseM
	{
		private void Awake()
		{
			operationDelegate = CreateOperation();
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
				operationDelegate();
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
				operationDelegate();
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
				operationDelegate = CreateOperation();
			}
		}
		
		[UnityEngine.Serialization.FormerlySerializedAs("eventFloat")]
		public EventLong eventResult;
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
		private delegate void OperationDelegate();
		private OperationDelegate operationDelegate = delegate(){};
		private OperationDelegate CreateOperation()
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
			xDebug.LogExceptionFormat(this,this.name+":Op_Null");
		}
		#endregion
	}
}
#endif