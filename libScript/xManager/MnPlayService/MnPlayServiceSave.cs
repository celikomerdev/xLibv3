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
				xDebug.LogExceptionFormat(this,this.name+"OpenSavedGamesPopup:null");
				return;
			}
			savedGameClient.ShowSelectSavedGameUI(MnLocalize.GetValue("select your saved game").ToTitleExt(),maxDisplayedSavedGames,showCreateSaveUI,showDeleteSaveUI,OnGameSelected);
		}
		
		private void OnGameSelected(SelectUIStatus status, ISavedGameMetadata game)
		{
			if(status != SelectUIStatus.SavedGameSelected)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("game could not be selected").ToTitleExt()+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("game selected").ToTitleExt());
			
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
				StPopupBar.QueueMessage(MnLocalize.GetValue("game could not be opened").ToTitleExt()+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("game opened").ToTitleExt());
			
			if(string.IsNullOrWhiteSpace(game.Filename)) SaveGame(game);
			else LoadGame(game);
		}
		#endregion
		
		
		#region Save
		private void SaveGame(ISavedGameMetadata game)
		{
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
			builder.WithUpdatedDescription("Play Time: "+ TimeSpan.FromTicks(MnSnapshot.ins.PlayTime).ToStringCustom("HH:mm:ss"));
			builder.WithUpdatedPlayedTime(TimeSpan.FromTicks(MnSnapshot.ins.PlayTime));
			SavedGameMetadataUpdate updatedMetadata = builder.Build();
			
			savedGameClient.CommitUpdate(game, updatedMetadata, MnSnapshot.ins.SnapshotByte, OnGameSaved);
		}
		
		private void OnGameSaved(SavedGameRequestStatus status, ISavedGameMetadata game)
		{
			if (status != SavedGameRequestStatus.Success)
			{
				StPopupBar.QueueMessage(MnLocalize.GetValue("game could not be saved").ToTitleExt()+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("game saved").ToTitleExt());
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
				StPopupBar.QueueMessage(MnLocalize.GetValue("game could not be loaded").ToTitleExt()+"\n"+status.ToString());
				return;
			}
			StPopupBar.QueueMessage(MnLocalize.GetValue("game loaded").ToTitleExt());
			MnSnapshot.ins.SnapshotByte = data;
		}
		#endregion
	}
}
#else
using System;

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