package com.example.tourismapp;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.TextView;

import com.example.tourismapp.domain.Category;
import com.example.tourismapp.domain.Item;
import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.SectionItem;
import com.example.tourismapp.domain.TourCategory;

public class MainActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<Item> items;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
//		manager = DomainEntityManager.getManager(getApplicationContext());
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		
		TextView map = (TextView) findViewById(R.id.buttonMap);
		map.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				startActivity(new Intent(getApplicationContext(), MapISActivity.class));
			}
		});
		TextView search = (TextView) findViewById(R.id.buttonSearch);
		search.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				startActivity(new Intent(getApplicationContext(), POIListActivity.class));
			}
		});
		TextView fav = (TextView) findViewById(R.id.buttonFav);
		fav.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				startActivity(new Intent(getApplicationContext(), FavouritesActivity.class));
			}
		});
		
		ListView lv = (ListView) findViewById(R.id.listViewCat);
		items = new ArrayList<Item>();
		items.add(new SectionItem("Tours"));
		items.addAll(helper.getAllTourCategories());
		
		items.add(new SectionItem("Categories"));
		items.addAll(helper.getAllCategories());
		
		SectionedListAdapter adapter = new SectionedListAdapter(getApplicationContext(), items);
		lv.setAdapter(adapter);
		lv.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> l, View v, int position,
					long id) {
				Item i = items.get(position);
				if (i instanceof TourCategory) {
					TourCategory tc = (TourCategory) i;
					Intent intent = new Intent(getApplicationContext(), TourListActivity.class);
					intent.putExtra("TOUR_CATEGORY", tc.getName());
					startActivity(intent);
				} else if (i instanceof Category) {
					Category c = (Category) i;
					Intent intent = new Intent(getApplicationContext(), POIListActivity.class);
					intent.putExtra("CATEGORY", c.getName());
					startActivity(intent);
				}
			}
		});
	}
}
