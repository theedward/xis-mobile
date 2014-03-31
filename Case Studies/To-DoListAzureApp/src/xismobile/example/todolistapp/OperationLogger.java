package xismobile.example.todolistapp;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.util.ArrayList;
import java.util.List;

import xismobile.example.todolistapp.domain.Note;
import xismobile.example.todolistapp.domain.Task;
import android.content.Context;

public class OperationLogger {

	public static boolean logHasContent(Context context) {
		File file = new File(context.getFilesDir(), "operation_log.txt");
		return file.length() > 0;
	}
	
	public static void addtoLog(Context context, OperationType operation, Object obj) {
		String query = getQueryString(operation, obj);
		
		if (query != null)
		{
			try {
				File file = new File(context.getFilesDir(), "operation_log.txt");
				
				if (!file.exists()) {
					file.createNewFile();
				}
	 
				FileWriter fw = new FileWriter(file, true);
				BufferedWriter bw = new BufferedWriter(fw);
				bw.write(query);
				bw.newLine();
				bw.close();
				fw.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
	
	public static List<String> getLog(Context context) {
		List<String> log = new ArrayList<String>();
		try {
			File file = new File(context.getFilesDir(), "operation_log.txt");
			FileReader fr = new FileReader(file);
			BufferedReader br = new BufferedReader(fr);
			String line = null;
			
			while ((line = br.readLine()) != null) {
				log.add(line);
			}
			
			br.close();
			fr.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return log;
	}
	
	public static void clearLog(Context context) {
		try {
			File file = new File(context.getFilesDir(), "operation_log.txt");
			FileWriter fw = new FileWriter(file);
			BufferedWriter bw = new BufferedWriter(fw);
			bw.write("");
			bw.close();
			fw.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	private static String getQueryString(OperationType operation, Object obj) {
		String url = "http://demo--1.azurewebsites.net/JSON.php?";
		String query = null;
		
		switch (operation) {
			case CreateTask:
				if (obj instanceof Task) {
					Task t = (Task) obj;
					query = url + String.format("f=addTodo&t=%s&D=%s&d=%s&c=%s",
						t.getTitle(), t.getDescription(), t.getDate(), t.getCategory().getName());
				}
				break;
			case UpdateTask:
				if (obj instanceof Task) {
					Task t = (Task) obj;
					query = url + String.format("f=updateTodo&tid=%s&t=%s&D=%s&d=%s&c=%s",
						t.getExternalId(), t.getTitle(), t.getDescription(), t.getDate(), t.getCategory().getName());
				}
				break;
			case DeleteTask:
				if (obj instanceof Task) {
					Task t = (Task) obj;
					query = url + "f=removeTodo&tid=" + t.getExternalId();
				}
				break;
			case CreateNote:
				if (obj instanceof Note) {
					Note n = (Note) obj;
					query = url + String.format("f=addNote&tid=%s&D=%s",
						n.getTask().getExternalId(), n.getDescription());
				}
				break;
			case UpdateNote:
				if (obj instanceof Note) {
					Note n = (Note) obj;
					query = url + String.format("f=updateNote&nid=%s&D=%s",
						n.getId(), n.getDescription());
				}
				break;
			case DeleteNote:
				if (obj instanceof Note) {
					Note n = (Note) obj;
					query = url + "f=removeNote&nid=" + n.getId();
				}
				break;
			case DeleteAll:
				// TODO: Build DeleteAll query
				break;
		}
		if (query != null) {
			query = query.replace(" ", "%20");
		}
		
		return query;
	}
	
	enum OperationType {
		CreateTask,
		UpdateTask,
		DeleteTask,
		CreateNote,
		UpdateNote,
		DeleteNote,
		DeleteAll
	}
}
