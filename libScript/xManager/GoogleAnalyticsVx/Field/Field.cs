#if xLibv2
#if xAnalyticsGoogle

namespace xLib.xAnalyticsGoogle
{
	internal class Field
	{
		private readonly string parameter;
		
		internal Field(string parameter)
		{
			this.parameter = parameter;
		}
		
		public override string ToString()
		{
			return parameter;
		}
	}
}
#endif
#endif