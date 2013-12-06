package xismobile.example.todolistapp;

import java.util.Calendar;

import xismobile.example.todolistapp.OperationLogger.OperationType;
import xismobile.example.todolistapp.domain.Category;
import xismobile.example.todolistapp.domain.OrmLiteHelper;
import xismobile.example.todolistapp.domain.Task;
import xismobile.example.xistodoapp.R;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.DatePickerDialog.OnDateSetListener;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;


public class EditTaskActivity extends Activity {

	private final Calendar mCalendar = Calendar.getInstance();
	
	private EditText mTitle;
	private EditText mDescription;
	private EditText mDatePickerBtn;
	private Spinner mCategorySpinner;
	private Button mNotesButton;
	private OrmLiteHelper helper;
	private Task task;
	private int taskID = -1;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);	
		setContentView(R.layout.activity_edittask);

		helper = OrmLiteHelper.getHelper(getApplicationContext());
		
		if (getIntent() != null && getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			taskID = (int) extras.getLong("TaskID");
			task = helper.getTaskById(taskID);
		} else {
			task = new Task();
		}
		initWidgets();
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.menu_edittask, menu);
        return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_save:
			task.setTitle(mTitle.getText().toString());
			task.setDescription(mDescription.getText().toString());
			task.setDate(mDatePickerBtn.getText().toString());
			task.setCategory(helper.getCategoryById(1));
			helper.createOrUpdateTask(task);
			if (taskID == -1) {
				OperationLogger.addtoLog(getApplicationContext(), OperationType.CreateTask, task);
			} else {
				OperationLogger.addtoLog(getApplicationContext(), OperationType.UpdateTask, task);
			}
			finish();
			return true;
		case R.id.action_cancel:
			finish();
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}
	
	public void initWidgets() {
		mTitle = (EditText) findViewById(R.id.editTextTitle);
		mDescription = (EditText) findViewById(R.id.editTextDescription);
		mDatePickerBtn = (EditText) findViewById(R.id.buttonDate);
		mCategorySpinner = (Spinner) findViewById(R.id.spinnerCategories);
		
		if (taskID > 0 && task != null) {
			mTitle.setText(task.getTitle());
			mDescription.setText(task.getDescription());
			mDatePickerBtn.setText(task.getDate());
		}
		initMCategorySpinner();
		initMNotesButton();
	}
	
	public void initMNotesButton() {
		mNotesButton = (Button) findViewById(R.id.buttonNotes);
		mNotesButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent intentEdit = new Intent(getApplicationContext(), NoteListActivity.class);
				startActivity(intentEdit);
			}
		});
	}
	
	public void initMCategorySpinner() {
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item);
		for (Category c : helper.getAllCategorys()) {
			adapter.add(c.getName());
		}
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		mCategorySpinner.setPrompt("Select Category...");
		mCategorySpinner.setAdapter(adapter);
	}
	
	public void setDate(View v)
	{
		int yearC = mCalendar.get(Calendar.YEAR);
		int monthC = mCalendar.get(Calendar.MONTH);
		int dayC = mCalendar.get(Calendar.DAY_OF_MONTH);
		
		new DatePickerDialog(this, new OnDateSetListener() {
			@Override
			public void onDateSet(DatePicker view, int year, int monthOfYear,
					int dayOfMonth) {
				mDatePickerBtn.setText(String.format("%s-%s-%s", year, monthOfYear+1,
					dayOfMonth));
				mCalendar.set(year, monthOfYear, dayOfMonth);
			}
		}, yearC, monthC, dayC).show();
	}	
}
