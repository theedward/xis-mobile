package xismobile.example.todolistapp;

import java.util.List;

import xismobile.example.todolistapp.domain.Note;
import xismobile.example.xistodoapp.R;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;


public class NoteAdapter extends ArrayAdapter<Note> {

	private Context mContext;
	private int row;
	private List<Note> notes;
	
	public NoteAdapter(Context context, int textViewResourceId, List<Note> list) {
		super(context, textViewResourceId, list);
		this.mContext = context;
		this.row = textViewResourceId;
		this.notes = list;
	}

	@Override
	public int getCount() {
		return notes.size();
	}

	@Override
	public Note getItem(int position) {
		return notes.get(position);
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		NoteViewHolder holder;
		if (convertView == null) {
			LayoutInflater inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			convertView = inflater.inflate(row, null);
			holder = new NoteViewHolder();
			convertView.setTag(holder);
		} else {
			holder = (NoteViewHolder) convertView.getTag();
		}

		if ((notes == null) || ((position + 1) > notes.size()))
			return convertView; // Can't extract item

		Note obj = notes.get(position);
		holder.description = (EditText) convertView.findViewById(R.id.editTextNoteDescription);
		holder.description.setText(obj.getDescription());
		holder.buttonUpdate = (Button) convertView.findViewById(R.id.buttonUpdate);
		holder.buttonDelete = (Button) convertView.findViewById(R.id.buttonDelete);
		
		return convertView;
	}
	
	public static class NoteViewHolder {
		public EditText description;
		public Button buttonUpdate;
		public Button buttonDelete;
	}
}
