package xismobile.example.todolistapp;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import xismobile.example.todolistapp.OperationLogger.OperationType;
import xismobile.example.todolistapp.domain.OrmLiteHelper;
import xismobile.example.todolistapp.domain.Task;
import xismobile.example.xistodoapp.R;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

public class TaskListActivity extends Activity {
	
//	private String jsonResult;
	private String url = "http://demo--1.azurewebsites.net/JSON.php?f=getToDo";
	private ListView lv;
	private OrmLiteHelper helper;
	private List<Task> tasks;
	private ArrayAdapter<Task> tasksAdapter;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_tasklist);
		lv = (ListView) findViewById(R.id.listViewTasks);
		helper = OrmLiteHelper.getHelper(getApplicationContext());
		lv.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> a, View v, int position,
				long id) {
				Intent intentView = new Intent(getApplicationContext(), EditTaskActivity.class);
				intentView.putExtra("TaskID", id);
				startActivityForResult(intentView, 0);
			}
		});
		registerForContextMenu(lv);
		fillData();
	}
	
	private void fillData() {
		tasks = helper.getAllTasks();
		tasksAdapter = new TaskAdapter(getApplicationContext(), R.layout.tasklist_item, tasks); 
		lv.setAdapter(tasksAdapter);
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		fillData();
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.menu_tasklist, menu);
        return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_add:
			Intent intentAdd = new Intent(getApplicationContext(), EditTaskActivity.class);
			startActivityForResult(intentAdd, 0);
			return true;
		case R.id.action_sync:
			sync();
			return true;
		case R.id.action_delete:
			tasks.clear();
			helper.deleteAllTasks();
			tasksAdapter.notifyDataSetChanged();
			Toast.makeText(getApplicationContext(), "All Tasks deleted!",
				Toast.LENGTH_SHORT).show();
			// TODO: LOG
//			OperationLogger.addtoLog(getApplicationContext(), OperationType.DeleteAll, dt);
			return true;
		default:
			return super.onOptionsItemSelected(item);
		}
	}
	
	@Override
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		super.onCreateContextMenu(menu, v, menuInfo);
		getMenuInflater().inflate(R.menu.context_tasklist, menu);
	}
	
	@Override
	public boolean onContextItemSelected(MenuItem item) {
		AdapterContextMenuInfo info = (AdapterContextMenuInfo) item.getMenuInfo();
		
		switch (item.getItemId()) {
		case R.id.view:
			Intent intentView = new Intent(getApplicationContext(), EditTaskActivity.class);
			intentView.putExtra("TaskID", tasks.get(info.position).getId());	
			startActivity(intentView);
			return true;
		case R.id.edit:
			Intent intentEdit = new Intent(getApplicationContext(), EditTaskActivity.class);
			intentEdit.putExtra("TaskID", tasks.get(info.position).getId());
			startActivity(intentEdit);
			return true;
		case R.id.delete:
			Task t = tasks.get(info.position);
			OperationLogger.addtoLog(getApplicationContext(), OperationType.DeleteTask, t);
			tasks.remove(info.position);
			helper.deleteTask(t);
			tasksAdapter.notifyDataSetChanged();
			Toast.makeText(getApplicationContext(),
				"Task " + t.getTitle() + " deleted!",
				Toast.LENGTH_SHORT).show();
			return true;
		default:
			return super.onContextItemSelected(item);
		}
	}
	
	private void sync() {
		if (isNetworkConnected()) {
			JsonParseTask task = new JsonParseTask(this);
			task.execute(url);
		}
		else {
			Toast.makeText(getApplicationContext(), "No internet connection!", Toast.LENGTH_SHORT).show();
		}
	}
	
	private boolean isNetworkConnected() {
        ConnectivityManager connectivity = (ConnectivityManager) getApplicationContext().getSystemService(Context.CONNECTIVITY_SERVICE);
		if (connectivity != null) 
		{
	      NetworkInfo[] info = connectivity.getAllNetworkInfo();
	      if (info != null) { 
	          for (int i = 0; i < info.length; i++) {
	              if (info[i].getState() == NetworkInfo.State.CONNECTED)
	              {
	                  return true;
	              }
	          }
	      }
  		}
  		return false;
    }
	
	class JsonParseTask extends AsyncTask<String, Void, String> {
		private final ProgressDialog dialog;
		private final Context context;
		
		public JsonParseTask(Context context) {
			this.context = context;
			dialog = new ProgressDialog(context);
		}
		
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			dialog.setMessage("Syncing Tasks...");
			dialog.setCancelable(true);
			dialog.show();
		}
		
		@Override
		protected String doInBackground(String... params) {
			if (OperationLogger.logHasContent(getApplicationContext())) {
				for (String query : OperationLogger.getLog(getApplicationContext())) {
					HttpClient client = new DefaultHttpClient();
					HttpGet get = new HttpGet(query);
					try {
						HttpResponse resp = client.execute(get);
						int statusCode = resp.getStatusLine().getStatusCode();
						if (statusCode == 200) {
							Log.d("WS", "Success: " + query);
						}
					} catch (Exception e) {
						Log.e("WS", "Failed to call the WS!");
					}
				}
			}
			
			HttpClient client = new DefaultHttpClient();
			HttpGet get = new HttpGet(params[0]);
			String res = null;
			try {
				HttpResponse resp = client.execute(get);
				int statusCode = resp.getStatusLine().getStatusCode();
				if (statusCode == 200) {
					res = streamToString(resp.getEntity().getContent());
					Log.d("WS", res);
				}
			} catch (Exception e) {
				Log.e("WS", "Failed to call the WS!");
			}
			return res;
		}
		
		@Override
		protected void onPostExecute(String result) {
			super.onPostExecute(result);
			dialog.dismiss();
			
			if (result != null) {
				try {
					JSONArray arr = new JSONArray(result);
					JSONObject obj = null;
					helper.deleteAllTasks();
					tasks.clear();
					
					for(int i = 0; i < arr.length(); i++) {
						obj = arr.getJSONObject(i);
						Task t = new Task(obj.getString("Title"), obj.getString("Description"),
							obj.getString("Date"));
						t.setExternalId(Integer.parseInt(obj.getString("id")));
						helper.createOrUpdateTask(t);
						tasks.add(t);
					}
					OperationLogger.clearLog(getApplicationContext());
					tasksAdapter.notifyDataSetChanged();
					Toast.makeText(context, "Tasks successfully updated!", Toast.LENGTH_SHORT).show();
				} catch (JSONException e) {
					e.printStackTrace();
				}
			} else {
				Toast.makeText(context, "No result!", Toast.LENGTH_SHORT).show();
			}
		}
		
		private String streamToString(InputStream is) {
			String line = null;
			StringBuilder sb = new StringBuilder();
			BufferedReader br = new BufferedReader(new InputStreamReader(is));
			try {
				while ((line = br.readLine()) != null) {
					sb.append(line);
				}
			} catch (IOException e) {
				e.printStackTrace();
			} finally {
				try {
					br.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
			return sb.toString();
		}
	}
}
