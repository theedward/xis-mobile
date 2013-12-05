package xismobile.example.todolistapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "Notes")
public class Note {

	@DatabaseField(generatedId = true)
	private int id;
	@DatabaseField
	private String description;
	@DatabaseField(foreign = true)
	private Task task;

	public Note() { }
	
	public Note(String description) {
		this.description = description;
	}

	public int getId() {
		return id;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}
	
	public Task getTask() {
		return task;
	}

	public void setTask(Task task) {
		this.task = task;
	}
}
