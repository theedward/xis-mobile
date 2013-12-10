package com.example.tourismapp;

import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.ListView;
import android.widget.TextView;

import com.example.tourismapp.domain.Favourite;
import com.example.tourismapp.domain.OrmLiteHelper;

public class FavouritesActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<Favourite> favourites;
	private FavouriteAdapter favsAdapter;
	private ListView lv;
	private TextView tv;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_favourites);
		setTitle("Favourites");
//		manager = DomainEntityManager.getManager(getApplicationContext());
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		favourites = helper.getAllFavourites();
		lv = (ListView) findViewById(R.id.listViewFavs);
		tv = (TextView) findViewById(R.id.textViewNoFavs);
		
		if (favourites.size() > 0) {
			registerForContextMenu(lv);
			favsAdapter = new FavouriteAdapter(this, R.layout.poi_row, favourites); 
			lv.setAdapter(favsAdapter);
		} else {
			tv.setText("No Favourites...");
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.favourites_menu, menu);
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
	
	@Override
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		super.onCreateContextMenu(menu, v, menuInfo);
		getMenuInflater().inflate(R.menu.favourites_context, menu);
	}
	
	@Override
	public boolean onContextItemSelected(android.view.MenuItem item) {
		AdapterContextMenuInfo info = (AdapterContextMenuInfo) item.getMenuInfo();
		
		switch (item.getItemId()) {
		case R.id.view: 
			Intent i = new Intent(getApplicationContext(), POIDetailActivity.class);
			i.putExtra("POI", favourites.get(info.position).getName());
			startActivity(i);
			return true;
		case R.id.delete:
			Favourite f = favourites.get(info.position);
			favourites.remove(info.position);
			helper.deleteFavourite(f);
			favsAdapter.notifyDataSetChanged();
			if (favourites.size() == 0) {
				tv.setText("No favourites...");
			}
			return true;
		default:
			return super.onContextItemSelected(item);
		}
	}
}
