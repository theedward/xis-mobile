package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisButtonGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.Button;

public class XisButton extends Button {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisButton(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisButton(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisButton(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisButtonGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
