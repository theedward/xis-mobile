package xismobile.example.todolistapp;

import java.util.List;

import xismobile.example.todolistapp.domain.Task;
import xismobile.example.xistodoapp.R;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.TextView;


public class TaskAdapter extends ArrayAdapter<Task> {

	private Context mContext;
	private int row;
	private List<Task> tasks;
	
	public TaskAdapter(Context context, int textViewResourceId, List<Task> list) {
		super(context, textViewResourceId, list);
		this.mContext = context;
		this.row = textViewResourceId;
		this.tasks = list;
	}

	@Override
	public int getCount() {
		return tasks.size();
	}

	@Override
	public Task getItem(int position) {
		return tasks.get(position);
	}

	@Override
	public long getItemId(int position) {
		return tasks.get(position).getId();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		final TaskViewHolder holder;
		if (convertView == null) {
			LayoutInflater inflater = (LayoutInflater) mContext
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			convertView = inflater.inflate(row, null);
			holder = new TaskViewHolder();
			holder.title = (TextView)convertView.findViewById(R.id.title);
			holder.date = (TextView)convertView.findViewById(R.id.date);
			holder.selected = (CheckBox)convertView.findViewById(R.id.checkBoxSelected);
			holder.selected.setOnCheckedChangeListener(new OnCheckedChangeListener() {
				@Override
				public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
					Task t = (Task) holder.selected.getTag();
					t.setSelected(isChecked);
				}
			});
			convertView.setTag(holder);
			holder.selected.setTag(tasks.get(position));
		} else {
			holder = (TaskViewHolder) convertView.getTag();
			holder.selected.setTag(tasks.get(position));
		}

		if ((tasks == null) || ((position + 1) > tasks.size()))
			return convertView; // Can't extract item

		Task obj = tasks.get(position);
		holder.title.setText(obj.getTitle());
		holder.date.setText(obj.getDate());
		holder.selected.setChecked(obj.isSelected());
		return convertView;
	}
	
	public static class TaskViewHolder {
		public TextView title;
		public TextView date;
		public CheckBox selected;
	}
}
