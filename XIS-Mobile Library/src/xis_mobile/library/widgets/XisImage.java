package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisImageGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.ImageView;

public class XisImage extends ImageView {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisImage(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisImage(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisImage(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisImageGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
