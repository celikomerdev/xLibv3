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
		[SerializeField]private string saveName = "SaveFile";
		[SerializeField]private uint maxDisplayedSavedGames = 5;
		[SerializeField]private bool showCreateSaveUI = true;
		[SerializeField]private bool showDeleteSaveUI = true;
		
		#region Select
		public void ShowSavesUI()
		{
			if(CanDebug) Debug.Log($"{this.name}:ShowSavesUI",this);
			if(!PlayGamesPlatform.Instance.IsAuthenticated())
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Please Login"));
				return;
			}
			
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
			if(CanDebug) Debug.Log($"{this.name}:OnGameSelected:status:{status}:game:{game}",this);
			
			if(status != SelectUIStatus.SavedGameSelected)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Select File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Selected"));
			
			bool hasData = !game.IsNull();
			if(hasData && game.IsOpen)
			{
				OnGameOpened(SavedGameRequestStatus.Success,game,hasData);
				return;
			}
			
			string saveNameNew = saveName;
			if(maxDisplayedSavedGames>1) saveNameNew = saveName+"_"+DateTime.UtcNow.Ticks.ToString();
			
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.OpenWithAutomaticConflictResolution(saveNameNew,DataSource.ReadCacheOrNetwork,ConflictResolutionStrategy.UseLongestPlaytime,
			(SavedGameRequestStatus statusOpen, ISavedGameMetadata gameOpen) =>
			{
				OnGameOpened(statusOpen,gameOpen,hasData);
			});
		}
		#endregion
		
		#region Open
		private void OnGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game,bool hasData)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnGameOpened:status:{status}:game:{game}:hasData:{hasData}",this);
			if(status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Open File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Opened"));
			
			if(hasData) LoadGame(game);
			else SaveGame(game);
		}
		#endregion
		
		
		#region Save
		private void SaveGame(ISavedGameMetadata game)
		{
			if(CanDebug) Debug.Log($"{this.name}:SaveGame:game:{game}",this);
			xTimeSpan playTime = TimeSpan.FromTicks(MnSnapshot.PlayTime);
			string description = $"{MnLocalize.GetValue("Total Time")}: {playTime.ToString(@"HH\:mm\:ss")}";
			
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
			builder.WithUpdatedPlayedTime(playTime);
			builder.WithUpdatedDescription(description);
			SavedGameMetadataUpdate updatedMetadata = builder.Build();
			savedGameClient.CommitUpdate(game,updatedMetadata,MnSnapshot.SnapshotByte,OnGameSaved);
		}
		
		private void OnGameSaved(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnGameSaved:status:{status}:game:{game}",this);
			if (status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("Failed To Save File")+"\n"+status);
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("File Saved"));
		}
		#endregion
		
		
		#region Load
		private void LoadGame(ISavedGameMetadata game)
		{
			if(CanDebug) Debug.Log($"{this.name}:LoadGame:game:{game}",this);
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			savedGameClient.ReadBinaryData(game,OnGameLoaded);
		}
		
		private void OnGameLoaded(SavedGameRequestStatus status, byte[] data)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnGameLoaded:status:{status}:lenght:{data.Length}",this);
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
		[SerializeField]private string saveName = "SaveFile";
		[SerializeField]private uint maxDisplayedSavedGames = 5;
		[SerializeField]private bool showCreateSaveUI = true;
		[SerializeField]private bool showDeleteSaveUI = true;
		
		public void ShowSavesUI(){}
		#pragma warning restore
	}
}
#endif
#endif