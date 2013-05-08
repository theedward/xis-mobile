package .domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;


@DatabaseTable(tableName = "Class3")
public class Class3 {
	
	
	@DatabaseField
	private String description;
	
	public Class3() {
		// Non-Arg constructor needed by OrmLite
	}
	
	public Class3(String description) {
		this.description = description;
	}
	
	public String getDescription() {
		return this.description;
	}
	
	public void setDescription(String description) {
		this.description = description;
	}
}
