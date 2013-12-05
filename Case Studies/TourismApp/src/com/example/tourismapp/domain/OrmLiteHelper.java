package com.example.tourismapp.domain;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;

import com.j256.ormlite.android.apptools.OrmLiteSqliteOpenHelper;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.GenericRawResults;
import com.j256.ormlite.support.ConnectionSource;
import com.j256.ormlite.table.TableUtils;


public class OrmLiteHelper extends OrmLiteSqliteOpenHelper {
 
	private static final String TAG = "OrmLiteHelper";
	private static final String DB_NAME = "Tourism.db";
	private static final int DB_VERSION = 1;
	private static OrmLiteHelper instance = null;
	private Context context;

	private Dao<TourCategory, Integer> tourCategoryDao = null;
	private Dao<Tour, Integer> tourDao = null;
	private Dao<Category, Integer> categoryDao = null;
	private Dao<POI, Integer> poiDao = null;
	private Dao<Favourite, Integer> favouriteDao = null;
	private Dao<GeoPointTour, Integer> geoPointTourDao = null;
	
	protected OrmLiteHelper(Context context) {
		super(context, DB_NAME, null, DB_VERSION);
		this.context = context;
	}
	
	public static OrmLiteHelper getHelper(Context context) {
		if (instance == null) {
			instance = new OrmLiteHelper(context);
			instance.populateDb();
		}
		return instance;
	}
	
