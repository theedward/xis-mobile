package xis_mobile.library.widgets;

import java.util.Calendar;

import xis_mobile.library.gestures.XisDatePickerGestureManager;
import xis_mobile.library.gestures.XisGestureOnTouchListener;
import android.app.DatePickerDialog;
import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;

public class XisDatePicker extends Button {

	private static final Calendar mCalendar = Calendar.getInstance();
	private Context mContext;
	private XisGestureOnTouchListener mXisGestureOnTouchListener;
	
	public XisDatePicker(Context context) {
		super(context);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setDate();
			}
		});
	}
	
	public XisDatePicker(Context context, AttributeSet attrs) {
		super(context, attrs);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setDate();
			}
		});
	}
	
	public XisDatePicker(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		mContext = context;
		mXisGestureOnTouchListener = new XisGestureOnTouchListener(context);
		setOnTouchListener(mXisGestureOnTouchListener);
		setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				setDate();
			}
		});
	}

	public void setXisGestureManager(XisDatePickerGestureManager manager) {
		mXisGestureOnTouchListener.setXisGestureManager(manager);
	}
	
	private void setDate()
	{
		int yearC = mCalendar.get(Calendar.YEAR);
		int monthC = mCalendar.get(Calendar.MONTH);
		int dayC = mCalendar.get(Calendar.DAY_OF_MONTH);
		
		new DatePickerDialog(mContext, new DatePickerDialog.OnDateSetListener() {
			@Override
			public void onDateSet(DatePicker view, int year, int monthOfYear,
				int dayOfMonth) {
				setText(String.format("%s-%s-%s", year, monthOfYear+1,
					dayOfMonth));
				mCalendar.set(year, monthOfYear, dayOfMonth);
			}
		}, yearC, monthC, dayC).show();
	}
}
