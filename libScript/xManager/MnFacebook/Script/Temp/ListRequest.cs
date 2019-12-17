#if xLibv2
#if Facebook
using System.Collections.Generic;
using Facebook.Unity;
using Newtonsoft.Json;
using UnityEngine;

namespace xLib.xFacebook
{
	public class ListRequest : SingletonM<ListRequest>
	{
		public Transform container;
		public InfoRequest prefabRequest;
		
		public Invitations invitations;
		private List<InfoRequest> requests = new List<InfoRequest>();
		
		public TextAsset json;
		
		public void Refresh()
		{
			FB.API("/me/apprequests",HttpMethod.GET,OnRequestsLoad);
		}
		
		public void GetRequests()
		{
			//FB.API("/me/apprequests",HttpMethod.GET,OnRequestsLoad);
		}
		
		protected void OnRequestsLoad(IResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnRequestsLoad:{0}",result.ToString());
			
			invitations = JsonConvert.DeserializeObject<Invitations>(result.RawResult);
			CleanUI();
			CreateUI();
		}
		
		private void CreateUI()
		{
			requests = new List<InfoRequest>();
			for (int i = 0; i < invitations.data.Length; i++)
			{
				InfoRequest temp = Instantiate(prefabRequest);
				requests.Add(temp);
				temp.Setup(invitations.data[i]);
				
				temp.transform.SetParent(container);
				temp.transform.ResetTransform();
				temp.gameObject.SetActive(true);
			}
		}
		
		private void CleanUI()
		{
			for (int i = 0; i < requests.Count; i++)
			{
				Destroy(requests[i].gameObject);
			}
		}
		
		protected override void OnDisabled()
		{
			//CleanUI();
		}
	}
}
#endif
#endif