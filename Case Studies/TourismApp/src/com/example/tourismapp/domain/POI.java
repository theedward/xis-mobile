package com.example.tourismapp.domain;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "POIs")
public class POI {

	@DatabaseField(id = true)
	private String name;
	@DatabaseField
	private String description;
	@DatabaseField
	private String image;
	@DatabaseField
	private String url;
	@DatabaseField
	private double latitude;
	@DatabaseField
	private double longitude;
	@DatabaseField
	private double rating;
	@DatabaseField(foreign = true)
	private Favourite favourite;
	@DatabaseField(foreign = true)
	private Category category;
	@DatabaseField(foreign = true)
	private Tag tag;
	
	public POI() { }
	
	public POI(String name, String description, String image, String url,
			double latitude, double longitude, double rating) {
		this.name = name;
		this.description = description;
		this.image = image;
		this.url = url;
		this.latitude = latitude;
		this.longitude = longitude;
		this.rating = rating;
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

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public double getLatitude() {
		return latitude;
	}

	public void setLatitude(double latitude) {
		this.latitude = latitude;
	}

	public double getLongitude() {
		return longitude;
	}

	public void setLongitude(double longitude) {
		this.longitude = longitude;
	}

	public double getRating() {
		return rating;
	}

	public void setRating(double rating) {
		this.rating = rating;
	}

	public Favourite getFavourite() {
		return favourite;
	}

	public void setFavourite(Favourite favourite) {
		this.favourite = favourite;
	}

	public Category getCategory() {
		return category;
	}

	public void setCategory(Category category) {
		this.category = category;
	}

	public Tag getTag() {
		return tag;
	}

	public void setTag(Tag tag) {
		this.tag = tag;
	}
}
