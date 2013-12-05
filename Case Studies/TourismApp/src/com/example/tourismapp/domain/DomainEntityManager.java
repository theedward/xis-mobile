package com.example.tourismapp.domain;

import java.util.ArrayList;
import java.util.List;

import android.content.Context;

public class DomainEntityManager {

	private static DomainEntityManager instance = null;
	private Context context;
	private List<TourCategory> tourCategories;
	private List<Tour> tours;
	private List<Category> categories;
	private List<POI> pois;
	private List<Favourite> favourites;

	protected DomainEntityManager(Context context) {
		this.context = context;
		tourCategories = new ArrayList<TourCategory>();
		tourCategories.add(new TourCategory("Lisbon Tours", "lisbon_tours"));
				
		tours = new ArrayList<Tour>();
		tours.add(new Tour("Lisbon Highlights Tour", "Compact, cosmopolitan and across the 7 hills, Lisbon is very much a walker's city",
			"lisbon_highlights", 12, 3.6));
		tours.add(new Tour("Lisbon Belém Tour", "Stretched out across the river bank just 15 minutes west of the centre, Belém reflects all the grandeur and heroism of the Age of Discovery.",
			"lisbon_belem", 12, 5.7));
		
		categories = new ArrayList<Category>();
		categories.add(new Category("Food & Drink", "Best places to eat and drink.",
			"food"));
		categories.add(new Category("Hotels", "Best places to sleep.",
			"hotel"));
		categories.add(new Category("Attractions", "Best places to visit.",
			"attraction"));
		
		pois = new ArrayList<POI>();
		pois.add(new POI("Instituto Superior Técnico (IST)", "Instituto Superior Técnico (IST) is a school of engineering, part of the Universidade de Lisboa (University of Lisbon). Founded in 1911, IST is the largest and the most prestigious school of engineering in Portugal. It is a public school with a large degree of scientific and financial autonomy.",
			"ist", "http://en.wikipedia.org/wiki/Instituto_Superior_T%C3%A9cnico", 0, 0, 1));
		pois.add(new POI("Bairro Alto", "Bairro Alto is a central district of the city of Lisbon, the Portuguese capital. Unlike many of the civil parishes of Lisbon, this region can be commonly explained as a loose association of neighbourhoods, with no formal local political authority but social and historical significance to the urban community of Lisbon.",
			"bairro", "http://en.wikipedia.org/wiki/Bairro_Alto", 0, 0, 1));
		
		favourites = new ArrayList<Favourite>();
	}

	public static DomainEntityManager getManager(Context context) {
		if (instance == null) {
			instance = new DomainEntityManager(context);
		}
		return instance;
	}

	public boolean createTour(Tour tour) {
		return tours.add(tour);
	}
	
	public boolean createCategory(Category category) {
		return categories.add(category);
	}
	
	public boolean createPOI(POI poi) {
		return pois.add(poi);
	}
	
	public boolean createFavourite(Favourite fav) {
		return favourites.add(fav);
	}
	
	public List<POI> getAllPOIs() {
		return pois;
	}
	
	public List<Category> getAllCategories() {
		return categories;
	}
	
	public List<Tour> getAllTours() {
		return tours;
	}
	
	public List<Favourite> getAllFavourites() {
		return favourites;
	}

	public POI getPOIByName(String name) {
		for (POI p : pois) {
			if (p.getName().equals(name)) {
				return p;
			}
		}
		return null;
	}

	public Tour getTourByName(String name) {
		for (Tour t : tours) {
			if (t.getName().equals(name)) {
				return t;
			}
		}
		return null;
	}
}
