package xis_mobile.library.gestures;

public abstract class XisLabelGestureManager implements XisGestureManager {
	
	public abstract void onTapGesture();
	
	public abstract void onDoubleTapGesture();
	
	public abstract void onLongTapGesture();
	
	public abstract void onSwipeGesture();

	@Override
	public void onPinchGesture() {
		// TODO Auto-generated method stub
	}
	
	@Override
	public void onStretchGesture() {
		// TODO Auto-generated method stub
	}
}
