package md5cc34af8da4329e09bfedf090c42d5b99;


public class MyBroadcastReceiver
	extends md5214eafb7e7b3b7fcc363a68a6358563f.GcmBroadcastReceiverBase_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("connectivesport.MyBroadcastReceiver, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyBroadcastReceiver.class, __md_methods);
	}


	public MyBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("connectivesport.MyBroadcastReceiver, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
