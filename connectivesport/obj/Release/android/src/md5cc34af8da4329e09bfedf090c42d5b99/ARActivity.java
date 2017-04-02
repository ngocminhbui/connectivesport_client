package md5cc34af8da4329e09bfedf090c42d5b99;


public class ARActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.wikitude.architect.ArchitectView.ArchitectUrlListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onStop:()V:GetOnStopHandler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_onLowMemory:()V:GetOnLowMemoryHandler\n" +
			"n_onPostCreate:(Landroid/os/Bundle;)V:GetOnPostCreate_Landroid_os_Bundle_Handler\n" +
			"n_urlWasInvoked:(Ljava/lang/String;)Z:GetUrlWasInvoked_Ljava_lang_String_Handler:Wikitude.Architect.ArchitectView/IArchitectUrlListenerInvoker, Wikitude.SDK\n" +
			"";
		mono.android.Runtime.register ("connectivesport.ARActivity, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ARActivity.class, __md_methods);
	}


	public ARActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ARActivity.class)
			mono.android.TypeManager.Activate ("connectivesport.ARActivity, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onStop ()
	{
		n_onStop ();
	}

	private native void n_onStop ();


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();


	public void onLowMemory ()
	{
		n_onLowMemory ();
	}

	private native void n_onLowMemory ();


	public void onPostCreate (android.os.Bundle p0)
	{
		n_onPostCreate (p0);
	}

	private native void n_onPostCreate (android.os.Bundle p0);


	public boolean urlWasInvoked (java.lang.String p0)
	{
		return n_urlWasInvoked (p0);
	}

	private native boolean n_urlWasInvoked (java.lang.String p0);

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
