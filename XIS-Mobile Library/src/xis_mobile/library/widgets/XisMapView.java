package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.gms.maps.SupportMapFragment;

public class XisMapView extends SupportMapFragment {

	private XisGestureOnTouchListener listener;
	
	public XisMapView() {
		super();
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View v = super.onCreateView(inflater, container, savedInstanceState);
		listener = new XisGestureOnTouchListener(null);
		v.setOnTouchListener(listener);
		return v;
	}
	
	public void setXisGestureManager(XisGestureManager xisGestureManager) {
		listener.setXisGestureManager(xisGestureManager);
	}
}
