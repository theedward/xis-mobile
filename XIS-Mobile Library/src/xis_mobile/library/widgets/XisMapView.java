package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.gms.maps.SupportMapFragment;

public class XisMapView extends SupportMapFragment {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisMapView() {
		super();
	}
	
	// FIXME: Review XisGestureOnTouchListener creation
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View v = super.onCreateView(inflater, container, savedInstanceState);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(getActivity());
		v.setOnTouchListener(mXisGestureOnTouchListener);
		return v;
	}
	
	public void setXisGestureManager(XisGestureManager xisGestureManager) {
		mXisGestureOnTouchListener.setXisGestureManager(xisGestureManager);
	}
}
