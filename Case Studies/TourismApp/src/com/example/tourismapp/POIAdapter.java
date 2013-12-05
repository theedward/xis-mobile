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

import com.example.tourismapp.domain.POI;


public class POIAdapter extends ArrayAdapter<POI> {

	private Context mContext;
	private int row;
	private List<POI> pois;
	
	public POIAdapter(Context context, int textViewResourceId, List<POI> pois) {
		super(context, textViewResourceId, pois);
		this.mContext = context;
		this.row = textViewResourceId;
		this.pois = pois;
	}

	@Override
	public int getCount() {
		return pois.size();
	}

	@Override
	public POI getItem(int position) {
		return pois.get(position);
	}

	@Override
	public long getItemId(int position) {
//		return categories.get(position).getId();
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		POIViewHolder holder;
		if (convertView == null) {
			LayoutInflater inflater = (LayoutInflater) mContext
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			convertView = inflater.inflate(row, null);
			holder = new POIViewHolder();
			convertView.setTag(holder);
		} else {
			holder = (POIViewHolder) convertView.getTag();
		}

		if ((pois == null) || ((position + 1) > pois.size()))
			return convertView; // Can't extract item

		POI obj = pois.get(position);
		holder.image = (ImageView) convertView.findViewById(R.id.poiImage);
		Drawable img = mContext.getResources().getDrawable(
			mContext.getResources().getIdentifier(obj.getImage(),
			"drawable", mContext.getPackageName()));
		holder.image.setImageDrawable(img);
		holder.name = (TextView) convertView.findViewById(R.id.poiNameLbl);
		holder.name.setText(obj.getName());
		
		return convertView;
	}
	
	public static class POIViewHolder {
		public ImageView image;
		public TextView name;
	}
}
