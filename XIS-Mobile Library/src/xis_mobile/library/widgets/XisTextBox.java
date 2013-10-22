package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisTextBoxGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.EditText;

public class XisTextBox extends EditText {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisTextBox(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisTextBox(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisTextBox(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisTextBoxGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
