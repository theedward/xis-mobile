package xismobile.pim.uml2.gen.android.services;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Generalization;
import org.eclipse.uml2.uml.Interface;
import org.eclipse.uml2.uml.InterfaceRealization;
import org.eclipse.uml2.uml.Model;
import org.eclipse.uml2.uml.Property;
import org.eclipse.uml2.uml.Type;

import xismobile.pim.uml2.gen.android.main.Uml2Android;

public class Services {

	public List<String> getReferencedEntities(Class c) {
		List<String> entities = new ArrayList<String>();

		for (Association a : c.getAssociations()) {
			entities.add(a.getEndTypes().get(1).getName());
		}
		return entities;
	}

	public List<Property> getClassAssociationAttributes(Class c) {
		List<Type> endTypes = null;
		Property first = null;
		Property second = null;
		List<Property> memberEnds = null;
		List<Property> result = new ArrayList<Property>();

		for (Association a : c.getAssociations()) {
			endTypes = a.getEndTypes();
			memberEnds = a.getMemberEnds();
			first = memberEnds.get(0);
			second = memberEnds.get(1);
			if (!first.isNavigable() && !second.isNavigable()
					|| first.isNavigable() && second.isNavigable()) {
				if (endTypes.get(0).getName().compareTo(c.getName()) == 0) {
					result.add(second);
				} else {
					result.add(first);
				}
			} else if (first.isNavigable()) {
				if (endTypes.get(0).getName().compareTo(c.getName()) != 0) {
					result.add(first);
				}
			} else if (second.isNavigable()) {
				if (endTypes.get(1).getName().compareTo(c.getName()) != 0) {
					result.add(second);
				}
			}
		}
		return result;
	}

	public String getExtensionAndImplementations(Class c) {
		StringBuilder builder = new StringBuilder();
		List<Generalization> gens = c.getGeneralizations(); 

		if (gens.size() == 1 && isXisInheritance(gens.get(0))) {
			Class cl = (Class) gens.get(0).getTargets()
					.get(0);
			builder.append(" extends ").append(cl.getName());
		}

		if (c.getInterfaceRealizations().size() > 0) {
			if (builder.length() > 0) {
				builder.append(" ");
			}
			builder.append("implements ");
			Interface i;
			for (InterfaceRealization it : c.getInterfaceRealizations()) {
				i = (Interface) it.getTargets().get(0);
				builder.append(i.getName()).append(", ");
			}
			builder.delete(builder.length() - 2, builder.length());
		}
		return builder.toString();
	}

	public List<String> getManyToManyAssociations(Class c) {
		List<String> joinEntities = new ArrayList<String>();
		Property first = null;
		Property second = null;

		for (Association a : c.getAssociations()) {
			first = a.getMemberEnds().get(0);
			second = a.getMemberEnds().get(1);
			if (first.upperBound() == -1 && second.upperBound() == -1) {
				if (a.getEndTypes().get(0).getName().equals(c.getName())) {
					joinEntities.add(toUpperFirst(a.getEndTypes().get(0)
							.getName())
							+ toUpperFirst(a.getEndTypes().get(1).getName()));
				}
			}
		}
		return joinEntities;
	}
	
