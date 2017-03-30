package mono.com.wikitude.common.tracking;


public class TrackingMapRecorderEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.wikitude.common.tracking.TrackingMapRecorderEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onErrorSavingTrackingMap:(Ljava/lang/String;)V:GetOnErrorSavingTrackingMap_Ljava_lang_String_Handler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"n_onFinishedSavingTrackingMap:(Ljava/io/File;)V:GetOnFinishedSavingTrackingMap_Ljava_io_File_Handler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"n_onTrackingMapRecordingCanceled:()V:GetOnTrackingMapRecordingCanceledHandler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"n_onTrackingMapRecordingQualityChanged:(II)V:GetOnTrackingMapRecordingQualityChanged_IIHandler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"n_onTrackingMapRecordingStateChanged:(Z)V:GetOnTrackingMapRecordingStateChanged_ZHandler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"n_onTrackingMapRecordingUpdate:(Lcom/wikitude/common/tracking/RecognizedTarget;)V:GetOnTrackingMapRecordingUpdate_Lcom_wikitude_common_tracking_RecognizedTarget_Handler:Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerInvoker, Wikitude.SDK\n" +
			"";
		mono.android.Runtime.register ("Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TrackingMapRecorderEventListenerImplementor.class, __md_methods);
	}


	public TrackingMapRecorderEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TrackingMapRecorderEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Wikitude.Common.Tracking.ITrackingMapRecorderEventListenerImplementor, Wikitude.SDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onErrorSavingTrackingMap (java.lang.String p0)
	{
		n_onErrorSavingTrackingMap (p0);
	}

	private native void n_onErrorSavingTrackingMap (java.lang.String p0);


	public void onFinishedSavingTrackingMap (java.io.File p0)
	{
		n_onFinishedSavingTrackingMap (p0);
	}

	private native void n_onFinishedSavingTrackingMap (java.io.File p0);


	public void onTrackingMapRecordingCanceled ()
	{
		n_onTrackingMapRecordingCanceled ();
	}

	private native void n_onTrackingMapRecordingCanceled ();


	public void onTrackingMapRecordingQualityChanged (int p0, int p1)
	{
		n_onTrackingMapRecordingQualityChanged (p0, p1);
	}

	private native void n_onTrackingMapRecordingQualityChanged (int p0, int p1);


	public void onTrackingMapRecordingStateChanged (boolean p0)
	{
		n_onTrackingMapRecordingStateChanged (p0);
	}

	private native void n_onTrackingMapRecordingStateChanged (boolean p0);


	public void onTrackingMapRecordingUpdate (com.wikitude.common.tracking.RecognizedTarget p0)
	{
		n_onTrackingMapRecordingUpdate (p0);
	}

	private native void n_onTrackingMapRecordingUpdate (com.wikitude.common.tracking.RecognizedTarget p0);

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
