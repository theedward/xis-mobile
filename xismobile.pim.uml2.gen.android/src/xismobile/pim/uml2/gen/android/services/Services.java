package xismobile.pim.uml2.gen.android.services;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
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
import org.eclipse.uml2.uml.Stereotype;
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

		if (gens.size() == 1 && ServiceUtils.isXisInheritance(gens.get(0))) {
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
			if (ServiceUtils.isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisMasterAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity(first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity(second)) {
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
			if (ServiceUtils.isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisDetailAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity(first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity(second)) {
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
			if (ServiceUtils.isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity(first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity(second)) {
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
			if (ServiceUtils.isXisDomainAssociation(a)) {
				assocs.add(a);
			}
		}

		for (Association a : assocs) {
			first = a.getEndTypes().get(0);
			second = a.getEndTypes().get(1);
			if (ServiceUtils.isXisBusinessEntity(first)) {
				bes.add((Class) first);
			} else if (ServiceUtils.isXisBusinessEntity(second)) {
				bes.add((Class) second);
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisMasterAssociation(a) || ServiceUtils.isXisDetailAssociation(a)
						|| ServiceUtils.isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity(first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity(second)) {
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
	 * @param space the interaction space
	 * @param widgets list of the widgets to be ordered
	 * @return ordered list of widgets
	 */
	public List<Class> orderWidgetsByPosition(List<Class> widgets) {
		Collections.sort(widgets, new WidgetComparator());
		return widgets;
	}
	
	public String writeWidgetRelativePositioning(List<Class> widgets, Class c) {
		String newLine = "\n\t";
		StringBuilder sb = new StringBuilder();
		Class space = (Class) c.getOwner();
		Stereotype s = ServiceUtils.getXisInteractionSpace(space);
		int spaceX = ServiceUtils.getPosX(space, s);
		int spaceY = ServiceUtils.getPosY(space, s);
		int spaceWidth = ServiceUtils.getWidth(space, s);
		int spaceHeight = ServiceUtils.getHeight(space, s);
		int spaceLeft = spaceX - spaceWidth/2;
		int spaceRight = spaceX + spaceWidth/2;
		int spaceTop = spaceY - spaceHeight/2;
		int spaceBottom = spaceY + spaceHeight/2;
		
		s = ServiceUtils.getWidgetStereotype(c);
		int posX = ServiceUtils.getPosX(c, s);
		int posY = ServiceUtils.getPosY(c, s);
		int width = ServiceUtils.getWidth(c, s);
		int height = ServiceUtils.getHeight(c, s);
		int left = posX - width/2;
		int right = posX + width/2;
		int top = posY - height/2;
		int bottom = posY + height/2;
		
		List<Class> predecessors = new ArrayList<Class>();
		
		for (Class w : widgets) {
			if (w.getName().equals(c.getName())) {
				break;
			} else {
				predecessors.add(w);
			}
		}
		
		if (posX == spaceX) {
			sb.append("android:layout_centerHorizontal=\"true\"");
		}
		
		if(predecessors.size() > 0) {
			if (sb.length() > 0) {
				// only Y is missing
				int closerTop = spaceTop;
				int closerMargin = top - spaceTop - 35;
				Class closer = null;
				
				for (Class w : predecessors) {
					Stereotype wStereo = ServiceUtils.getWidgetStereotype(w);
					int wTop = ServiceUtils.getPosY(w, wStereo) - ServiceUtils.getHeight(w, wStereo)/2;
					
					if (wTop == top) {
						closerTop = wTop;
						closerMargin = 0;
						closer = w;
						break;
					} else if (wTop < top) {
						if ((top - wTop) < closerMargin) {
							closerTop = wTop;
							closerMargin = top - wTop;
							closer = w;
						}
					}
				}
				
				if (closerTop == spaceTop) {
					sb.append(newLine + "android:layout_alignParentTop=\"true\"");
				} else {
					sb.append(newLine + "android:layout_below=\"@+id/" + ServiceUtils.getWidgetName(closer) + "\"");
				}
				
				if (closerMargin > 0) {
					sb.append(newLine + "android:layout_marginTop=\"" + closerMargin + "dp\"");
				}
			} else {
				// TODO: Relative positioning for X and Y axis
				int closerLeft = spaceLeft;
				int closerTop = spaceTop;
				int closerMarginX = left - spaceLeft - 10;
				int closerMarginY = top - spaceTop - 35;
				Class closerX = null;
				Class closerY = null;
				
				for (Class w : predecessors) {
					Stereotype wStereo = ServiceUtils.getWidgetStereotype(w);
					int wLeft = ServiceUtils.getPosX(w, wStereo) - ServiceUtils.getWidth(w, wStereo)/2;
					int wTop = ServiceUtils.getPosY(w, wStereo) - ServiceUtils.getHeight(w, wStereo)/2;
					
					if (wLeft == left) {
						closerLeft = wLeft;
						closerMarginX = 0;
						closerX = w;
						break;
					} else if (wLeft < left) {
						if ((left - wLeft) < closerMarginX) {
							closerLeft = wLeft;
							closerMarginX = left - wLeft;
							closerX = w;
						}
					}
					
					if (wTop == top) {
						closerTop = wTop;
						closerMarginY = 0;
						closerY = w;
						break;
					} else if (wTop < top) {
						if ((top - wTop) < closerMarginY) {
							closerTop = wTop;
							closerMarginY = top - wTop;
							closerY = w;
						}
					}
				}
				
				if (closerLeft == spaceLeft) {
					sb.append("android:layout_alignParentLeft=\"true\"");
				} else {
					sb.append("android:layout_alignLeft=\"@+id/" + ServiceUtils.getWidgetName(closerX) + "\"");
				}
				
				if (closerTop == spaceTop) {
					sb.append(newLine + "android:layout_alignParentTop=\"true\"");
				} else {
					sb.append(newLine + "android:layout_below=\"@+id/" + ServiceUtils.getWidgetName(closerY) + "\"");
				}
				
				if (closerMarginX > 0) {
					sb.append(newLine + "android:layout_marginLeft=\"" + closerMarginX + "dp\"");
				}
				
				if (closerMarginY > 0) {
					sb.append(newLine + "android:layout_marginTop=\"" + closerMarginY + "dp\"");
				}
			}
		} else {
			// Align with parent
			if (sb.length() > 0) {
				// only Y remaining
				int distTop = top - spaceTop;
				int distBottom = spaceBottom - bottom;
				
				if (distTop <= distBottom) {
					sb.append(newLine + "android:layout_alignParentTop=\"true\"");
					if (distTop > 35) {
						int margin = distTop - 35;
						sb.append(newLine + "android:layout_marginTop=\"" + margin  + "dp\"");
					}
				} else {
					sb.append(newLine + "android:layout_alignParentBottom=\"true\"");
					if (distBottom > 10) {
						int margin = distBottom - 10;
						sb.append(newLine + "android:layout_marginBottom=\"" + margin  + "dp\"");
					}
				}
			} else {
				int distLeft = left - spaceLeft;
				int distRight = right -spaceRight;
				int distTop = top - spaceTop;
				int distBottom = spaceBottom - bottom;
				// Set X positioning
				if (distLeft <= distRight) {
					sb.append("android:layout_alignParentLeft=\"true\"");
					if (distLeft > 10) {
						int margin = distLeft - 10;
						sb.append(newLine + "android:layout_marginLeft=\"" + margin  + "dp\"");
					}
				} else {
					sb.append("android:layout_alignParentRight=\"true\"");
					if (distRight > 10) {
						int margin = distRight - 10;
						sb.append(newLine + "android:layout_marginRight=\"" + margin  + "dp\"");
					}
				}
				// Set Y positioning
				if (distTop <= distBottom) {
					sb.append(newLine + "android:layout_alignParentTop=\"true\"");
					if (distTop > 35) {
						int margin = distTop - 35;
						sb.append(newLine + "android:layout_marginTop=\"" + margin  + "dp\"");
					}
				} else {
					sb.append(newLine + "android:layout_alignParentBottom=\"true\"");
					if (distBottom > 10) {
						int margin = distBottom - 10;
						sb.append(newLine + "android:layout_marginBottom=\"" + margin  + "dp\"");
					}
				}
			}
		}
		return sb.toString();
	}
	
	/**
	 * Checks if a string contains the other one specified. 
	 * 
	 * @param s1 the string where the search is performed
	 * @param s2 the string to search for
	 * @return true if string s1 contains s2, false otherwise
	 */
	public boolean stringContains(String s1, String s2) {
		return s1.contains(s2);
	}
	
	/**
	 * Auxiliary Methods
	 */

	/**
	 * Puts the first letter of a string in upper case and returns it.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in upper case
	 */
	private String toUpperFirst(String s) {
		return s.substring(0, 1).toUpperCase() + s.substring(1);
	}
	
	/**
	 * Puts the first letter of a string in lower case and returns it.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in lower case
	 */
	@SuppressWarnings("unused")
	private String toLowerFirst(String s) {
		return s.substring(0, 1).toLowerCase() + s.substring(1);
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
	
	/**
	 * Compares XisWidgets according to their positions.
	 * 
	 * @author André Ribeiro
	 * @see Comparator
	 */
	class WidgetComparator implements Comparator<Class> {

		@Override
		public int compare(Class c1, Class c2) {
			Stereotype s1 = ServiceUtils.getWidgetStereotype(c1);
			Stereotype s2 = ServiceUtils.getWidgetStereotype(c2);
			int x1 = ServiceUtils.getPosX(c1, s1);
			int x2 = ServiceUtils.getPosX(c2, s2);
			int y1 = ServiceUtils.getPosY(c1, s1);
			int y2 = ServiceUtils.getPosY(c2, s2);

			if (y1 < y2) {
				return -y2;
			} else if (y1 > y2) {
				return y1;
			} else if (x1 < x2) {
				return -x2;
			} else if (x1 >= x2) {
				return x1;
			} else {
				return -x2;
			}
		}
	}
}
