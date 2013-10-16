package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.webkit.WebView;

public class XisWebView extends WebView {

	private XisGestureOnTouchListener listener;
	
	public XisWebView(Context context) {
		super(context);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisWebView(Context context, AttributeSet attrs) {
		super(context, attrs);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}
	
	public XisWebView(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		listener = new XisGestureOnTouchListener(context);
		setOnTouchListener(listener);
	}

	public void setXisGestureManager(XisGestureManager xisGestureManager) {
		listener.setXisGestureManager(xisGestureManager);
	}
}
