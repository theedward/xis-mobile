package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisTextBoxGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.EditText;

public class XisTextBox extends EditText {

	private XisGestureOnTouchListener listener;
	
	public XisTextBox(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisTextBox(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisTextBox(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisTextBoxGestureManager manager) {
		listener.setXisGestureManager(manager);
	}
}
