package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.content.Context;
import android.util.AttributeSet;
import android.webkit.WebView;
import android.webkit.WebViewClient;

public class XisWebView extends WebView {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisWebView(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setWebViewClient(new WebViewClient());
	}
	
	public XisWebView(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setWebViewClient(new WebViewClient());
	}
	
	public XisWebView(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setWebViewClient(new WebViewClient());
	}

	public void setXisGestureManager(XisGestureManager xisGestureManager) {
		mXisGestureOnTouchListener.setXisGestureManager(xisGestureManager);
	}
}
