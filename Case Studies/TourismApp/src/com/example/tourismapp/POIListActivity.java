package com.example.tourismapp;

import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.EditText;
import android.widget.ListView;

import com.example.tourismapp.domain.OrmLiteHelper;
import com.example.tourismapp.domain.POI;

public class POIListActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<POI> pois;
	private String catName;
	private POIAdapter adapter;
	private EditText search;
	private ListView lvPois;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_poi_list);
		setTitle("Points of Interest List");
//		manager = DomainEntityManager.getManager(getApplicationContext());
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		
		if (getIntent().getExtras() != null) {
			Bundle extras = getIntent().getExtras();
			catName = extras.getString("CATEGORY");
			pois = helper.getPOIsByCategoryName(catName);
		} else {
			pois = helper.getAllPOIs();
		}
		
		adapter = new POIAdapter(this, R.layout.poi_row, pois);
		lvPois = (ListView) findViewById(R.id.listViewPois);
		lvPois.setAdapter(adapter);
		lvPois.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> l, View v, int position,
					long id) {
				POI p = pois.get(position);
				Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
				i.putExtra("POI", p.getName());
				if (catName != null) {
					i.putExtra("CATEGORY", catName);
				}
				startActivity(i);
			}
		});
		
		search = (EditText) findViewById(R.id.inputSearch);
		search.addTextChangedListener(new TextWatcher() {
			@Override
			public void onTextChanged(CharSequence s, int start, int before, int count) {
				String txt = search.getText().toString();
				
				if (!txt.equals("")) {
					pois = helper.searchPOIs(txt, catName);
				} else {
					if (catName == null) {
						pois = helper.getAllPOIs();
					} else {
						pois = helper.getPOIsByCategoryName(catName);
					}
				}
				adapter.clear();
//				adapter.addAll(pois);
				for (POI p : pois) {
					adapter.add(p);
				}
				adapter.notifyDataSetChanged();
			}
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
				int after) { }
			@Override
			public void afterTextChanged(Editable s) { }
		});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.poi_list_menu, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
			case R.id.action_back:
				startActivity(new Intent(getApplicationContext(), MainActivity.class));
				return true;
			case R.id.action_map:
				if (catName != null) {
					Intent intent = new Intent(getApplicationContext(), MapISActivity.class);
					intent.putExtra("CATEGORY", catName);
					startActivity(intent);
				} else {
					startActivity(new Intent(getApplicationContext(), MapISActivity.class));
				}
				return true;
			default:
				return super.onOptionsItemSelected(item);
		}
	}
}
