package xis_mobile.library.gestures;

import android.content.Context;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.Toast;

public class XisGestureOnTouchListener implements OnTouchListener {

	private Context mContext;
	private XisGestureListener mXisGestureListener;
	private GestureDetector mGestureDetector;

	public XisGestureOnTouchListener(Context context) {
		super();
		this.mContext = context;
		this.mXisGestureListener = new XisGestureListener(mContext);
		this.mGestureDetector = new GestureDetector(mContext, mXisGestureListener);
	}

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		mGestureDetector.onTouchEvent(event);
		return true;
	}

	public void onPinchOrStretch(MotionEvent event) {
		int action = event.getAction() & MotionEvent.ACTION_MASK;
		
		switch (action) {
			case MotionEvent.ACTION_DOWN: {
				Toast.makeText(mContext, "Pointer Down", Toast.LENGTH_SHORT).show();
				break;
			}
			case MotionEvent.ACTION_UP: {
				Toast.makeText(mContext, "Pointer Up", Toast.LENGTH_SHORT).show();
				break;
			}
			case MotionEvent.ACTION_MOVE: {
				// Toast.makeText(getApplicationContext(), "Pointer Move",
				// Toast.LENGTH_SHORT).show();
				break;
			}
			case MotionEvent.ACTION_POINTER_DOWN: {
				Toast.makeText(mContext, "Other Pointer Down", Toast.LENGTH_SHORT)
						.show();
				break;
			}
			case MotionEvent.ACTION_POINTER_UP: {
				Toast.makeText(mContext, "Other Pointer Up", Toast.LENGTH_SHORT)
						.show();
				break;
			}
			default: break;
		}
	}

	public void setXisGestureManager(XisGestureManager gestureManager) {
		mXisGestureListener.setXisGestureManager(gestureManager);
	}
}
