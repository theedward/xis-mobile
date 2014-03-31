package xismobile.example.todolistapp.domain;

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
 
	private static final String TAG = "OrmLiteHelper";
	private static final String DB_NAME = "ToDo.db";
	private static final int DB_VERSION = 1;
	private static OrmLiteHelper instance = null;

	private Dao<Task, Integer> taskDao = null;
	private Dao<Note, Integer> noteDao = null;
	private Dao<Category, Integer> categoryDao = null;
 
	protected OrmLiteHelper(Context context) {
		super(context, DB_NAME, null, DB_VERSION);
		createOrUpdateCategory(new Category("Work"));
		createOrUpdateCategory(new Category("Personal"));
	}
	
	public static OrmLiteHelper getHelper(Context context) {
		if (instance == null) {
			instance = new OrmLiteHelper(context);
		}
		return instance;
	}
 
	public Dao<Task, Integer> getTaskDao() {
		if (taskDao == null) {
			try {
				taskDao = getDao(Task.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return taskDao;
	}
	
	public int createOrUpdateTask(Task task) {
		int i = -1;
		try {
			taskDao = getTaskDao();
			i = taskDao.createOrUpdate(task).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Task getTaskById(int id) {
		Task res = null;
		try {
			taskDao = getTaskDao();
			res = taskDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<Task> getAllTasks() {
		List<Task> list = null;
		try {
			taskDao = getTaskDao();
			list = taskDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteTask(Task task) {
		int i = -1;
		try {
			taskDao = getTaskDao();
			i = taskDao.delete(task);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllTasks() {
		int i = -1;
		try {
			taskDao = getTaskDao();
			List<Task> list = taskDao.queryForAll();
			i = taskDao.delete(list);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}

	public Dao<Note, Integer> getNoteDao() {
		if (noteDao == null) {
			try {
				noteDao = getDao(Note.class);
			} catch(SQLException e) {
				e.printStackTrace();
			}
		}
		return noteDao;
	}
	
	public int createOrUpdateNote(Note note) {
		int i = -1;
		try {
			noteDao = getNoteDao();
			i = noteDao.createOrUpdate(note).getNumLinesChanged();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public Note getNoteById(int id) {
		Note res = null;
		try {
			noteDao = getNoteDao();
			res = noteDao.queryForId(id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return res;
	}
	
	public List<Note> getAllNotes() {
		List<Note> list = null;
		try {
			noteDao = getNoteDao();
			list = noteDao.queryForAll();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return list;
	}
	
	public int deleteNote(Note note) {
		int i = -1;
		try {
			noteDao = getNoteDao();
			i = noteDao.delete(note);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return i;
	}
	
	public int deleteAllNotes() {
		int i = -1;
		try {
			noteDao = getNoteDao();
			List<Note> list = noteDao.queryForAll();
			i = noteDao.delete(list);
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
	
	public List<Category> getAllCategorys() {
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
	
	public int deleteAllCategorys() {
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

	public void createTables(ConnectionSource connectionSource) throws SQLException {
		TableUtils.createTable(connectionSource, Task.class);
		TableUtils.createTable(connectionSource, Note.class);
		TableUtils.createTable(connectionSource, Category.class);
	}

	public void dropTables(ConnectionSource connectionSource) throws SQLException {
		TableUtils.dropTable(connectionSource, Task.class, true);
		TableUtils.dropTable(connectionSource, Note.class, true);
		TableUtils.dropTable(connectionSource, Category.class, true);
	}
	
	@Override
	public void close() {
		super.close();
		taskDao = null;
		noteDao = null;
		categoryDao = null;
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
