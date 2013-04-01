package 

import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;


public class DummyISActivity extends Activity {


	/** Called when the activity is first created. */
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.dummyis);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.dummyOptions, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.menu_optionb:
			return true;
		case R.id.menu_optiona:
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}
}
