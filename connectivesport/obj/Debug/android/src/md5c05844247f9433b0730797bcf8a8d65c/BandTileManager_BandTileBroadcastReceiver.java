package md5c05844247f9433b0730797bcf8a8d65c;


public class BandTileManager_BandTileBroadcastReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Portable.Tiles.BandTileManager+BandTileBroadcastReceiver, Microsoft.Band.Portable, Version=1.3.10.0, Culture=neutral, PublicKeyToken=null", BandTileManager_BandTileBroadcastReceiver.class, __md_methods);
	}


	public BandTileManager_BandTileBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandTileManager_BandTileBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Portable.Tiles.BandTileManager+BandTileBroadcastReceiver, Microsoft.Band.Portable, Version=1.3.10.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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