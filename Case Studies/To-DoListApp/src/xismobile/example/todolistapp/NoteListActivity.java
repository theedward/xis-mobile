package xismobile.example.todolistapp;

import java.util.List;

import xismobile.example.todolistapp.domain.Note;
import xismobile.example.todolistapp.domain.OrmLiteHelper;
import xismobile.example.xistodoapp.R;
import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

public class NoteListActivity extends Activity {

	private ListView lv;
	private OrmLiteHelper helper;
	private List<Note> notes;
	private ArrayAdapter<Note> noteAdapter;
	private EditText mEditTextNoteDescription;
	private Button mButtonAdd;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_notelist);
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		notes = helper.getAllNotes();
		lv = (ListView) findViewById(R.id.listViewNotes);
		noteAdapter = new NoteAdapter(getApplicationContext(), R.layout.notelist_item, notes);
		lv.setAdapter(noteAdapter);
		mEditTextNoteDescription = (EditText) findViewById(R.id.editTextNoteDescription);
		mButtonAdd = (Button) findViewById(R.id.buttonAdd);
		mButtonAdd.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				String description = mEditTextNoteDescription.getText().toString();
				if (description != null && !description.equals("")) {
					Note n = new Note(description);
					helper.createOrUpdateNote(n);
					notes.add(n);
					noteAdapter.notifyDataSetChanged();
				}
			}
		});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.note_list, menu);
		return true;
	}
}
