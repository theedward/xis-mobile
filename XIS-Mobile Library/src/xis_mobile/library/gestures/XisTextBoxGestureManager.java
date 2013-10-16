package xis_mobile.library.gestures;

public abstract class XisTextBoxGestureManager implements XisGestureManager {

	public abstract void onTap();
	
	public abstract void onDoubleTap();
	
	public abstract void onLongTap();
	
	public abstract void onSwipe();

	@Override
	public void onPinch() {
		// TODO Auto-generated method stub
	}
	
	@Override
	public void onStretch() {
		// TODO Auto-generated method stub
	}
}
