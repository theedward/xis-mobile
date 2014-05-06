package xis_mobile.library.widgets;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisLinkGestureManager;
import android.content.Context;
import android.text.util.Linkify;
import android.util.AttributeSet;
import android.widget.TextView;

public class XisLink extends TextView {

	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisLink(Context context) {
		super(context);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setAutoLinkMask(Linkify.WEB_URLS);
	}
	
	public XisLink(Context context, AttributeSet attrs) {
		super(context, attrs);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setAutoLinkMask(Linkify.WEB_URLS);
	}
	
	public XisLink(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setAutoLinkMask(Linkify.WEB_URLS);
	}

	public void setXisGestureManager(XisLinkGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
}
