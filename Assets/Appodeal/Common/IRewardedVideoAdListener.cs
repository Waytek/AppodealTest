using System;

namespace AppodealAds.Unity.Common {
	public interface IRewardedVideoAdListener {
		void onRewardedVideoLoaded(bool precache);
		void onRewardedVideoFailedToLoad();
		void onRewardedVideoShown();
		void onRewardedVideoFinished(int amount, string name);
		void onRewardedVideoClosed(bool finished);
	}
}
