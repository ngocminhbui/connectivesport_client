package md5cc34af8da4329e09bfedf090c42d5b99;


public class ViewFriendMapActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.OnMapReadyCallback,
		com.wikitude.common.permission.PermissionManager.PermissionManagerCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"n_onMapReady:(Lcom/google/android/gms/maps/GoogleMap;)V:GetOnMapReady_Lcom_google_android_gms_maps_GoogleMap_Handler:Android.Gms.Maps.IOnMapReadyCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_permissionsDenied:([Ljava/lang/String;)V:GetPermissionsDenied_arrayLjava_lang_String_Handler:Wikitude.Common.Permission.IPermissionManagerPermissionManagerCallbackInvoker, Wikitude.SDK\n" +
			"n_permissionsGranted:(I)V:GetPermissionsGranted_IHandler:Wikitude.Common.Permission.IPermissionManagerPermissionManagerCallbackInvoker, Wikitude.SDK\n" +
			"n_showPermissionRationale:(I[Ljava/lang/String;)V:GetShowPermissionRationale_IarrayLjava_lang_String_Handler:Wikitude.Common.Permission.IPermissionManagerPermissionManagerCallbackInvoker, Wikitude.SDK\n" +
			"";
		mono.android.Runtime.register ("connectivesport.ViewFriendMapActivity, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ViewFriendMapActivity.class, __md_methods);
	}


	public ViewFriendMapActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ViewFriendMapActivity.class)
			mono.android.TypeManager.Activate ("connectivesport.ViewFriendMapActivity, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);


	public void onMapReady (com.google.android.gms.maps.GoogleMap p0)
	{
		n_onMapReady (p0);
	}

	private native void n_onMapReady (com.google.android.gms.maps.GoogleMap p0);


	public void permissionsDenied (java.lang.String[] p0)
	{
		n_permissionsDenied (p0);
	}

	private native void n_permissionsDenied (java.lang.String[] p0);


	public void permissionsGranted (int p0)
	{
		n_permissionsGranted (p0);
	}

	private native void n_permissionsGranted (int p0);


	public void showPermissionRationale (int p0, java.lang.String[] p1)
	{
		n_showPermissionRationale (p0, p1);
	}

	private native void n_showPermissionRationale (int p0, java.lang.String[] p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
