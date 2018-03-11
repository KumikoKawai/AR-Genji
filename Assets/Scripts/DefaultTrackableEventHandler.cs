/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        public static TrackableBehaviour mTrackableBehaviour;
				//////////////////////////////////
				private GameObject[] mSet = null;
				private Canvas mKaisetsu = null;
				public static TrackableBehaviour.Status mStatus;

				public static Renderer[] rendererComponents = null;
				public static Canvas[] mActiveKaisetsu = null;
				public int flg;
				/////////////////////////////////

        #endregion // PRIVATE_MEMBER_VARIABLES


        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
						mSet = GameObject.FindGameObjectsWithTag("Kaisetsu");
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

						foreach(GameObject obj in mSet) {
							mKaisetsu = obj.GetComponentInChildren<Canvas>();
							mKaisetsu.enabled = false;
						}

						//Debug.Log("mTrackableBehaviour中身：" + mTrackableBehaviour);

					//mKaisetsu.enabled = false;
					flg = 0;
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {

					mStatus = newStatus;

            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
								flg = 1;
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        public void OnTrackingFound()
        {
					//Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

					if(rendererComponents != null)
					{
						foreach (Renderer component in rendererComponents)
            {
								component.enabled = false;
            }
						foreach (Canvas component in mActiveKaisetsu)
            {
								component.enabled = false;
            }
					}

					if(flg == 1)
					{
						rendererComponents = GetComponentsInChildren<Renderer>();
						mActiveKaisetsu = GetComponentsInChildren<Canvas>();
						flg = 0;
					}

						if(StrategySettings.mActiveStrategy == 1)
						{
							rendererComponents[0].enabled = true;
							mActiveKaisetsu[0].enabled = true;
						}
						else if(StrategySettings.mActiveStrategy == 2)
						{
							rendererComponents[2].enabled = true;
							mActiveKaisetsu[1].enabled = true;
						}
						else{
							rendererComponents[4].enabled = true;
							mActiveKaisetsu[2].enabled = true;
						}

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        public void OnTrackingLost()
        {

            rendererComponents = GetComponentsInChildren<Renderer>(true);
						mActiveKaisetsu = GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
						foreach (Canvas component in mActiveKaisetsu)
            {
                component.enabled = false;
            }
						/////////////////////////////

						/*foreach(GameObject obj in mSet) {
							Canvas mEnabledKaisetsu = obj.GetComponentInChildren<Canvas>();
							mEnabledKaisetsu.enabled = false;
						}*/

						/////////////////////////////

						//mActiveKaisetsu.enabled = false;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
