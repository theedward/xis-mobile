package com.example.tourismapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "Categories")
public class Category implements Item {

	@DatabaseField(id = true)
	private String name;
	@DatabaseField
	private String description;
	@DatabaseField
	private String image;
	
	public Category() { }
	
	public Category(String name, String description, String icon) {
		this.name = name;
		this.description = description;
		this.image = icon;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getIcon() {
		return image;
	}

	public void setIcon(String icon) {
		this.image = icon;
	}

	@Override
	public boolean isSection() {
		return false;
	}

	@Override
	public boolean isItem() {
		return true;
	}
}
