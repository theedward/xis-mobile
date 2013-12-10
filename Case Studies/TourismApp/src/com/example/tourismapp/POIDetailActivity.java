package com.example.tourismapp;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

import com.example.tourismapp.domain.Favourite;
import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.POI;
import com.example.tourismapp.domain.Tour;

public class POIDetailActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<POI> pois;
	private List<Tour> tours;
	private POI poi;
	private String catName;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_poi_detail);
		
		if (getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			String name = extras.getString("POI");
//			manager = DomainEntityManager.getManager(getApplicationContext());
			helper = OrmLiteHelper.getHelper(getApplicationContext());
			catName = extras.getString("CATEGORY");
			if (catName != null) {
				pois = helper.getPOIsByCategoryName(catName);
			} else {
				pois = helper.getAllPOIs();
			}
			poi = helper.getPOIByName(name);
			setTitle(name);
			initWidgets();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.poidetail, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_back:
			startActivity(new Intent(getApplicationContext(), POIListActivity.class));
			return true;
		case R.id.action_map:
			Intent intent = new Intent(getApplicationContext(), MapISActivity.class);
			intent.putExtra("POI", poi.getName());
			startActivity(intent);
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}
	
	private void initWidgets() {
		Button buttonWiki = (Button) findViewById(R.id.buttonWiki);
		buttonWiki.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent i = new Intent(Intent.ACTION_VIEW);
				i.setData(Uri.parse(poi.getUrl()));
				startActivity(i);
			}
		});
		Button buttonSave = (Button) findViewById(R.id.buttonSave);
		buttonSave.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Favourite f = new Favourite(poi.getName(), poi.getImage());
				poi.setFavourite(f);
				helper.createOrUpdateFavourite(f);
				Toast.makeText(getApplicationContext(), "Saved!", Toast.LENGTH_SHORT).show();
			}
		});
		Button buttonPrevious = (Button) findViewById(R.id.buttonPrevious);
		buttonPrevious.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				int index = -1;
				for (int i = 0; i < pois.size(); i++) {
					if (pois.get(i).getName().equals(poi.getName())) {
						index = i;
						break;
					}
				}
				
				if (index > 0) {
					POI p = pois.get(index - 1);
					Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
					i.putExtra("POI", p.getName());
					if (catName != null) {
						i.putExtra("CATEGORY", catName);
					}
					startActivity(i);
				}
			}
		});
		Button buttonNext = (Button) findViewById(R.id.buttonNext);
		buttonNext.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				int index = -1;
				for (int i = 0; i < pois.size(); i++) {
					if (pois.get(i).getName().equals(poi.getName())) {
						index = i;
						break;
					}
				}
				
				if (index != -1 && index < (pois.size() - 1)) {
					POI p = pois.get(index + 1);
					Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
					i.putExtra("POI", p.getName());
					if (catName != null) {
						i.putExtra("CATEGORY", catName);
					}
					startActivity(i);
				}
			}
		});
		
		ImageView image = (ImageView) findViewById(R.id.imageViewPOI);
		Drawable img = getResources().getDrawable(
			getResources().getIdentifier(poi.getImage(),
			"drawable", getPackageName()));
		image.setImageDrawable(img);
		TextView tvDescription = (TextView) findViewById(R.id.textViewDescription);
		tvDescription.setText(poi.getDescription());
		
		tours = helper.getToursByPOI(poi.getName());
		if (tours == null) {
			tours = new ArrayList<Tour>();
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
}
