package com.example.tourismapp;

import java.util.List;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.tourismapp.domain.Tour;


public class TourAdapter extends ArrayAdapter<Tour> {

	private Context mContext;
	private int row;
	private List<Tour> tours;
	
	public TourAdapter(Context context, int textViewResourceId, List<Tour> tours) {
		super(context, textViewResourceId, tours);
		this.mContext = context;
		this.row = textViewResourceId;
		this.tours = tours;
	}

	@Override
	public int getCount() {
		return tours.size();
	}

	@Override
	public Tour getItem(int position) {
		return tours.get(position);
	}

	@Override
	public long getItemId(int position) {
//		return categories.get(position).getId();
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		TaskViewHolder holder;
		if (convertView == null) {
			LayoutInflater inflater = (LayoutInflater) mContext
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			convertView = inflater.inflate(row, null);
			holder = new TaskViewHolder();
			convertView.setTag(holder);
		} else {
			holder = (TaskViewHolder) convertView.getTag();
		}

		if ((tours == null) || ((position + 1) > tours.size()))
			return convertView; // Can't extract item

		Tour obj = tours.get(position);
		holder.image = (ImageView) convertView.findViewById(R.id.tourImage);
		Drawable img = mContext.getResources().getDrawable(
			mContext.getResources().getIdentifier(obj.getImage(),
			"drawable", mContext.getPackageName()));
		holder.image.setImageDrawable(img);
		holder.name = (TextView) convertView.findViewById(R.id.tourNameLbl);
		holder.name.setText(obj.getName());
		holder.totalHours = (TextView) convertView.findViewById(R.id.totalHoursLbl);
		holder.totalHours.setText(obj.getTotalHours() + "h");
		holder.totalKms = (TextView) convertView.findViewById(R.id.totalKmsLbl);
		holder.totalKms.setText(obj.getTotalKms() + "Km");
		
		return convertView;
	}
	
	public static class TaskViewHolder {
		public ImageView image;
		public TextView name;
		public TextView totalHours;
		public TextView totalKms;
	}
}
