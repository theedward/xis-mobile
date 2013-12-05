package com.example.tourismapp;

import java.util.List;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.example.tourismapp.domain.Favourite;
import com.example.tourismapp.domain.OrmLiteHelper;

public class FavouritesActivity extends Activity {

//	private DomainEntityManager manager;
	private OrmLiteHelper helper;
	private List<Favourite> favourites;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_favourites);
		setTitle("Favourites");
//		manager = DomainEntityManager.getManager(getApplicationContext());
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		favourites = helper.getAllFavourites();
		RelativeLayout layout = (RelativeLayout) findViewById(R.id.layoutFav);
		
		if (favourites.size() > 0) {
			ListView lv = new ListView(getApplicationContext());
			lv.setAdapter(new ArrayAdapter<Favourite>(this,  android.R.layout.simple_list_item_1, favourites));
			layout.addView(lv);
		} else {
			TextView tv = new TextView(getApplicationContext());
			tv.setText("No Favourites...");
			layout.addView(tv);
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.favourites_menu, menu);
		return true;
	}

	@Override
	public boolean onMenuItemSelected(int featureId, MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_back:
			finish();
			return true;	
		default:
			return super.onOptionsItemSelected(item);
		}
	}	
}
