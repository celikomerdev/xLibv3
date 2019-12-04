#if xLibv2
#if xAnalyticsGoogle

namespace xLib.xAnalyticsGoogle
{
	internal class HitBuilderException : HitBuilder<HitBuilderException>
	{
		private string exceptionDescription = "";
		private bool fatal = false;
		
		internal string GetExceptionDescription()
		{
			return exceptionDescription;
		}
		
		internal HitBuilderException SetExceptionDescription(string exceptionDescription)
		{
			if (exceptionDescription != null)
			{
				this.exceptionDescription = exceptionDescription;
			}
			return this;
		}
		
		internal bool IsFatal()
		{
			return fatal;
		}
		
		internal HitBuilderException SetFatal(bool fatal)
		{
			this.fatal = fatal;
			return this;
		}
		
		internal override HitBuilderException GetThis()
		{
			return this;
		}
		
		internal override HitBuilderException Validate()
		{
			return this;
		}
	}
}
#endif
#endif