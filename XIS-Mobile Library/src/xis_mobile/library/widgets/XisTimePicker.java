package xis_mobile.library.widgets;

import java.util.Calendar;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisTimePickerGestureManager;
import android.app.TimePickerDialog;
import android.app.TimePickerDialog.OnTimeSetListener;
import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.widget.Button;
import android.widget.TimePicker;

public class XisTimePicker extends Button {

	private static final Calendar mCalendar = Calendar.getInstance();
	private Context mContext;
	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisTimePicker(Context context) {
		super(context);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisTimePicker(Context context, AttributeSet attrs) {
		super(context, attrs);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}
	
	public XisTimePicker(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
	}

	public void setXisGestureManager(XisTimePickerGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
	
	@Override
	public void setOnClickListener(OnClickListener l) {
		super.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setTime();
			}
		});
	}
	
	private void setTime()
	{
		int hourC = mCalendar.get(Calendar.HOUR_OF_DAY);
		int minuteC = mCalendar.get(Calendar.MINUTE);
		
		new TimePickerDialog(mContext, new OnTimeSetListener() {
			@Override
			public void onTimeSet(TimePicker view, int hourOfDay, int minute) {
				setText(String.format("%s:%s", hourOfDay, minute));
				mCalendar.set(hourOfDay, minute);
			}
		}, hourC, minuteC, true);
	}
}
