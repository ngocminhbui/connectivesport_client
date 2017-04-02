package mono.com.wikitude.architect.services.camera;


public class CameraLifecycleListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.wikitude.architect.services.camera.CameraLifecycleListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCameraOpen:()V:GetOnCameraOpenHandler:Com.Wikitude.Architect.Services.Camera.ICameraLifecycleListenerInvoker, Wikitude.SDK\n" +
			"n_onCameraOpenAbort:()V:GetOnCameraOpenAbortHandler:Com.Wikitude.Architect.Services.Camera.ICameraLifecycleListenerInvoker, Wikitude.SDK\n" +
			"n_onCameraReleased:()V:GetOnCameraReleasedHandler:Com.Wikitude.Architect.Services.Camera.ICameraLifecycleListenerInvoker, Wikitude.SDK\n" +
			"";
		mono.android.Runtime.register ("Com.Wikitude.Architect.Services.Camera.ICameraLifecycleListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CameraLifecycleListenerImplementor.class, __md_methods);
	}


	public CameraLifecycleListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CameraLifecycleListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Wikitude.Architect.Services.Camera.ICameraLifecycleListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCameraOpen ()
	{
		n_onCameraOpen ();
	}

	private native void n_onCameraOpen ();


	public void onCameraOpenAbort ()
	{
		n_onCameraOpenAbort ();
	}

	private native void n_onCameraOpenAbort ();


	public void onCameraReleased ()
	{
		n_onCameraReleased ();
	}

	private native void n_onCameraReleased ();

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
