package .domain;

import java.sql.SQLException;
import java.util.List;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;

import com.j256.ormlite.android.apptools.OrmLiteSqliteOpenHelper;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.support.ConnectionSource;
import com.j256.ormlite.table.TableUtils;


public class OrmLiteHelper extends OrmLiteSqliteOpenHelper {
 
	private Context context;

	private static final String TAG = "OrmLiteHelper";
	private static final String DB_NAME = "OrmDummy.db";
	private static final int DB_VERSION = 1;
 
	private Dao<Class2, Integer> class2Dao = null;
	private Dao<Class3, Integer> class3Dao = null;
	private Dao<Class4, Integer> class4Dao = null;
 
	public OrmLiteHelper(Context context) {
		super(context, DB_NAME, null, DB_VERSION);
		this.context = context;
	}
 
	public Dao<Class2, Integer> getClass2Dao() throws SQLException {
		if (class2Dao == null) {
			class2Dao = getDao(Class2.class);
		}
		return class2Dao;
	}

	public int createClass2(Class2 class2) {
		int i = -1;
		class2Dao = getClass2Dao();
		try {
			i = class2Dao.create(class2);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Class2 getClass2ById() {
		class2Dao = getClass2Dao();
		Class2 res = null;
		try {
			res = class2Dao.query();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}

	public List<Class2> GetAllClass2s() {
		class2Dao = getClass2Dao();
		List<Class2> list = null;
		try {
			list = class2Dao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}

	public int deleteClass2(class2) {
		int i = -1;
		class2Dao = getClass2Dao();
		try {
			i = class2Dao.delete(class2);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public void deleteAllClass2s() {
		int i = -1;
		class2Dao = getClass2Dao();
		try {
			List<Class2> list = class2Dao.queryForAll();
			i = class2Dao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Dao<Class3, Integer> getClass3Dao() throws SQLException {
		if (class3Dao == null) {
			class3Dao = getDao(Class3.class);
		}
		return class3Dao;
	}

	public int createClass3(Class3 class3) {
		int i = -1;
		class3Dao = getClass3Dao();
		try {
			i = class3Dao.create(class3);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Class3 getClass3ById() {
		class3Dao = getClass3Dao();
		Class3 res = null;
		try {
			res = class3Dao.query();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}

	public List<Class3> GetAllClass3s() {
		class3Dao = getClass3Dao();
		List<Class3> list = null;
		try {
			list = class3Dao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}

	public int deleteClass3(class3) {
		int i = -1;
		class3Dao = getClass3Dao();
		try {
			i = class3Dao.delete(class3);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public void deleteAllClass3s() {
		int i = -1;
		class3Dao = getClass3Dao();
		try {
			List<Class3> list = class3Dao.queryForAll();
			i = class3Dao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Dao<Class4, Integer> getClass4Dao() throws SQLException {
		if (class4Dao == null) {
			class4Dao = getDao(Class4.class);
		}
		return class4Dao;
	}

	public int createClass4(Class4 class4) {
		int i = -1;
		class4Dao = getClass4Dao();
		try {
			i = class4Dao.create(class4);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Class4 getClass4ById() {
		class4Dao = getClass4Dao();
		Class4 res = null;
		try {
			res = class4Dao.query();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}

	public List<Class4> GetAllClass4s() {
		class4Dao = getClass4Dao();
		List<Class4> list = null;
		try {
			list = class4Dao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}

	public int deleteClass4(class4) {
		int i = -1;
		class4Dao = getClass4Dao();
		try {
			i = class4Dao.delete(class4);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public void deleteAllClass4s() {
		int i = -1;
		class4Dao = getClass4Dao();
		try {
			List<Class4> list = class4Dao.queryForAll();
			i = class4Dao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public void createTables(ConnectionSource connectionSource) {
		TableUtils.createTable(connectionSource, Class2.class);
		TableUtils.createTable(connectionSource, Class3.class);
		TableUtils.createTable(connectionSource, Class4.class);
	}

	public void dropTables(ConnectionSource connectionSource) {
		TableUtils.dropTable(connectionSource, Class2.class, true);
		TableUtils.dropTable(connectionSource, Class3.class, true);
		TableUtils.dropTable(connectionSource, Class4.class, true);
	}
	
	@Override
	public void close() {
		super.close();
		class2Dao = null;
		class3Dao = null;
		class4Dao = null;
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
