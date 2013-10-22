package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisLabelGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.TextView;

public class XisLabel extends TextView {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisLabel(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisLabel(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisLabel(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisLabelGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
