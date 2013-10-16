package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisImageGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.ImageView;

public class XisImage extends ImageView {

	private XisGestureOnTouchListener listener;
	
	public XisImage(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisImage(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisImage(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisImageGestureManager manager) {
		listener.setXisGestureManager(manager);
	}
}