	public List<Class> getMasterEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisMasterAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}

	public List<Class> getDetailEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisDetailAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getReferenceEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getInteractionSpaceReferencedEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		List<Association> assocs = new ArrayList<Association>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				assocs.add(a);
			}
		}

		for (Association a : assocs) {
			first = a.getEndTypes().get(0);
			second = a.getEndTypes().get(1);
			if (isXisBusinessEntity(first)) {
				bes.add((Class) first);
			} else if (isXisBusinessEntity(second)) {
				bes.add((Class) second);
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisMasterAssociation(a) || isXisDetailAssociation(a)
						|| isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}

		return entities;
	}
	
	public List<Class> getInboundNavigationReferencedEntities(Class c) {
		List<Class> entities = new ArrayList<>();
//		Property first = null;
//		Property second = null;
//		Type endType = null;
//		
////		Element el = c.getOwner();
//		
//		for (Association a : c.getAssociations()) {
//			if (isXisNavigationAssociation(a)) {
//				first = a.getMemberEnds().get(0);
//				second = a.getMemberEnds().get(1);
//				if (first.isNavigable()) {
//					if (a.getEndTypes().get(0).getName().compareTo(c.getName()) != 0) {
////						result.add(first);
//					}
//				}
//				else {
//					if (a.getEndTypes().get(1).getName().compareTo(c.getName()) != 0) {
////						result.add(first);
//					}
//				}
//			}
//		}
		return entities;
	}

	public String getEntityAttributeOfWidget(String value) {
		return value.split(".")[1];
	}
	
	/**
	 * Copies the desired library jar into the target generation folder.
	 * 
	 * @param jar the library jar name
	 */
	public void addLibrary(String jar) {
		try {
			String target = Uml2Android.targetFolderPath + "/libs/";
			JarFile srcFile = new JarFile("generator.jar");
			File destFolder = new File(target);
			File destFile = new File(target + jar);
			
			if (!destFolder.exists()) {
				destFolder.mkdir();
			}
			
			Enumeration<JarEntry> entries = srcFile.entries();
			JarEntry entry = null;
			
			while (entries.hasMoreElements()) {
				entry = (JarEntry) entries.nextElement();
				if (entry.getName().compareTo("libs/" + jar) == 0) {
					break;
	            }
			}
			
			InputStream is = null;
			FileOutputStream os = null;
			
			try {
				is = srcFile.getInputStream(entry);
	            os = new FileOutputStream(destFile);
	            byte[] buffer = new byte[1024];
	            int length;
	            while ((length = is.read(buffer)) > 0) {
	                os.write(buffer, 0, length);
	            }
			} catch (Exception e) {
				e.printStackTrace();
			} finally {
				if (is != null) {
					is.close();
				}
				if (os != null) {
					os.close();
				}
				if (srcFile != null) {
					srcFile.close();
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Copies the desired file into the target generation folder.
	 * 
	 * @param fileName the file name
	 * @param resolution the icon resolution ('low', 'medium', 'high' or 'xhigh')
	 */
	public void addDrawable(String fileName, String resolution) {
		try {
			String folder = getResolutionFolder(resolution);
			
			String target = Uml2Android.targetFolderPath + "/res/" + folder + "/";
			JarFile srcFile = new JarFile("generator.jar");
			File destFolder = new File(target);
			File destFile = new File(target + fileName);
			
			if (!destFolder.exists()) {
				destFolder.mkdirs();
			}
			
			Enumeration<JarEntry> entries = srcFile.entries();
			JarEntry entry = null;
			
			while (entries.hasMoreElements()) {
				entry = (JarEntry) entries.nextElement();
				if (entry.getName().equals("icons/" + folder + "/" + fileName)) {
					break;
	            }
			}
			
			InputStream is = null;
			FileOutputStream os = null;
			
			try {
				is = srcFile.getInputStream(entry);
	            os = new FileOutputStream(destFile);
	            byte[] buffer = new byte[1024];
	            int length;
	            while ((length = is.read(buffer)) > 0) {
	                os.write(buffer, 0, length);
	            }
			} catch (Exception e) {
				e.printStackTrace();
			} finally {
				if (is != null) {
					is.close();
				}
				if (os != null) {
					os.close();
				}
				if (srcFile != null) {
					srcFile.close();
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Stores the value of a string resource to be included in res/values/strings.xml.
	 * 
	 * @param key the name of the string resource
	 * @param value the value of the string resource
	 */
	public void addStringResource(String key, String value) {
		Uml2Android.stringResources.put(key, value);
	}
	
	/**
	 * Retrieves all the string resources to be included in res/values/strings.xml.
	 * 
	 * @param m
	 * @return the string resources in a XML string
	 */
	public String getStringResources(Model m) {
		StringBuilder sb = new StringBuilder();
		String resource = "<string name=\"%s\">%s</string>\n\t"; 
		
		for (String key : Uml2Android.stringResources.keySet()) {
			sb.append(String.format(resource, key, Uml2Android.stringResources.get(key)));
		}
		
		if (sb.length() > 0) {
			sb = sb.delete(sb.length()-2, sb.length()-1);
		}
		return sb.toString();
	}
	
	/**
	 * Orders the widgets of a screen according to their position.
	 * 
	 * @param widgets list of the widgets to be ordered
	 * @return ordered list of widgets
	 */
	public List<Class> orderWidgetsByPosition(List<Class> widgets) {
		// TODO: Implement
		return widgets;
	}
	
	/**
	 * AUXILIARY METHODS REGION
	 */
	private boolean isXisEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisEntity") != null;
	}
	
	private boolean isXisDomainAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDomainAssociation") != null;
	}
	
//	private boolean isXisNavigationAssociation(Association a) {
//		return a.getAppliedStereotype("XIS-Mobile::XisNavigationAssociation") != null;
//	}

	private boolean isXisBusinessEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisBusinessEntity") != null;
	}

	private boolean isXisMasterAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisMasterAssociation") != null;
	}

	private boolean isXisDetailAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDetailAssociation") != null;
	}

	private boolean isXisReferenceAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisReferenceAssociation") != null;
	}
	
	private boolean isXisInheritance(Generalization g) {
		return g.getAppliedStereotype("XIS-Mobile::XisInheritance") != null;
	}
	
	/**
	 * Checks if a string contains the another one specified. 
	 * 
	 * @param s1 the string where the search is performed
	 * @param s2 the string to search for
	 * @return true if string s1 contains s2, false otherwise
	 */
	public boolean stringContains(String s1, String s2) {
		return s1.contains(s2);
	}
	
	/**
	 * Auxiliary method to put the first letter of a string in upper case.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in upper case
	 */
	private String toUpperFirst(String s) {
		return s.substring(0, 1).toUpperCase() + s.substring(1);
	}
	
	/**
	 * Gets the name of the drawable folder according to its resolution.
	 * 
	 * @param resolution the resolution of the drawable
	 * @return the drawable folder name
	 */
	private String getResolutionFolder(String resolution) {
		String folder = "drawable";
		
		if (resolution.equalsIgnoreCase("low")) {
			return folder + "-ldpi";
		} else if (resolution.equalsIgnoreCase("medium")) {
			return folder + "-mdpi";
		} else if (resolution.equalsIgnoreCase("high")) {
			return folder + "-hdpi";
		} else if (resolution.equalsIgnoreCase("xhigh")) {
			return folder + "-xhdpi";
		} else {
			return folder;
		}
	}
}
