package md5cc34af8da4329e09bfedf090c42d5b99;


public class UserWrapper
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("connectivesport.UserWrapper, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserWrapper.class, __md_methods);
	}


	public UserWrapper () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserWrapper.class)
			mono.android.TypeManager.Activate ("connectivesport.UserWrapper, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
