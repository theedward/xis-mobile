package .domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;


@DatabaseTable(tableName = "Class2")
public class Class2 {
	
	@DatabaseField(generatedId = true)
    private int id;
	@DatabaseField
	private String name;
	@DatabaseField
	private String property1;
	public List<Class4> class4;
	public List<Class3> class3;
	
	public Class2() {
		// Non-Arg constructor needed by OrmLite
	}
	
	public Class2(String property1, String name) {
		this.name = name;
		this.property1 = property1;
	}
	
	public String getName() {
		return this.name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public String getProperty1() {
		return this.property1;
	}
	
	public void setProperty1(String property1) {
		this.property1 = property1;
	}
}
