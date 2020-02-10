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
		
		private void OnGameSelected(SelectUIStatus status, ISavedGameMetadata game)
		{
			if(status != SelectUIStatus.SavedGameSelected)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Select File")+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Selected"));
			
			OpenSavedGame(game.Filename);
			
			// if(!game.IsOpen) OnGameOpened(SavedGameRequestStatus.Success,game);
			// else OpenSavedGame(game.Filename);
		}
		#endregion
		
		
		#region Open
		private void OpenSavedGame(string filename)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.OpenWithAutomaticConflictResolution(filename,DataSource.ReadCacheOrNetwork,ConflictResolutionStrategy.UseLongestPlaytime,OnGameOpened);
		}
		
		private void OnGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if(status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Open File")+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Opened"));
			
			if(string.IsNullOrWhiteSpace(game.Filename)) SaveGame(game);
			else LoadGame(game);
		}
		#endregion
		
		
		#region Save
		private void SaveGame(ISavedGameMetadata game)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
			string description = string.Format(@"{0}: {1:HH\:mm\:ss}",MnLocalize.GetValue("Total Time"),(xTimeSpan)TimeSpan.FromTicks(MnSnapshot.PlayTime));
			builder.WithUpdatedDescription(description);
			builder.WithUpdatedPlayedTime(TimeSpan.FromTicks(MnSnapshot.PlayTime));
			SavedGameMetadataUpdate updatedMetadata = builder.Build();
			
			savedGameClient.CommitUpdate(game, updatedMetadata, MnSnapshot.SnapshotByte, OnGameSaved);
		}
		
		private void OnGameSaved(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if (status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Save File")+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Saved"));
		}
		#endregion
		
		
		#region Load
		private void LoadGame(ISavedGameMetadata game)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.ReadBinaryData(game,OnGameLoaded);
		}
		
		private void OnGameLoaded(SavedGameRequestStatus status, byte[] data)
		{
			if(status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Load File")+"\n"+status.ToString());
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