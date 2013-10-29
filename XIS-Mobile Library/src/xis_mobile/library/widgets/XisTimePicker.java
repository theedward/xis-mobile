package xis_mobile.library.widgets;

import java.util.Calendar;

import xis_mobile.library.gestures.XisGestureOnTouchListener;
import xis_mobile.library.gestures.XisTimePickerGestureManager;
import android.app.TimePickerDialog;
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
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setTime();
			}
		});
	}
	
	public XisTimePicker(Context context, AttributeSet attrs) {
		super(context, attrs);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setTime();
			}
		});
	}
	
	public XisTimePicker(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setTime();
			}
		});
	}

	public void setXisGestureManager(XisTimePickerGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
	
	private void setTime()
	{
		int hourC = mCalendar.get(Calendar.HOUR_OF_DAY);
		int minuteC = mCalendar.get(Calendar.MINUTE);
		
		new TimePickerDialog(mContext, new TimePickerDialog.OnTimeSetListener() {
			@Override
			public void onTimeSet(TimePicker view, int hourOfDay, int minute) {
				setText(String.format("%s:%s", hourOfDay, minute));
				mCalendar.set(Calendar.HOUR_OF_DAY, hourOfDay);
				mCalendar.set(Calendar.MINUTE, minute);
			}
		}, hourC, minuteC, true).show();
	}
}
