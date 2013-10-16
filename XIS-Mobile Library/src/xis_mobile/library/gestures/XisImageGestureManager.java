package xis_mobile.library.gestures;

public abstract class XisImageGestureManager implements XisGestureManager {

	public abstract void onTap();
	
	public abstract void onDoubleTap();
	
	public abstract void onLongTap();
	
	public abstract void onSwipe();

	public abstract void onPinch();
	
	public abstract void onStretch();
	
}
