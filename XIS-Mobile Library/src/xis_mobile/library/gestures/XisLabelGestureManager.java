package xis_mobile.library.gestures;

public abstract class XisLabelGestureManager implements XisGestureManager {
	
	public abstract void onDoubleTap();
	
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
