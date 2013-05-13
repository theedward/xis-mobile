package .domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;


@DatabaseTable(tableName = "Class2")
public class Class2 {
	
	public List<Class3> class3;
	@DatabaseField
	private String name;
	
	public Class2() {
		// Non-Arg constructor needed by OrmLite
	}
	
	public Class2(String name) {
		this.name = name;
	}
	
	public String getName() {
		return this.name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
}
