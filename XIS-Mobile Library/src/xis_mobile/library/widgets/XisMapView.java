package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;

import com.google.android.maps.MapView;

public class XisMapView extends MapView {

	private XisGestureOnTouchListener listener;
	
	public XisMapView(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisMapView(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisMapView(Context context, String apiKey) {
		super(context, apiKey);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public void setXisGestureManager(XisGestureManager xisGestureManager) {
		listener.setXisGestureManager(xisGestureManager);
	}
}
