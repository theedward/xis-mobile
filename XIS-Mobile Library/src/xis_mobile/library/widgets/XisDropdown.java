package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisDropdownGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.Spinner;

public class XisDropdown extends Spinner {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisDropdown(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisDropdown(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisDropdown(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public void setXisGestureManager(XisDropdownGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
