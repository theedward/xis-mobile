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

import com.example.tourismapp.domain.Favourite;


public class FavouriteAdapter extends ArrayAdapter<Favourite> {

	private Context mContext;
	private int row;
	private List<Favourite> favourites;
	
	public FavouriteAdapter(Context context, int textViewResourceId, List<Favourite> favourites) {
		super(context, textViewResourceId, favourites);
		this.mContext = context;
		this.row = textViewResourceId;
		this.favourites = favourites;
	}

	@Override
	public int getCount() {
		return favourites.size();
	}

	@Override
	public Favourite getItem(int position) {
		return favourites.get(position);
	}

	@Override
	public long getItemId(int position) {
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

		if ((favourites == null) || ((position + 1) > favourites.size()))
			return convertView; // Can't extract item

		Favourite obj = favourites.get(position);
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
