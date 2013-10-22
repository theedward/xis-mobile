package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisCheckBoxGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.CheckBox;

public class XisCheckBox extends CheckBox {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisCheckBox(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisCheckBox(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisCheckBox(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisCheckBoxGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
