package xismobile.example.todolistapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "Tasks")
public class Task {
	
	@DatabaseField(generatedId = true)
	private int id;
	@DatabaseField
	private int externalId;
	@DatabaseField
	private String title;
	@DatabaseField
	private String description;
	@DatabaseField
	private String date;
	@DatabaseField(foreign = true)
	private Category category;
	
	public Task() { }
	
	public Task(String title, String description, String date) {
		this.title = title;
		this.description = description;
		this.date = date;
	}
	
	public int getId() {
		return id;
	}
	
	public int getExternalId() {
		return externalId;
	}

	public void setExternalId(int id) {
		this.externalId = id;
	}
	
	public String getTitle() {
		return title;
	}
	
	public void setTitle(String title) {
		this.title = title;
	}
	
	public String getDescription() {
		return description;
	}
	
	public void setDescription(String description) {
		this.description = description;
	}
	
	public String getDate() {
		return date;
	}
	
	public void setDate(String date) {
		this.date = date;
	}

	public Category getCategory() {
		return category;
	}

	public void setCategory(Category category) {
		this.category = category;
	}
}
