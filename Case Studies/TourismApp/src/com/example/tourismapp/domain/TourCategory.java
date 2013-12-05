package com.example.tourismapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "TourCategories")
public class TourCategory implements Item {

	@DatabaseField(id = true)
	private String name;
	@DatabaseField
	private String image;
	
	public TourCategory() { }
	
	public TourCategory(String name, String image) {
		this.name = name;
		this.image = image;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getImage() {
		return image;
	}

	public void setImage(String image) {
		this.image = image;
	}

	@Override
	public boolean isSection() {
		return false;
	}

	@Override
	public boolean isItem() {
		return false;
	}
}
