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

import com.example.tourismapp.domain.Category;
import com.example.tourismapp.domain.Item;
import com.example.tourismapp.domain.SectionItem;
import com.example.tourismapp.domain.TourCategory;


public class SectionedListAdapter extends ArrayAdapter<Item> {

	private Context context;
	private LayoutInflater inflater;
	private List<Item> items;
	
	public SectionedListAdapter(Context context, List<Item> items) {
		super(context, 0, items);
		this.items = items;
		this.inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		this.context = context;
	}

	@Override
	public int getCount() {
		return items.size();
	}

	@Override
	public Item getItem(int position) {
		return items.get(position);
	}

	@Override
	public long getItemId(int position) {
//		return categories.get(position).getId();
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {

		if (items == null && items.size() == 0)
			return convertView;
		
		Item i = items.get(position);
		
		if (i.isSection()) {
			SectionItem s = (SectionItem) i;
			convertView = inflater.inflate(R.layout.list_item_section, null);
			TextView sectionView = (TextView) convertView.findViewById(R.id.list_item_section_text);
			sectionView.setText(s.getTitle());
		} else if (i instanceof TourCategory) {
			TourCategory t = (TourCategory) i;
			convertView = inflater.inflate(R.layout.category_row, null);
			ImageView image = (ImageView) convertView.findViewById(R.id.catIconImage);
			Drawable img = context.getResources().getDrawable(
				context.getResources().getIdentifier(t.getImage(),
				"drawable", context.getPackageName()));
			image.setImageDrawable(img);
			TextView name = (TextView) convertView.findViewById(R.id.catNameLbl);
			name.setText(t.getName());
		} else if (i instanceof Category) {
			Category c = (Category) i;
			convertView = inflater.inflate(R.layout.category_row, null);
			ImageView image = (ImageView) convertView.findViewById(R.id.catIconImage);
			Drawable img = context.getResources().getDrawable(
				context.getResources().getIdentifier(c.getIcon(),
				"drawable", context.getPackageName()));
			image.setImageDrawable(img);
			TextView name = (TextView) convertView.findViewById(R.id.catNameLbl);
			name.setText(c.getName());
		}
		return convertView;
	}
}
