package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisLabelGestureManager;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.TextView;

public class XisLabel extends TextView {

	private XisGestureOnTouchListener listener;
	
	public XisLabel(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisLabel(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisLabel(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisLabelGestureManager manager) {
		listener.setXisGestureManager(manager);
	}
}
