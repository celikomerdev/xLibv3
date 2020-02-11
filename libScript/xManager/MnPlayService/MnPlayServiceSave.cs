#if xLibv3
#if GooglePlayService
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

namespace xLib
{
	public class MnPlayServiceSave : SingletonM<MnPlayServiceSave>
	{
		[SerializeField]private uint maxDisplayedSavedGames = 5;
		[SerializeField]private bool showCreateSaveUI = true;
		[SerializeField]private bool showDeleteSaveUI = true;
		
		
		#region Select
		public void ShowSavesUI()
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			if(savedGameClient == null)
			{
				Debug.LogException(new UnityException($"{this.name}:OpenSavedGamesPopup:null"),this);
				return;
			}
			savedGameClient.ShowSelectSavedGameUI(MnLocalize.GetValue("Select File"),maxDisplayedSavedGames,showCreateSaveUI,showDeleteSaveUI,OnGameSelected);
		}
		
		private static void OnGameSelected(SelectUIStatus status, ISavedGameMetadata game)
		{
			if(status != SelectUIStatus.SavedGameSelected)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Select File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Selected"));
			
			OpenSavedGame(game.Filename);
			
			// if(!game.IsOpen) OnGameOpened(SavedGameRequestStatus.Success,game);
			// else OpenSavedGame(game.Filename);
		}
		#endregion
		
		
		#region Open
		private static void OpenSavedGame(string filename)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.OpenWithAutomaticConflictResolution(filename,DataSource.ReadCacheOrNetwork,ConflictResolutionStrategy.UseLongestPlaytime,OnGameOpened);
		}
		
		private static void OnGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if(status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Open File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Opened"));
			
			if(string.IsNullOrWhiteSpace(game.Filename)) SaveGame(game);
			else LoadGame(game);
		}
		#endregion
		
		
		#region Save
		private static void SaveGame(ISavedGameMetadata game)
		{
			xTimeSpan playTime = TimeSpan.FromTicks(MnSnapshot.PlayTime);
			string description = $"{MnLocalize.GetValue("Total Time")}: {playTime.ToString(@"HH\:mm\:ss")}";
			
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
			builder.WithUpdatedPlayedTime(playTime);
			builder.WithUpdatedDescription(description);
			SavedGameMetadataUpdate updatedMetadata = builder.Build();
			savedGameClient.CommitUpdate(game,updatedMetadata,MnSnapshot.SnapshotByte,OnGameSaved);
		}
		
		private static void OnGameSaved(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if (status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Save File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Saved"));
		}
		#endregion
		
		
		#region Load
		private static void LoadGame(ISavedGameMetadata game)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.ReadBinaryData(game,OnGameLoaded);
		}
		
		private static void OnGameLoaded(SavedGameRequestStatus status, byte[] data)
		{
			if(status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Load File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Loaded"));
			MnSnapshot.SnapshotByte = data;
		}
		#endregion
	}
}
#else
using UnityEngine;

namespace xLib
{
	public class MnPlayServiceSave : SingletonM<MnPlayServiceSave>
	{
		#pragma warning disable
		[SerializeField]private uint maxDisplayedSavedGames = 5;
		[SerializeField]private bool showCreateSaveUI = true;
		[SerializeField]private bool showDeleteSaveUI = true;
		
		public void ShowSavesUI(){}
		#pragma warning restore
	}
}
#endif
#endif