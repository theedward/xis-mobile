package xis_mobile.library.gestures;

public abstract class XisDatePickerGestureManager implements XisGestureManager {

	public abstract void onTap();
	
	public abstract void onDoubleTap();
	
	public abstract void onLongTap();

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
