using UnityEngine ;

public static class Toast {

   public static void Show (string message) {
      #if UNITY_EDITOR
      Debug.Log ("Toast: " + message) ;

      #elif UNITY_ANDROID
      AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer") ;
      AndroidJavaObject appActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity") ;

      if (appActivity != null) {
         AndroidJavaClass androidToast = new AndroidJavaClass ("android.widget.Toast") ;
         appActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
            AndroidJavaObject toastObject = androidToast.CallStatic <AndroidJavaObject> ("makeText", appActivity, message, 0) ;
            toastObject.Call ("show") ;
         })) ;
      }

      #endif
   }

}
