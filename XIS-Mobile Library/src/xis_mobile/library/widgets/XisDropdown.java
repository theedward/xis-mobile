package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisDropdownGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.Spinner;

public class XisDropdown extends Spinner {

	private XisGestureOnTouchListener listener;
	
	public XisDropdown(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisDropdown(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisDropdown(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisDropdownGestureManager manager) {
		listener.setXisGestureManager(manager);
	}
}
