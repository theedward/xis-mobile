package xis_mobile.library.gestures;

public abstract class XisCheckBoxGestureManager implements XisGestureManager {

	public abstract void onTapGesture();
	
	public abstract void onDoubleTapGesture();
	
	public abstract void onLongTapGesture();

	@Override
	public void onPinch() {
		// TODO Auto-generated method stub
	}
	
	@Override
	public void onStretch() {
		// TODO Auto-generated method stub
	}
	
	@Override
	public void onSwipe() {
		// TODO Auto-generated method stub
	}
}
