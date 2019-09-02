#if xLibv3
using System;

namespace xLib
{
	public class MnSnapshot : SingletonM<MnSnapshot>
	{
		private long playTime;
		public long PlayTime
		{
			get
			{
				return playTime;
			}
			set
			{
				playTime = value;
			}
		}
		
		public byte[] SnapshotByte
		{
			get
			{
				string tempString = SnapshotString;
				byte[] bytes = new byte[tempString.Length*sizeof(char)];
				Buffer.BlockCopy(tempString.ToCharArray(), 0, bytes, 0, bytes.Length);
				return bytes;
			}
			set
			{
				char[] chars = new char[value.Length / sizeof(char)];
				Buffer.BlockCopy(value, 0, chars, 0, value.Length);
				SnapshotString = new string(chars);
			}
		}
		
		public string SnapshotString
		{
			get
			{
				return "";
			}
			set
			{
				// GameSnapshotData snapshotData = JsonConvert.DeserializeObject<GameSnapshotData>(str);
				// DataController.instance.LoadGame(snapshotData);
				// MnLoader.ins.LoadLevel("2-Intro");
			}
		}
	}
}
#endif




// StartCoroutine(DownloadImage(PlayGamesPlatform.Instance.GetUserImageUrl()));
// IEnumerator DownloadImage(string url)
// {
// 	WWW www = WWW.LoadFromCacheOrDownload(url, 1);
// 	yield return www;
// 	if (www.texture != null)
// 	{
// 		OnProfileImageDownloaded?.Invoke(www.texture);
// 	}
// }


			// StPopupWindow.Reset();
			// StPopupWindow.Header(MnLocalize.GetValue("warning").ToTitleExt());
			// StPopupWindow.Body(MnLocalize.GetValue("everything will be deleted").ToTitleExt());
			// StPopupWindow.Accept(MnLocalize.GetValue("reset").ToUpperExt());
			// StPopupWindow.Decline(MnLocalize.GetValue("cancel").ToUpperExt());
			// StPopupWindow.Listener(Listener,true);
			// void Listener(bool result)
			// {
			// 	StPopupWindow.Listener(Listener,false);
			// 	if(!result) return;
			// }
			// StPopupWindow.Show();


	// private GameSnapshotData CreateNewSnapshot()
		// {
		// 	GameSnapshotData snapshotData = new GameSnapshotData();
		// 	snapshotData.UserID = DataController.instance.user_id;
		// 	snapshotData.PlayerName = DataController.instance.playerName;
		// 	snapshotData.Level = DataController.instance.level;
		// 	snapshotData.CurrentMissionID = DataController.instance.currentMissionId;
		// 	snapshotData.Country = DataController.instance.playerCountry;
		// 	snapshotData.VipPoint = DataController.instance.vipPoint;
		// 	snapshotData.Coins = DataController.instance.coins;
		// 	snapshotData.Tickets = DataController.instance.tickets;
		// 	snapshotData.FirstLaunchVersion = ObscuredPrefs.GetString("firstlaunch_check_version");
		// 	snapshotData.OfferRateCompleted = DataController.instance.offerRateCompleted;
		// 	snapshotData.OfferFacebookCompleted = DataController.instance.offerFacebookCompleted;
		// 	snapshotData.OfferTwitterCompleted = DataController.instance.offerTwitterCompleted;
		// 	snapshotData.OfferSigninCompleted = DataController.instance.offerSigninCompleted;
		// 	snapshotData.OfferAppCompleted = DataController.instance.offerAppCompleted;
		// 	snapshotData.CustomizableModeLocked = DataController.instance.customizableModeLocked;
		// 	snapshotData.LastSelectedCarModel = DataController.instance.lastSelectedCarModel;
			
		// 	//trainer states
		// 	snapshotData.shouldDisplayTutorialForControls = DataController.instance.shouldDisplayTutorialForControls;
		// 	snapshotData.shouldDisplayTutorialForHighSpeed = DataController.instance.shouldDisplayTutorialForHighSpeed;
		// 	snapshotData.shouldDisplayTutorialForFrontLift = DataController.instance.shouldDisplayTutorialForFrontLift;
		// 	snapshotData.shouldDisplayTutorialForNitro = DataController.instance.shouldDisplayTutorialForNitro;
		// 	snapshotData.shouldDisplayScenarioVideo = DataController.shouldDisplayScenarioVideo;
		// 	snapshotData.shouldDisplayTrainerForScenario = DataController.instance.shouldDisplayTrainerForScenario;
		// 	snapshotData.shouldDisplayTrainerForFirstMotoBuy = DataController.instance.shouldDisplayTrainerForFirstMotoBuy;
		// 	snapshotData.shouldDisplayFirstRaceFromGarage = DataController.instance.shouldDisplayFirstRaceFromGarage;
		// 	snapshotData.shouldDisplayTrainerForMap = DataController.instance.shouldDisplayTrainerForMap;
		// 	snapshotData.shouldDisplayFirstRaceFromMap = DataController.instance.shouldDisplayFirstRaceFromMap;
		// 	snapshotData.shouldDisplayTrainerForFreeMode = DataController.instance.shouldDisplayTrainerForFreeMode;
		// 	snapshotData.shouldDisplayTrainerForPoliceMode = DataController.instance.shouldDisplayTrainerForPoliceMode;
		// 	snapshotData.shouldDisplayRaceForPoliceMode = DataController.instance.shouldDisplayRaceForPoliceMode;
		// 	snapshotData.shouldDisplayTrainerForCustomizableMode = DataController.instance.shouldDisplayTrainerForCustomizableMode;
		// 	snapshotData.shouldDisplayTrainerForOnlineMode = DataController.instance.shouldDisplayTrainerForOnlineMode;
		// 	snapshotData.shouldDisplayTrainerForCaseOpen = DataController.instance.shouldDisplayTrainerForCaseOpen;
		// 	snapshotData.shouldDisplayTrainerForPolice = DataController.instance.shouldDisplayTrainerForPolice;
		// 	snapshotData.shouldDisplayTrainerForUpgrade = DataController.instance.shouldDisplayTrainerForUpgrade;
		// 	snapshotData.shouldDisplayTrainerForFinalRace = DataController.instance.shouldDisplayTrainerForFinalRace;
		// 	snapshotData.shouldDisplayTrainerForTheEnd = DataController.instance.shouldDisplayTrainerForTheEnd;
			
		// 	//Purchased motos
		// 	snapshotData.purchasedCars = new Dictionary<string, bool>();
		// 	for (int i = 0; i < ApplicationController.instance.cars.Count; i++)
		// 	{
		// 		string model = ApplicationController.instance.cars[i].model;
		// 		bool purchased = DataController.instance.isCarPurchased(model);
		// 		snapshotData.purchasedCars.Add(model, purchased);
		// 	}
			
		// 	//Collected missions
		// 	snapshotData.collectedMissions = new Dictionary<int, bool>();
		// 	foreach (MissionsManager.Mission mission in MissionsManager.instance.missions)
		// 	{
		// 		snapshotData.collectedMissions.Add(mission.id, mission.isCollected);
		// 	}
			
		// 	//Map current race ids
		// 	snapshotData.currentRaceIdByLevel = new Dictionary<int, int>();
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.Village, DataController.instance.getCurrentRaceIdForLevel(LevelType.Village));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.Desert, DataController.instance.getCurrentRaceIdForLevel(LevelType.Desert));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.City, DataController.instance.getCurrentRaceIdForLevel(LevelType.City));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.Winter, DataController.instance.getCurrentRaceIdForLevel(LevelType.Winter));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.Winter2, DataController.instance.getCurrentRaceIdForLevel(LevelType.Winter2));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.City2, DataController.instance.getCurrentRaceIdForLevel(LevelType.City2));
		// 	snapshotData.currentRaceIdByLevel.Add((int)LevelType.Desert2, DataController.instance.getCurrentRaceIdForLevel(LevelType.Desert2));
			
		// 	//Races bonus state
		// 	int[] levelIds = { (int)LevelType.Village, (int)LevelType.Desert, (int)LevelType.City, (int)LevelType.Winter, (int)LevelType.Winter2, (int)LevelType.City2, (int)LevelType.Desert2 };
		// 	snapshotData.raceBonusStateByLevel = new Dictionary<string, bool>();
		// 	foreach (int id in levelIds)
		// 	{
		// 		LevelType levelType = (LevelType)id;
		// 		Maps map = RacingController.instance.getMapFromLevelType(levelType);
		// 		if (map != null)
		// 		{
		// 			if (map.lastRaceId > 0)
		// 			{
		// 				for (int i = 0; i < map.races.Length; i++)
		// 				{
		// 					if (DataController.instance.getRaceBonusState(map.levelType, map.races[i].id))
		// 						snapshotData.raceBonusStateByLevel.Add(map.levelType.ToString() + "_race" + map.races[i].id, true);
		// 				}
		// 			}
		// 		}
		// 	}
		// 	return snapshotData;
		// }