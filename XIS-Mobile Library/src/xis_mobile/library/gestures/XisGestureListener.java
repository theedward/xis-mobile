package xis_mobile.library.gestures;

import android.content.Context;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;

public class XisGestureListener extends SimpleOnGestureListener {

	private static final int SWIPE_MIN_DISTANCE = 15;
	private static final int SWIPE_MAX_OFF_PATH = 100;
	private static final int SWIPE_THRESHOLD_VELOCITY = 10;
//	private Context mContext;
	private XisGestureManager mGestureManager;

	public XisGestureListener(Context context) {
		super();
//		this.mContext = context;
	}

	@Override
	public boolean onDoubleTap(MotionEvent e) {
		if (mGestureManager != null) {
			mGestureManager.onDoubleTap();
			return true;
		}
//		} else {
//			Toast.makeText(mContext, "DoubleTap", Toast.LENGTH_SHORT).show();
//		}
		return super.onDoubleTap(e);
	}

	@Override
	public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
		float dX = e2.getX() - e1.getX();
		float dY = e1.getY() - e2.getY();

		if (Math.abs(dY) < SWIPE_MAX_OFF_PATH &&
			Math.abs(velocityX) >= SWIPE_THRESHOLD_VELOCITY &&
			Math.abs(dX) >= SWIPE_MIN_DISTANCE) {
			
			if (dX > 0) {
				if (mGestureManager != null) {
					mGestureManager.onSwipe();
					return true;
				}
//				} else {
//					Toast.makeText(mContext, "Right Swipe", Toast.LENGTH_SHORT).show();
//				}
			} else {
				if (mGestureManager != null) {
					mGestureManager.onSwipe();
					return true;
				}
//				} else {
//					Toast.makeText(mContext, "Left Swipe", Toast.LENGTH_SHORT).show();
//				}
			}
		} else if (Math.abs(dX) < SWIPE_MAX_OFF_PATH &&
			Math.abs(velocityY) >= SWIPE_THRESHOLD_VELOCITY &&
			Math.abs(dY) >= SWIPE_MIN_DISTANCE) {

			if (dY > 0) {
				if (mGestureManager != null) {
					mGestureManager.onSwipe();
					return true;
				}
//				} else {
//					Toast.makeText(mContext, "Up Swipe", Toast.LENGTH_SHORT).show();
//				}
			} else {
				if (mGestureManager != null) {
					mGestureManager.onSwipe();
					return true;
				}
//				} else {
//					Toast.makeText(mContext, "Down Swipe", Toast.LENGTH_SHORT).show();
//				}
			}
		}
		return super.onFling(e1, e2, velocityX, velocityY);
	}

	public XisGestureManager getXisGestureManager() {
		return this.mGestureManager;
	}
	
	public void setXisGestureManager(XisGestureManager gestureManager) {
		this.mGestureManager = gestureManager;  
	}
}
