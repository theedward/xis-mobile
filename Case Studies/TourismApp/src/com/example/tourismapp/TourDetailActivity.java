package com.example.tourismapp;

import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.POI;
import com.example.tourismapp.domain.Tour;

public class TourDetailActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<Tour> tours;
	private List<POI> pois;
	private Tour tour;
	private ImageView imageTour;
	private TextView tvDetails;
	private TextView tvDescription;
	private ListView lvPOIs;
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_tour_detail);
		
		if (getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			String name = extras.getString("TOUR");
			setTitle(name);
//			manager = DomainEntityManager.getManager(getApplicationContext());
			helper = OrmLiteHelper.getHelper(getApplicationContext());
			tour = helper.getTourByName(name);
			tours = helper.getToursByTourCategoryName(tour.getTourCategory().getName());
			pois = helper.getPOIsByTourName(name);
			initWidgets();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.tour_detail_menu, menu);
		return true;
	}

	@Override
	public boolean onMenuItemSelected(int featureId, MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_back:
			startActivity(new Intent(getApplicationContext(), TourListActivity.class));
			return true;
		case R.id.action_map:
			Intent intent = new Intent(getApplicationContext(), MapISActivity.class);
			intent.putExtra("TOUR", tour.getName());
			startActivity(intent);
			return true;	
		default:
			return super.onOptionsItemSelected(item);
		}
	}
	
	private void initWidgets() {
		imageTour = (ImageView) findViewById(R.id.imageViewTour);
		Drawable img = getResources().getDrawable(
			getResources().getIdentifier(tour.getImage(),
			"drawable", getPackageName()));
		imageTour.setImageDrawable(img);
		tvDetails = (TextView) findViewById(R.id.textViewDetails);
		tvDetails.setText(pois.size() + " stops, " + tour.getTotalKms() + "Km, "
			+ tour.getTotalHours() + "h");
		tvDescription = (TextView) findViewById(R.id.textViewDescription);
		tvDescription.setText(tour.getDescription());
		
		lvPOIs = (ListView) findViewById(R.id.listViewPOIs);
		POIAdapter adapter = new POIAdapter(getApplicationContext(), R.layout.poi_row, pois);
		lvPOIs.setAdapter(adapter);
		lvPOIs.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> arg0, View v, int position,
				long id) {
				POI p = pois.get(position);
				Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
				i.putExtra("POI", p.getName());
				startActivity(i);
			}
		});
		
		Button buttonPrevious = (Button) findViewById(R.id.buttonPrevious);
		buttonPrevious.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				int index = -1;
				for (int i = 0; i < tours.size(); i++) {
					if (tours.get(i).getName().equals(tour.getName())) {
						index = i;
						break;
					}
				}
				
				if (index > 0) {
					Tour t = tours.get(index - 1);
					Intent i = new Intent(getApplicationContext(), TourDetailActivity.class);
					i.putExtra("TOUR", t.getName());
					startActivity(i);
				}
			}
		});
		Button buttonNext = (Button) findViewById(R.id.buttonNext);
		buttonNext.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				int index = -1;
				for (int i = 0; i < tours.size(); i++) {
					if (tours.get(i).getName().equals(tour.getName())) {
						index = i;
						break;
					}
				}
				
				if (index != -1 && index < (tours.size() - 1)) {
					Tour t = tours.get(index + 1);
					Intent i = new Intent(getApplicationContext(), TourDetailActivity.class);
					i.putExtra("TOUR", t.getName());
					startActivity(i);
				}
			}
		});
	}
}
