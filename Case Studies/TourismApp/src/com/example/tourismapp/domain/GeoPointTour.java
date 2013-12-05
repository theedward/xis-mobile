package com.example.tourismapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "GeoPointTours")
public class GeoPointTour implements Item {

	@DatabaseField(generatedId = true)
	private long id;
	@DatabaseField
	private int position;
	@DatabaseField(foreign = true)
	private POI poi;
	@DatabaseField(foreign = true)
	private Tour tour;
	
	public GeoPointTour() { }

	public GeoPointTour(int order) {
		this.position = order;
	}

	public long getId() {
		return id;
	}
	
	public int getPosition() {
		return position;
	}

	public void setPosition(int position) {
		this.position = position;
	}
	
	public POI getPoi() {
		return poi;
	}

	public void setPoi(POI poi) {
		this.poi = poi;
	}

	public Tour getTour() {
		return tour;
	}

	public void setTour(Tour tour) {
		this.tour = tour;
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
