package md5cc34af8da4329e09bfedf090c42d5b99;


public class GoalViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("connectivesport.GoalViewHolder, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GoalViewHolder.class, __md_methods);
	}


	public GoalViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == GoalViewHolder.class)
			mono.android.TypeManager.Activate ("connectivesport.GoalViewHolder, connectivesport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