	private void populateDb() {
		TourCategory tcLisbon = new TourCategory("Lisbon Tours", "lisbon_tours");
		TourCategory tcSintra = new TourCategory("Sintra Tours", "sintra_tours");
		createOrUpdateTourCategory(tcLisbon);
		createOrUpdateTourCategory(tcSintra);
		
		Tour highTour = new Tour("Lisbon Highlights Tour", "Compact, cosmopolitan and across the 7 hills, Lisbon is very much a walker's city",
			"lisbon_highlights", 4, 3.6);
		highTour.setTourCategory(tcLisbon);
		createOrUpdateTour(highTour);
		
		Tour belemTour = new Tour("Lisbon Belém Tour", "Stretched out across the river bank just 15 minutes west of the centre, Belém reflects all the grandeur and heroism of the Age of Discovery.",
			"lisbon_belem", 4, 5.7);
		belemTour.setTourCategory(tcLisbon);
		createOrUpdateTour(belemTour);
		
		Tour sintraTour = new Tour("Sintra Palaces Tour", "The most beautiful palaces in Sintra.",
			"sintra_palaces", 5, 6);
		sintraTour.setTourCategory(tcSintra);
		createOrUpdateTour(sintraTour);
		
		Category food = new Category("Food & Drink", "Best places to eat and drink.",
			"food");
		createOrUpdateCategory(food);
		Category hotel = new Category("Hotels", "Best places to sleep.",
			"hotel");
		createOrUpdateCategory(hotel);
		Category attraction = new Category("Attractions", "Best places to visit.",
			"attraction");
		createOrUpdateCategory(attraction);
		
		POI saoJorge = new POI("Castle of São Jorge", "...",
			"sao_jorge", "http://en.wikipedia.org/wiki/Castle_of_S%C3%A3o_Jorge", 38.714364, -9.133526, 1);
		saoJorge.setCategory(attraction);
		createOrUpdatePOI(saoJorge);
		POI se = new POI("Sé de Lisboa (Cathedral)", "...",
			"se", "http://en.wikipedia.org/wiki/S%C3%A9_de_Lisboa", 38.710328, -9.132603, 1);
		se.setCategory(attraction);
		createOrUpdatePOI(se);
		POI rossio = new POI("Rossio (Pedro IV Square)", "...",
			"rossio", "http://en.wikipedia.org/wiki/Rossio", 38.71423, -9.138955, 1);
		rossio.setCategory(attraction);
		createOrUpdatePOI(rossio);
		POI gloria = new POI("Calçada da Glória (Funicular)", "...",
			"gloria", "http://en.wikipedia.org/wiki/Elevador_da_Gl%C3%B3ria", 38.715552, -9.143482, 1);
		gloria.setCategory(attraction);
		createOrUpdatePOI(gloria);
		
		GeoPointTour g = new GeoPointTour(1);
		g.setPoi(saoJorge);
		g.setTour(highTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(2);
		g.setPoi(se);
		g.setTour(highTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(3);
		g.setPoi(rossio);
		g.setTour(highTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(4);
		g.setPoi(gloria);
		g.setTour(highTour);
		createOrUpdateGeoPointTour(g);
		
		POI presidencia = new POI("Museu da Presidência da República", "...",
			"presidencia", "http://pt.wikipedia.org/wiki/Museu_da_Presid%C3%AAncia_da_Rep%C3%BAblica", 38.701052, -9.201077, 1);
		presidencia.setCategory(attraction);
		createOrUpdatePOI(presidencia);
		POI pasteis = new POI("Pastéis de Belém", "...",
			"pasteis", "http://en.wikipedia.org/wiki/Pastel_de_nata", 38.69792, -9.203263, 1);
		pasteis.setCategory(food);
		createOrUpdatePOI(pasteis);
		POI jeronimos = new POI("Mosteiro dos Jerónimos", "...",
			"jeronimos", "http://en.wikipedia.org/wiki/Mosteiro_dos_jer%C3%B3nimos", 38.701186, -9.206741, 1);
		jeronimos.setCategory(attraction);
		createOrUpdatePOI(jeronimos);
		POI torre = new POI("Torre de Belém", "...",
			"belem_tower", "http://en.wikipedia.org/wiki/Torre_de_Bel%C3%A9m", 38.692444, -9.21612, 1);
		torre.setCategory(attraction);
		createOrUpdatePOI(torre);
		
		g = new GeoPointTour(1);
		g.setPoi(presidencia);
		g.setTour(belemTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(2);
		g.setPoi(pasteis);
		g.setTour(belemTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(3);
		g.setPoi(jeronimos);
		g.setTour(belemTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(4);
		g.setPoi(torre);
		g.setTour(belemTour);
		createOrUpdateGeoPointTour(g);
		
		POI pena = new POI("Palácio da Pena", "...",
			"pena", "http://en.wikipedia.org/wiki/Pal%C3%A1cio_da_Pena", 38.788412, -9.390528, 1);
		pena.setCategory(attraction);
		createOrUpdatePOI(pena);
		POI mouros = new POI("Castelo dos Mouros", "...",
			"mouros", "http://en.wikipedia.org/wiki/Castelo_dos_Mouros", 38.79557, -9.389389, 1);
		mouros.setCategory(attraction);
		createOrUpdatePOI(mouros);
		POI palace = new POI("Palácio Nacional de Sintra", "...",
			"sintra_palace", "http://en.wikipedia.org/wiki/Pal%C3%A1cio_Nacional_de_Sintra", 38.798079, -9.390632, 1);
		palace.setCategory(attraction);
		createOrUpdatePOI(palace);
		POI monserrate = new POI("Palácio de Monserrate", "...",
			"monserrate", "http://en.wikipedia.org/wiki/Pal%C3%A1cio_de_Monserrate", 38.794935, -9.42044, 1);
		monserrate.setCategory(attraction);
		createOrUpdatePOI(monserrate);
		
		g = new GeoPointTour(1);
		g.setPoi(pena);
		g.setTour(sintraTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(2);
		g.setPoi(mouros);
		g.setTour(sintraTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(3);
		g.setPoi(palace);
		g.setTour(sintraTour);
		createOrUpdateGeoPointTour(g);
		g = new GeoPointTour(4);
		g.setPoi(monserrate);
		g.setTour(sintraTour);
		createOrUpdateGeoPointTour(g);
		
		POI ist = new POI("Instituto Superior Técnico (IST)", "Instituto Superior Técnico (IST) is a school of engineering, part of the Universidade de Lisboa (University of Lisbon). Founded in 1911, IST is the largest and the most prestigious school of engineering in Portugal. It is a public school with a large degree of scientific and financial autonomy.",
			"ist", "http://en.wikipedia.org/wiki/Instituto_Superior_T%C3%A9cnico", 38.737616, -9.139387, 1);
		ist.setCategory(attraction);
		createOrUpdatePOI(ist);
		
		POI bairro = new POI("Bairro Alto", "Bairro Alto is a central district of the city of Lisbon, the Portuguese capital. Unlike many of the civil parishes of Lisbon, this region can be commonly explained as a loose association of neighbourhoods, with no formal local political authority but social and historical significance to the urban community of Lisbon.",
			"bairro", "http://en.wikipedia.org/wiki/Bairro_Alto", 38.711433, -9.150631, 1);
		bairro.setCategory(food);
		createOrUpdatePOI(bairro);
	}
 
	public Dao<TourCategory, Integer> getTourCategoryDao() {
		if (tourCategoryDao == null) {
			try {
				tourCategoryDao = getDao(TourCategory.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return tourCategoryDao;
	}
	
	public int createOrUpdateTourCategory(TourCategory tourCategory) {
		int i = -1;
		try {
			tourCategoryDao = getTourCategoryDao();
			i = tourCategoryDao.createOrUpdate(tourCategory).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public TourCategory getTourCategoryById(int id) {
		TourCategory res = null;
		try {
			tourCategoryDao = getTourCategoryDao();
			res = tourCategoryDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<TourCategory> getAllTourCategories() {
		List<TourCategory> list = null;
		try {
			tourCategoryDao = getTourCategoryDao();
			list = tourCategoryDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteTourCategory(TourCategory tourCategory) {
		int i = -1;
		try {
			tourCategoryDao = getTourCategoryDao();
			i = tourCategoryDao.delete(tourCategory);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllTourCategories() {
		int i = -1;
		try {
			tourCategoryDao = getTourCategoryDao();
			List<TourCategory> list = tourCategoryDao.queryForAll();
			i = tourCategoryDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Dao<Tour, Integer> getTourDao() {
		if (tourDao == null) {
			try {
				tourDao = getDao(Tour.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return tourDao;
	}
	
	public int createOrUpdateTour(Tour tour) {
		int i = -1;
		try {
			tourDao = getTourDao();
			i = tourDao.createOrUpdate(tour).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Tour getTourByName(String name) {
		Tour res = null;
		try {
			tourDao = getTourDao();
			res = tourDao.queryBuilder().where().eq("name", name).query().get(0);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<Tour> getToursByPOI(String poiName) {
		List<Tour> list = null;
		try {
			tourDao = getTourDao();
			GenericRawResults<String[]> rawResults = tourDao.queryRaw(
			    "select distinct t.name, t.image, t.totalHours, t.totalKms from tours as t " +
			    "inner join geopointtours as gp " +
			    "on gp.tour_id = t.name " +
			    "inner join pois as p " +
			    "on p.name = gp.poi_id " +
			    "where p.name = '" + poiName + "'");
			List<String[]> results = rawResults.getResults();
				
			if (results.size() > 0) {
				list = new ArrayList<Tour>();
				for (String[] s : results) {
					Tour t = new Tour();
					t.setName(s[0]);
					t.setImage(s[1]);
					t.setTotalHours(Double.valueOf(s[2]));
					t.setTotalKms(Double.valueOf(s[3]));
					list.add(t);
				}
			}
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public List<Tour> getAllTours() {
		List<Tour> list = null;
		try {
			tourDao = getTourDao();
			list = tourDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public List<Tour> getToursByTourCategoryName(String name) {
		List<Tour> list = null;
		try {
			tourDao = getTourDao();
			list = tourDao.queryBuilder().where().eq("tourCategory_id", name).query();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteTour(Tour tour) {
		int i = -1;
		try {
			tourDao = getTourDao();
			i = tourDao.delete(tour);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllTours() {
		int i = -1;
		try {
			tourDao = getTourDao();
			List<Tour> list = tourDao.queryForAll();
			i = tourDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Dao<Category, Integer> getCategoryDao() {
		if (categoryDao == null) {
			try {
				categoryDao = getDao(Category.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return categoryDao;
	}
	
	public int createOrUpdateCategory(Category category) {
		int i = -1;
		try {
			categoryDao = getCategoryDao();
			i = categoryDao.createOrUpdate(category).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Category getCategoryById(int id) {
		Category res = null;
		try {
			categoryDao = getCategoryDao();
			res = categoryDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<Category> getAllCategories() {
		List<Category> list = null;
		try {
			categoryDao = getCategoryDao();
			list = categoryDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteCategory(Category category) {
		int i = -1;
		try {
			categoryDao = getCategoryDao();
			i = categoryDao.delete(category);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllCategories() {
		int i = -1;
		try {
			categoryDao = getCategoryDao();
			List<Category> list = categoryDao.queryForAll();
			i = categoryDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Dao<POI, Integer> getPOIDao() {
		if (poiDao == null) {
			try {
				poiDao = getDao(POI.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return poiDao;
	}
	
	public int createOrUpdatePOI(POI poi) {
		int i = -1;
		try {
			poiDao = getPOIDao();
			i = poiDao.createOrUpdate(poi).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public POI getPOIByName(String name) {
		POI res = null;
		try {
			poiDao = getPOIDao();
			res = poiDao.queryBuilder().where().eq("name", name).queryForFirst();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<POI> getPOIsByCategoryName(String catName) {
		List<POI> res = null;
		try {
			poiDao = getPOIDao();
			res = poiDao.queryBuilder().where().eq("category_id", catName).query();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<POI> getPOIsByTourName(String tourName) {
		List<POI> res = null;
		try {
			poiDao = getPOIDao();
			GenericRawResults<String[]> rawResults = poiDao.queryRaw(
			    "select distinct gp.position, p.name, p.description, p.image, p.latitude, p.longitude from tours as t " +
			    "inner join geopointtours as gp " +
			    "on gp.tour_id = t.name " +
			    "inner join pois as p " +
			    "on p.name = gp.poi_id " +
			    "where t.name = '" + tourName + "'");
			List<String[]> results = rawResults.getResults();
			
			if (results.size() > 0) {
				res = new ArrayList<POI>();
				for (String[] s : results) {
					POI p = new POI();
					p.setName(s[1]);
					p.setDescription(s[2]);
					p.setImage(s[3]);
					p.setLatitude(Double.valueOf(s[4]));
					p.setLongitude(Double.valueOf(s[5]));
					res.add(p);
				}
			}
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<POI> searchPOIs(String text, String catName) {
		List<POI> list = null;
		try {
			poiDao = getPOIDao();
			if (catName == null) {
				list = poiDao.queryBuilder().where().like("name", "%"+ text + "%")
						.or().like("description", "%" + text + "%").query();
			} else {
				list = poiDao.queryBuilder().where().like("name", "%"+ text + "%")
						.or().like("description", "%" + text + "%")
						.and().eq("category_id", catName).query();
			}
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public List<POI> getAllPOIs() {
		List<POI> list = null;
		try {
			poiDao = getPOIDao();
			list = poiDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deletePOI(POI poi) {
		int i = -1;
		try {
			poiDao = getPOIDao();
			i = poiDao.delete(poi);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllPOIs() {
		int i = -1;
		try {
			poiDao = getPOIDao();
			List<POI> list = poiDao.queryForAll();
			i = poiDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Dao<Favourite, Integer> getFavouriteDao() {
		if (favouriteDao == null) {
			try {
				favouriteDao = getDao(Favourite.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return favouriteDao;
	}
	
	public int createOrUpdateFavourite(Favourite favourite) {
		int i = -1;
		try {
			favouriteDao = getFavouriteDao();
			i = favouriteDao.createOrUpdate(favourite).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Favourite getFavouriteById(int id) {
		Favourite res = null;
		try {
			favouriteDao = getFavouriteDao();
			res = favouriteDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<Favourite> getAllFavourites() {
		List<Favourite> list = null;
		try {
			favouriteDao = getFavouriteDao();
			list = favouriteDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteFavourite(Favourite favourite) {
		int i = -1;
		try {
			favouriteDao = getFavouriteDao();
			i = favouriteDao.delete(favourite);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllFavourites() {
		int i = -1;
		try {
			favouriteDao = getFavouriteDao();
			List<Favourite> list = favouriteDao.queryForAll();
			i = favouriteDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Dao<GeoPointTour, Integer> getGeoPointTourDao() {
		if (geoPointTourDao == null) {
			try {
				geoPointTourDao = getDao(GeoPointTour.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return geoPointTourDao;
	}
	
	public int createOrUpdateGeoPointTour(GeoPointTour geoPointTour) {
		int i = -1;
		try {
			geoPointTourDao = getGeoPointTourDao();
			i = geoPointTourDao.createOrUpdate(geoPointTour).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public GeoPointTour getGeoPointTourById(int id) {
		GeoPointTour res = null;
		try {
			geoPointTourDao = getGeoPointTourDao();
			res = geoPointTourDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<GeoPointTour> getAllGeoPointTours() {
		List<GeoPointTour> list = null;
		try {
			geoPointTourDao = getGeoPointTourDao();
			list = geoPointTourDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}

	public void createTables(ConnectionSource connectionSource) throws SQLException {
		TableUtils.createTable(connectionSource, TourCategory.class);
		TableUtils.createTable(connectionSource, Favourite.class);
		TableUtils.createTable(connectionSource, Category.class);
		TableUtils.createTable(connectionSource, Tag.class);
		TableUtils.createTable(connectionSource, Tour.class);
		TableUtils.createTable(connectionSource, POI.class);
		TableUtils.createTable(connectionSource, GeoPointTour.class);
	}

	public void dropTables(ConnectionSource connectionSource) throws SQLException {
		TableUtils.dropTable(connectionSource, TourCategory.class, true);
		TableUtils.dropTable(connectionSource, Favourite.class, true);
		TableUtils.dropTable(connectionSource, Category.class, true);
		TableUtils.dropTable(connectionSource, Tag.class, true);
		TableUtils.dropTable(connectionSource, Tour.class, true);
		TableUtils.dropTable(connectionSource, POI.class, true);
		TableUtils.dropTable(connectionSource, GeoPointTour.class, true);
	}
	
	@Override
	public void close() {
		super.close();
		categoryDao = null;
		tourDao = null;
		poiDao = null;
		favouriteDao = null;
	}

	@Override
	public void onCreate(SQLiteDatabase db, ConnectionSource connectionSource) {
		try {
			Log.i(TAG, "Creating the tables...");
			createTables(connectionSource);
		} catch(SQLException e) {
			Log.e(TAG, "Not possible to create the tables", e);
		}
	}
 
	@Override
	public void onUpgrade(SQLiteDatabase db, ConnectionSource connectionSource, int oldVersion, int newVersion) {
		try {
			Log.i(TAG,
				"Upgrading database from version " + oldVersion + " to "
		        + newVersion + ", which will destroy all old data...");
			dropTables(connectionSource);
			onCreate(db, connectionSource);
		} catch(SQLException e) {
			Log.e(TAG, "Not possible to drop the tables", e);
		}
	}
}
