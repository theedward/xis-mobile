package com.example.tourismapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "Tours")
public class Tour implements Item {

	@DatabaseField(id = true)
	private String name;
	@DatabaseField
	private String description;
	@DatabaseField
	private String image;
	@DatabaseField
	private double totalHours;
	@DatabaseField
	private double totalKms;
	@DatabaseField(foreign = true)
	private TourCategory tourCategory;
	
	public Tour() { }
	
	public Tour(String name, String description, String image, double totalHours,
		double totalKms) {
		this.name = name;
		this.description = description;
		this.image = image;
		this.totalHours = totalHours;
		this.totalKms = totalKms;
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
	
	public String getImage() {
		return image;
	}

	public void setImage(String image) {
		this.image = image;
	}

	public double getTotalHours() {
		return totalHours;
	}

	public void setTotalHours(double totalHours) {
		this.totalHours = totalHours;
	}

	public double getTotalKms() {
		return totalKms;
	}

	public void setTotalKms(double totalKms) {
		this.totalKms = totalKms;
	}

	public TourCategory getTourCategory() {
		return tourCategory;
	}

	public void setTourCategory(TourCategory tourCategory) {
		this.tourCategory = tourCategory;
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
