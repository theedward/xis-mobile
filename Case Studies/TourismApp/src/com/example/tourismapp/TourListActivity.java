package com.example.tourismapp;

import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;

import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.Tour;

public class TourListActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<Tour> tours;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_tour_list);
		setTitle("Tours");
//		manager = DomainEntityManager.getManager(getApplicationContext());
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		
		if (getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			String name = extras.getString("TOUR_CATEGORY");
			tours = helper.getToursByTourCategoryName(name);
		} else {
			tours = helper.getAllTours();
		}
		
		TourAdapter adapter = new TourAdapter(this, R.layout.tour_row, tours);
		ListView lvTours = (ListView) findViewById(R.id.listViewTours);
		lvTours.setAdapter(adapter);
		lvTours.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int position,
					long arg3) {
				Tour t = tours.get(position);
				Intent i = new Intent(getApplicationContext(), TourDetailActivity.class);
				i.putExtra("TOUR", t.getName());
				startActivity(i);
			}
		});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.tour_list_menu, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
			case R.id.action_back:
				finish();
				return true;
			default:
				return super.onOptionsItemSelected(item);
		}
	}
}
