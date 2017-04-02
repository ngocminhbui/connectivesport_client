package mono.com.wikitude.architect;


public class ArchitectView_ArchitectUrlListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.wikitude.architect.ArchitectView.ArchitectUrlListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_urlWasInvoked:(Ljava/lang/String;)Z:GetUrlWasInvoked_Ljava_lang_String_Handler:Wikitude.Architect.ArchitectView/IArchitectUrlListenerInvoker, Wikitude.SDK\n" +
			"";
		mono.android.Runtime.register ("Wikitude.Architect.ArchitectView+IArchitectUrlListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ArchitectView_ArchitectUrlListenerImplementor.class, __md_methods);
	}


	public ArchitectView_ArchitectUrlListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ArchitectView_ArchitectUrlListenerImplementor.class)
			mono.android.TypeManager.Activate ("Wikitude.Architect.ArchitectView+IArchitectUrlListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


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
