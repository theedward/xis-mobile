package ;

import android.os.Bundle;
import android.app.Activity;


public class Class1Activity extends Activity {

	/** Called when the activity is first created. */
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.class1);
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
	}



}
