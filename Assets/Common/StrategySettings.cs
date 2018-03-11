/*===============================================================================
Copyright (c) 2015 PTC Inc. All Rights Reserved.

Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Vuforia;

public class StrategySettings : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private bool mVuforiaStarted = false;
    public static int mActiveStrategy = 1;
		public GameObject mSet = null;
		public Canvas mKaisetsu = null;
		private DefaultTrackableEventHandler mDTEH = null;


    #endregion //PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS
    void Start ()
    {
        	VuforiaAbstractBehaviour vuforia = FindObjectOfType<VuforiaAbstractBehaviour>();
        	vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        	vuforia.RegisterOnPauseCallback(OnPaused);

				/////////
					mDTEH = FindObjectOfType<DefaultTrackableEventHandler>();
					//mActiveStrategy = SceneSwicher.st;
				////////

    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS


    public void SetStrategy(int new_strategy)
    {
			//基本は1
			if(mActiveStrategy != new_strategy){
				mActiveStrategy = new_strategy;

				if(DefaultTrackableEventHandler.mStatus == TrackableBehaviour.Status.DETECTED ||
						DefaultTrackableEventHandler.mStatus == TrackableBehaviour.Status.TRACKED ||
						DefaultTrackableEventHandler.mStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
							mDTEH.OnTrackingFound();
				}
				Debug.Log(mActiveStrategy);
			}
    }
    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void OnVuforiaStarted()
    {
        mVuforiaStarted = true;
      	SetStrategy(SceneSwicher.st);
				//mActiveStrategy);
    }

    private void OnPaused(bool paused)
    {
        bool appResumed = !paused;
        if (appResumed && mVuforiaStarted)
        {
            // Restore original focus mode when app is resumed
            /*if (mAutofocusEnabled)
                CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            else
                CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);*/

            // Set the torch flag to false on resume (cause the flash torch is switched off by the OS automatically)
            /*mFlashTorchEnabled = false;*/
        }
    }


    #endregion // PRIVATE_METHODS
}
