package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisCheckBoxGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.CheckBox;

public class XisCheckBox extends CheckBox {

	private XisGestureOnTouchListener listener;
	
	public XisCheckBox(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisCheckBox(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisCheckBox(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisCheckBoxGestureManager manager) {
		listener.setXisGestureManager(manager);
	}
}
