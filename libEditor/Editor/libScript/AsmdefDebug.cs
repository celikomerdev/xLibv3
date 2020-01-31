#if xLibv3
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace xLib.libEditor
{
	[InitializeOnLoad]
	internal class AsmdefDebug
	{
		private const string AssemblyReloadEventsEditorPref = "AssemblyReloadEventsTime";
		private const string AssemblyCompilationEventsEditorPref = "AssemblyCompilationEvents";
		
		private static Dictionary<string, DateTime> dictStartTime = new Dictionary<string, DateTime>();
		private static string buildEventTotal = "";
		private static TimeSpan compileTimeTotal = new TimeSpan();
		
		static AsmdefDebug()
		{
			xApp.FillAppVersion();
			CompilationPipeline.assemblyCompilationStarted += CompilationPipelineOnAssemblyCompilationStarted;
			CompilationPipeline.assemblyCompilationFinished += CompilationPipelineOnAssemblyCompilationFinished;
			AssemblyReloadEvents.beforeAssemblyReload += AssemblyReloadEventsOnBeforeAssemblyReload;
			AssemblyReloadEvents.afterAssemblyReload += AssemblyReloadEventsOnAfterAssemblyReload;
		}
		
		private static void CompilationPipelineOnAssemblyCompilationStarted(string assembly)
		{
			dictStartTime[assembly] = DateTime.UtcNow;
		}
		
		private static readonly int ScriptAssembliesPathLen = "Library/ScriptAssemblies/".Length;
		private static void CompilationPipelineOnAssemblyCompilationFinished(string assembly, CompilerMessage[] arg2)
		{
			DateTime time = dictStartTime[assembly];
			TimeSpan timeSpan = DateTime.UtcNow - dictStartTime[assembly];
			compileTimeTotal += timeSpan;
			
			assembly = assembly.Remove(0,ScriptAssembliesPathLen);
			buildEventTotal += string.Format($"{timeSpan.TotalSeconds.ToString("F3")}:{assembly}:\n");
		}
		
		private static void AssemblyReloadEventsOnBeforeAssemblyReload()
		{
			string temp = string.Format($"{compileTimeTotal.TotalSeconds.ToString("F3")}:compileTimeTotal:\n");
			buildEventTotal = temp+buildEventTotal;
			
			EditorPrefs.SetString(AssemblyReloadEventsEditorPref, DateTime.UtcNow.ToBinary().ToString());
			EditorPrefs.SetString(AssemblyCompilationEventsEditorPref, buildEventTotal.ToString());
		}
		
		private static void AssemblyReloadEventsOnAfterAssemblyReload()
		{
			var binString = EditorPrefs.GetString(AssemblyReloadEventsEditorPref);
			
			long bin = 0;
			if (long.TryParse(binString, out bin))
			{
				DateTime date = DateTime.FromBinary(bin);
				TimeSpan timeSpan = DateTime.UtcNow - date;
				string compilationTimes = EditorPrefs.GetString(AssemblyCompilationEventsEditorPref);
				if (!string.IsNullOrEmpty(compilationTimes))
				{
					Debug.Log($"{compilationTimes}");
					Debug.Log($"{timeSpan.TotalSeconds.ToString("F3")}:AssemblyReloadTime");
				}
			}
			
			EditorPrefs.DeleteKey(AssemblyReloadEventsEditorPref);
			EditorPrefs.DeleteKey(AssemblyCompilationEventsEditorPref);
		}
	}
}
#endif
#endif