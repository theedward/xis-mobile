package com.example.tourismapp;

import java.util.ArrayList;
import java.util.List;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.POI;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.GoogleMap.OnInfoWindowClickListener;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.PolylineOptions;

public class MapISActivity extends FragmentActivity {

	private OrmLiteHelper helper;
	private GoogleMap map;
	private List<POI> pois;
	private LatLng center;
	private boolean isTour = false;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_map_is);
		setTitle("Points of Interest Map");
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		
		if (getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			
			if (extras.getString("POI") != null) {
				String name = extras.getString("POI");
				pois = new ArrayList<POI>();
				pois.add(helper.getPOIByName(name));
			} else if (extras.getString("TOUR") != null) {
				String name = extras.getString("TOUR");
				pois = helper.getPOIsByTourName(name);
				isTour = true;
			} else if (extras.getString("CATEGORY") != null) {
				String name = extras.getString("CATEGORY");
				pois = helper.getPOIsByCategoryName(name);
			}
		} else {
			pois = helper.getAllPOIs();
		}
		
		initMap();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.map_is_menu, menu);
		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
			case R.id.action_back:
				finish();
				return true;
			case R.id.action_list:
				Toast.makeText(getApplicationContext(), "TODO", Toast.LENGTH_SHORT).show();
				return true;
			default:
				return super.onOptionsItemSelected(item);
		}
	}

	private void initMap() {
		if (map == null) {
			SupportMapFragment mv = (SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map);
			map = mv.getMap();
			
			if (map == null) {
				Toast.makeText(getApplicationContext(), "Unable to create the Map!", Toast.LENGTH_SHORT).show();
			} else {
				center = new LatLng(pois.get(0).getLatitude(), pois.get(0).getLongitude());
				map.moveCamera(CameraUpdateFactory.newLatLngZoom(center, 15));
				map.setOnInfoWindowClickListener(new OnInfoWindowClickListener() {
					@Override
					public void onInfoWindowClick(Marker marker) {
						Toast.makeText(getApplicationContext(), marker.getTitle(), Toast.LENGTH_SHORT).show();
						Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
						i.putExtra("POI", marker.getTitle());
						startActivity(i);
					}
				});
				
				if (isTour) {
					PolylineOptions opt = new PolylineOptions();
					opt.width(5).color(Color.BLUE);
					
					for (int i = 0; i < pois.size(); i++) {
						POI p = pois.get(i);
						LatLng pos = new LatLng(p.getLatitude(), p.getLongitude());
						MarkerOptions mOpt = new MarkerOptions();
						mOpt.icon(BitmapDescriptorFactory.fromAsset("number_" + (i+1) + ".png"));
						
						map.addMarker(mOpt.position(pos)
							.title(p.getName())
							.snippet(p.getDescription()));
						opt.add(pos);
					}
					map.addPolyline(opt);
				} else {
					for (POI p : pois) {
						LatLng pos = new LatLng(p.getLatitude(), p.getLongitude());
						map.addMarker(new MarkerOptions().position(pos)
							.title(p.getName()).snippet(p.getDescription()));
					}
				}
			}
		}
	}	
}
