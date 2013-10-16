package xis_mobile.library.gestures;

public abstract class XisTimePickerGestureManager implements XisGestureManager {

	public abstract void onTapGesture();
	
	public abstract void onDoubleTapGesture();
	
	public abstract void onLongTapGesture();

	@Override
	public void onPinchGesture() {
		// TODO Auto-generated method stub
	}
	
	@Override
	public void onStretchGesture() {
		// TODO Auto-generated method stub	
	}
	
	@Override
	public void onSwipeGesture() {
		// TODO Auto-generated method stub	
	}
}
